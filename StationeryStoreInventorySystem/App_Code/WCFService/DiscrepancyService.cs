﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DiscrepancyService" in code, svc and config file together.
public class DiscrepancyService : IDiscrepancyService
{
    public void SubmitDiscrepancies(List<WCFDiscrepancy> wdList)
    {
        List<Discrepency> dList = new List<Discrepency>();
        bool informSupervisor = false;
        bool informManager = false;

        foreach(WCFDiscrepancy wd in wdList)
        {
            Discrepency d = new Discrepency();
            d.ItemCode = wd.ItemCode;
            d.RequestedBy = Int32.Parse(wd.RequestedBy);
            d.AdjustmentQty = Int32.Parse(wd.AdjustmentQty);
            d.Remarks = wd.Remarks;
            d.Status = wd.Status;
            d.Date = DateTime.Now;

            //Code to determine the discrepancy amount
            List<PriceList> plHistory = EFBroker_PriceList.GetPriceListByItemCode(d.ItemCode);
            List<PriceList> itemPrices = new List<PriceList>();

            foreach (PriceList pl in plHistory)
            {    //Get only currently active suppliers for an item
                if (pl.TenderYear == DateTime.Now.Year.ToString())
                {
                    itemPrices.Add(pl);
                }
            }

            decimal totalPrice = 0;

            foreach (PriceList pl in itemPrices)
            {
                totalPrice += (decimal)pl.Price;
            }

            decimal averageUnitPrice = totalPrice / itemPrices.Count;
            d.TotalDiscrepencyAmount = d.AdjustmentQty * averageUnitPrice;

            //Set the approver based on discrepancy amount, and email notify them
            if (d.TotalDiscrepencyAmount < 250)
            {
                d.ApprovedBy = EFBroker_DeptEmployee.GetEmployeeListByRole("Store Supervisor")[0].EmpID;
                informSupervisor = true;
            }
            else
            {
                d.ApprovedBy = EFBroker_DeptEmployee.GetEmployeeListByRole("Store Manager")[0].EmpID;
                informManager = true;
            }
            dList.Add(d);
        }
            EFBroker_Discrepancy.SaveDiscrepencies(dList);

        if (informSupervisor)
        {
            string supervisorEmail = EFBroker_DeptEmployee.GetEmployeeListByRole("Store Supervisor")[0].Email;
            Utility.sendMail(supervisorEmail, "New Discrepancies Notification " + DateTime.Now.ToString(), "New item discrepancies have been submitted. Please log in to the system to review them. Thank you.");
        }
        if (informManager)
        {
            string managerEmail = EFBroker_DeptEmployee.GetEmployeeListByRole("Store Manager")[0].Email;
            Utility.sendMail(managerEmail, "New Discrepancies Notification " + DateTime.Now.ToString(), "New item discrepancies (worth at least $250) have been submitted. Please log in to the system to review them. Thank you.");
        }
    }
}