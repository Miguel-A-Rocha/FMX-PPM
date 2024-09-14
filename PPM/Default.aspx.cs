using PPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPM
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
                btnBuscar_Click(null, null);
            }
        }

        public void alerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), DateTime.Now.ToString(), "alert('" + mensaje.Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "") + "');", true);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                load_prensas(-1, -1, 0);
            }
            catch (Exception ex)
            {
                alerta(ex.Message);
            }
        }

        protected void load_prensas(int selectedIndex, int editIndex, int pageIndex)
        {
            using (PPMEntities db = new PPMEntities())
            {
                rptPrensas.DataSource = db.Prensas.Where(_ => _.activo).ToList();
                rptPrensas.DataBind();
            }
        }

        protected void rptPrensas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha);
            Int32.TryParse(ddlTurno.SelectedValue.Trim(), out Int32 _Turno);
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var data = e.Item.DataItem as Prensas;
                GridView gv = e.Item.FindControl("gvPrograma") as GridView;
                if (gv != null)
                {
                    using (PPMEntities db = new PPMEntities())
                    {
                        gv.DataSource = db.Programa.Where(_=>_.Fecha == _Fecha && _.Turno == _Turno && _.PrensaId == data.id).OrderBy(_=>_.Secuencia).ToList();
                        gv.DataBind();
                    }
                }
            }
        }

        protected void Unnamed_Tick(object sender, EventArgs e)
        {
            btnBuscar_Click(null, null);
        }
    }
}