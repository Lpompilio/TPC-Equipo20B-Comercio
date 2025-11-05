using System;

namespace TPC_Equipo20B
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("Dashboard.aspx");
        }
    }
}
