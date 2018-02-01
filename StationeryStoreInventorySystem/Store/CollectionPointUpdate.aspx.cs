using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CollectionPointUpdate : System.Web.UI.Page
{
    RetrievalControl retCon = new RetrievalControl();     

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["RetrievalID"] == null)
        {
            Response.Redirect(LoginController.RequisitionListClerkURI);
        }
        else
        {
            if (Request.UrlReferrer != null) // if previous page is not null
            {
                // if previous page is CollectionPointUpdate or RetrievalListDetail or RetrievalShortfall
                if (Request.UrlReferrer.ToString().Contains("CollectionPointUpdate") || Request.UrlReferrer.ToString().Contains("RetrievalListDetail") || Request.UrlReferrer.ToString().Contains("RetrievalShortfall"))
                {
                    if (!IsPostBack)
                    {
                            int retrievalId = (int)Session["RetrievalID"];
                            gvCollectionPoint.DataSource = retCon.DisplayCollectionPoint(retrievalId);
                            gvCollectionPoint.DataBind();

                    }
                }
                else
                {
                    Response.Redirect(LoginController.RequisitionListClerkURI);
                }
            }
            else
            {
                Response.Redirect(LoginController.RequisitionListClerkURI);
            }
        }
            
    }

    protected void DateValidator(object source, ServerValidateEventArgs args)
    {
        string date = txtSDate.Text;
        
        string todayDate = DateTime.Today.ToString("yyyy-MM-dd");
        if (Convert.ToDateTime(date) > Convert.ToDateTime(todayDate))
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }


    protected void Submit_Click(object sender, EventArgs e)
    {
    //    if (Session["RetrievalID"] != null)
    //    {
            int retrievalId = (int)Session["RetrievalID"];
        //}
        //else
        //{
        //    Response.Redirect(LoginController.DisbursementListURI);
        //}
   

        DateTime date = DateTime.Parse(txtSDate.Text);

        if (Page.IsValid)
        {
            foreach (GridViewRow row in gvCollectionPoint.Rows)
            {
                string collectionPoint = (row.FindControl("labCollectionPoint") as Label).Text;
                // DateTime date = DateTime.Parse((row.FindControl("txtDate") as TextBox).Text);
                string time = (row.FindControl("time") as TextBox).Text;
                retCon.SaveCollectionTimeAndDateToDisbursement(retrievalId, collectionPoint, date, time);
            }

            if (Session["discrepancyList"] != null)
            {
                if (((Dictionary<Item, int>)Session["discrepancyList"]).Count != 0)////////////////////////////////////
                {
                    Response.Redirect(LoginController.GenerateDiscrepancyV2URI);
                }
                else
                {
                    Session["discrepancyList"] = null;
                    Session["RetrievalShortfallItemList"] = null;
                    Session["RetrievalID"] = null;
                    Response.Redirect(LoginController.DisbursementListURI);
                }
            }
            else
            {
                Session["RetrievalShortfallItemList"] = null;
                Session["RetrievalID"] = null;
                Response.Redirect(LoginController.DisbursementListURI);
            }
        }

    }
}