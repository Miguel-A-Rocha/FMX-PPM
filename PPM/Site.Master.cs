using PPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPM
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lnkAdministrador.Text = HttpContext.Current.User.Identity.Name;
            using (PPMEntities db = new PPMEntities())
            {
                var user = db.administradores.Where(_ => _.UserName == HttpContext.Current.User.Identity.Name.Trim()).FirstOrDefault();
                if (user == null || !user.activo)
                {
                    liAdministrador.Visible = false;
                }
            }
        }
    }
}