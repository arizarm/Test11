using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisbursementListDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string disbId = Session["SelectedDisb"].ToString();

        StationeryEntities context = new StationeryEntities();

        List<Disbursement_Item> disbDetails = new List<Disbursement_Item>();
        disbDetails = context.Disbursement_Item.Where(x => x.DisbursementID.ToString().Equals(disbId)).ToList();       

        gvDisbDetail.DataSource = disbDetails;
        gvDisbDetail.DataBind();

        List<Disbursement> testDisb = new List<Disbursement>();
        testDisb = context.Disbursements.Where(x => x.DisbursementID.ToString().Equals(disbId)).ToList();        

        gvTestDisb.DataSource = DisbursementCotrol.gvDisbursementDetailPopulate(disbId);
        gvTestDisb.DataBind();
    }


   
}