using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopSite
{
    public partial class update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://www.youtube.com/watch?v=HbW-Bnm6Ipg");
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void excecuteBtn_Click(object sender, EventArgs e)
        {

        }
    }
}