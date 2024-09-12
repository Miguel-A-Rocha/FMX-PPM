using PPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPM.Administrador
{
    public partial class Captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtFecha.Text = DateTime.Now.ToString("yyyy/MM/dd");
                try
                {
                    using (PPMEntities db = new PPMEntities())
                    {
                        var user = db.administradores.Where(_ => _.UserName == HttpContext.Current.User.Identity.Name.Trim()).FirstOrDefault();
                        if (user == null || !user.activo)
                        {
                            Response.Redirect("~/Default.aspx", true);
                        }

                        ddlPrograma_Estatus.Items.Clear();
                        foreach (var estatus in db.Estatus.ToList())
                        {
                            ddlPrograma_Estatus.Items.Add(new ListItem() { Value = estatus.id.ToString(), Text = estatus.nombre.Trim(), Enabled = estatus.activo });
                        }
                        ddlPrograma_Estatus.Items.Insert(0, new ListItem() { Value = "0", Text = "SELECCIONAR" });
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Default.aspx", true);
                }
                lnkPrensaSearch_Click(null, null);
            }
        }

        public void alerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), DateTime.Now.ToString(), "alert('" + mensaje.Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "") + "');", true);
        }

        protected void load_prensas(int selectedIndex, int editIndex, int pageIndex)
        {
            string search = txtPrensaSearch.Text.Trim();
            using (PPMEntities db = new PPMEntities())
            {
                gvPrensas.DataSource = db.Prensas.Where(_ => _.nombre.Contains(search)).OrderBy(_ => _.nombre).ToList();
                gvPrensas.SelectedIndex = selectedIndex;
                gvPrensas.EditIndex = editIndex;
                gvPrensas.PageIndex = pageIndex;
                gvPrensas.DataBind();
            }
        }

        protected void lnkPrensaSearch_Click(object sender, EventArgs e)
        {
            try
            {
                load_prensas(-1, -1, 0);
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvPrensas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            hdnPrensa_id.Value = gvPrensas.DataKeys[e.NewSelectedIndex].Value.ToString();
            load_prensas(e.NewSelectedIndex, -1, gvPrensas.PageIndex);
            load_programas(-1, -1, 0);
            load_estatus(-1, -1, 0);
        }


        protected void load_programas(int selectedIndex, int editIndex, int pageIndex)
        {
            Int32.TryParse(hdnPrensa_id.Value.Trim(), out Int32 _PrensaId);
            string search = txtProgramaSearch.Text.Trim();
            using (PPMEntities db = new PPMEntities())
            {
                gvProgramas.DataSource = db.Programa.Where(_ => _.PrensaId == _PrensaId).OrderBy(_ => _.NoParte).ToList();
                gvProgramas.SelectedIndex = selectedIndex;
                gvProgramas.EditIndex = editIndex;
                gvProgramas.PageIndex = pageIndex;
                gvProgramas.DataBind();
            }
        }

        protected void load_estatus(int selectedIndex, int editIndex, int pageIndex)
        {
            using (PPMEntities db = new PPMEntities())
            {
                gvEstatus.DataSource = db.Estatus.Where(_=>_.activo).ToList();
                gvEstatus.SelectedIndex = selectedIndex;
                gvEstatus.EditIndex = editIndex;
                gvEstatus.PageIndex = pageIndex;
                gvEstatus.DataBind();
            }
        }

        protected void lnkProgramaSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Int32.TryParse(hdnPrensa_id.Value.Trim(), out Int32 _PrensaId);
                if (_PrensaId <= 0)
                    throw new Exception("Selecciona una Prensa");
                load_programas(-1, -1, 0);
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void lnkPrograma_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Int32.TryParse(hdnPrensa_id.Value.Trim(), out Int32 _PrensaId) && _PrensaId > 0)
                {
                    if (Int32.TryParse(ddlTurno.SelectedValue, out Int32 _Turno) && _Turno > 0)
                    {
                        hdnPrograma_id.Value = "0";
                        txtPrograma_NoParte.Text = "";
                        ddlPrograma_Estatus.SelectedValue = "0";
                        mdlPrograma.Show();
                    }
                    else
                        throw new Exception("Selecciona un Turno");
                }
                else
                    throw new Exception("Selecciona una Prensa");

            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvProgramas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int.TryParse(gvProgramas.DataKeys[e.RowIndex].Value.ToString(), out int _idPrograma);
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.Programa.Where(_ => _.id == _idPrograma).FirstOrDefault();
                    if (item != null)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                    }
                    load_programas(-1, -1, 0);
                }
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvProgramas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                int.TryParse(gvProgramas.DataKeys[e.NewEditIndex].Value.ToString(), out int _idPrograma);
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.Programa.Where(_ => _.id == _idPrograma).FirstOrDefault();
                    if (item != null)
                    {
                        hdnPrograma_id.Value = item.id.ToString();
                        txtPrograma_NoParte.Text = item.NoParte.Trim();
                        ddlPrograma_Estatus.SelectedValue = item.EstatusId.ToString();
                    }
                    load_programas(-1, e.NewEditIndex, gvProgramas.PageIndex);
                    mdlPrograma.Show();
                }
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void btnPrograma_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                Int32.TryParse(hdnPrensa_id.Value.Trim(), out Int32 _PrensaId);
                if (_PrensaId <= 0)
                    throw new Exception("Selecciona una prensa");
                int.TryParse(hdnPrograma_id.Value, out int _idPrograma);
                int.TryParse(ddlPrograma_Estatus.SelectedValue, out int _EstatusId);
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.Programa.Where(_ => _.id == _idPrograma).FirstOrDefault();
                    item = item != null ? item : new Programa() { };
                    item.PrensaId = _PrensaId;
                    item.NoParte = txtPrograma_NoParte.Text.Trim();
                    item.EstatusId = _EstatusId;
                    //item.Fecha =
                    //item.Turno =
                    item.usuario_captura = User.Identity.Name;
                    item.fecha_captura = DateTime.Now;
                    db.Entry(item).State = item.id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }
                load_programas(-1, -1, gvProgramas.PageIndex);
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
                mdlPrograma.Show();
            }
        }

        protected void gvEstatus_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void ddlTurno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {

        }
    }
}