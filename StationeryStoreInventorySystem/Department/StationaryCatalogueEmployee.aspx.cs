using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StationaryCatalogueEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SamCatalogue i1 = new SamCatalogue("C001", "Clip", "Clips Double 1", "50", "30", "Dozen");
        SamCatalogue i2 = new SamCatalogue("E120", "File", "A4 hardcover(100pg)", "100", "50", "Each");
        SamCatalogue i3 = new SamCatalogue("C001", "Clip", "Clips Double 1", "50", "30", "Dozen");
        SamCatalogue i4 = new SamCatalogue("E120", "File", "A4 hardcover(100pg)", "100", "50", "Each");
        SamCatalogue i5 = new SamCatalogue("C001", "Clip", "Clips Double 1", "50", "30", "Dozen");
        SamCatalogue i6 = new SamCatalogue("E120", "File", "A4 hardcover(100pg)", "100", "50", "Each");

        List<SamCatalogue> Catalogues = new List<SamCatalogue>();
        Catalogues.Add(i1);
        Catalogues.Add(i2);
        Catalogues.Add(i3);
        Catalogues.Add(i4);
        Catalogues.Add(i5);
        Catalogues.Add(i6);

        GridView1.DataSource = Catalogues;
        GridView1.DataBind();
    }
}