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
                lstEstatusPrensa.Items.Clear();
                using (PPMEntities db = new PPMEntities())
                {
                    foreach (var item in db.EstatusPrensa.ToList())
                    {
                        lstEstatusPrensa.Items.Add(new ListItem() { Value = item.id.ToString(), Text = item.descripcion, Enabled = item.activo });
                    }
                }
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
                load_aceros(-1, -1, 0);
                load_estatus(-1, -1, 0);
                load_estatus_prensa(-1, -1, 0);
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

        protected void load_aceros(int selectedIndex, int editIndex, int pageIndex)
        {
            DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha);
            using (PPMEntities db = new PPMEntities())
            {
                gvAceros.DataSource = db.EstatusAceros.Where(_ => _.Fecha == _Fecha).OrderBy(_ => _.Fecha).ThenBy(_=>_.Hora).ToList();
                gvAceros.SelectedIndex = selectedIndex;
                gvAceros.EditIndex = editIndex;
                gvAceros.PageIndex = pageIndex;
                gvAceros.DataBind();
            }
        }

        protected void load_estatus(int selectedIndex, int editIndex, int pageIndex)
        {
            using (PPMEntities db = new PPMEntities())
            {
                gvEstatus.DataSource = db.Estatus.Where(_ => _.activo).ToList();
                gvEstatus.SelectedIndex = selectedIndex;
                gvEstatus.EditIndex = editIndex;
                gvEstatus.PageIndex = pageIndex;
                gvEstatus.DataBind();
            }
        }

        protected void load_estatus_prensa(int selectedIndex, int editIndex, int pageIndex)
        {
            using (PPMEntities db = new PPMEntities())
            {
                gvEstatusPrensa.DataSource = db.EstatusPrensa.Where(_ => _.activo && _.id != 0).ToList();
                gvEstatusPrensa.SelectedIndex = selectedIndex;
                gvEstatusPrensa.EditIndex = editIndex;
                gvEstatusPrensa.PageIndex = pageIndex;
                gvEstatusPrensa.DataBind();
            }
        }

        protected void rptPrensas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha);
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var data = e.Item.DataItem as Prensas;
                GridView gv = e.Item.FindControl("gvPrograma") as GridView;
                if (gv != null)
                {
                    using (PPMEntities db = new PPMEntities())
                    {
                        gv.DataSource = db.Programa.Where(_=>_.Fecha == _Fecha && _.PrensaId == data.id).OrderBy(_=>_.Secuencia).ToList();
                        gv.DataBind();
                    }
                }
            }
        }

        protected void Unnamed_Tick(object sender, EventArgs e)
        {
            btnBuscar_Click(null, null);
        }

        protected void lstEstatusPrensa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                int.TryParse(lstEstatusPrensa.SelectedValue, out int _EstatusPrensaId);
                int.TryParse(hdnProgramaId.Value.Trim(), out int _ProgramaId);
                using (PPMEntities db = new PPMEntities())
                {
                    Programa prgm = db.Programa.Where(_ => _.id == _ProgramaId).FirstOrDefault();
                    if (prgm != null)
                    {
                        prgm.EstatusPrensaId = _EstatusPrensaId;
                        db.Entry(prgm).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        load_prensas(-1,-1,0);
                        lstEstatusPrensa.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                alerta(ex.Message);
            }
        }
    }
}