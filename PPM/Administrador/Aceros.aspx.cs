using PPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPM.Administrador
{
    public partial class Aceros : System.Web.UI.Page
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
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Default.aspx", true);
                }
                lnkSearch_Click(null, null);
            }
        }

        public void alerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), DateTime.Now.ToString(), "alert('" + mensaje.Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "") + "');", true);
        }

        protected void lnkAcerosSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha))
                {
                    load_aceros(-1, -1, 0);
                }
                else
                {
                    throw new Exception("Selecciona una Prensa");
                }

            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            load_aceros(-1, -1, 0);
        }

        protected void load_aceros(int selectedIndex, int editIndex, int pageIndex)
        {
            DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha);
            string search = txtAcerosSearch.Text.Trim();
            using (PPMEntities db = new PPMEntities())
            {
                gvAceros.DataSource = db.EstatusAceros.Where(_ =>  _.Fecha == _Fecha && _.Rollo.Contains(search)).OrderBy(_ => _.Hora).ToList();
                gvAceros.SelectedIndex = selectedIndex;
                gvAceros.EditIndex = editIndex;
                gvAceros.PageIndex = pageIndex;
                gvAceros.DataBind();
            }
        }

        protected void lnkAceros_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha) && _Fecha.Year > 1900)
                {
                    hdnAcero_id.Value = "0";
                    txtAcero_Responsable.Text = "";
                    txtAcero_Rollo.Text = "";
                    txtAcero_Hora.Text = DateTime.Now.ToString("HH:mm");
                    mdlAceros.Show();
                }
                else
                    throw new Exception("Selecciona una Fecha");

            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvAceros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int.TryParse(gvAceros.DataKeys[e.RowIndex].Value.ToString(), out int _idAcero);
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.EstatusAceros.Where(_ => _.id == _idAcero).FirstOrDefault();
                    if (item != null)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                    }
                    load_aceros(-1, -1, 0);
                }
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvAceros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                int.TryParse(gvAceros.DataKeys[e.NewEditIndex].Value.ToString(), out int _idAcero);
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.EstatusAceros.Where(_ => _.id == _idAcero).FirstOrDefault();
                    if (item != null)
                    {
                        hdnAcero_id.Value = item.id.ToString();
                        txtAcero_Responsable.Text = item.Responsable.Trim();
                        txtAcero_Rollo.Text = item.Rollo;
                        txtAcero_Hora.Text = item.Hora;
                    }
                    load_aceros(-1, e.NewEditIndex, gvAceros.PageIndex);
                    mdlAceros.Show();
                }
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void btnAcero_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                int.TryParse(hdnAcero_id.Value, out int _idAcero);
                if (txtAcero_Responsable.Text.Trim() == "")
                {
                    txtAcero_Responsable.Focus();
                    throw new Exception("Captura un responsable");
                }
                if (txtAcero_Rollo.Text.Trim() == "")
                {
                    txtAcero_Rollo.Focus();
                    throw new Exception("Captura un acero");
                }
                if (!TimeSpan.TryParse(txtAcero_Hora.Text, out TimeSpan _hora))
                {
                    txtAcero_Hora.Focus();
                    throw new Exception("Captura una hora valida");
                }
                if (DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _fecha))
                {
                    using (PPMEntities db = new PPMEntities())
                    {
                        var item = db.EstatusAceros.Where(_ => _.id == _idAcero).FirstOrDefault();
                        item = item != null ? item : new EstatusAceros() { Fecha = _fecha };
                        item.Responsable = txtAcero_Responsable.Text.Trim();
                        item.Rollo = txtAcero_Rollo.Text.Trim();
                        item.Hora = _hora.ToString("hh\\:mm");
                        item.usuario_captura = User.Identity.Name;
                        item.fecha_captura = DateTime.Now;
                        db.Entry(item).State = item.id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                    }
                    load_aceros(-1, -1, gvAceros.PageIndex);
                }
                else
                {
                    alerta("Fecha no valida");
                    txtFecha.Focus();
                }
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
                mdlAceros.Show();
            }
        }

        protected void gvEstatus_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void gvAceros_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            load_aceros(e.NewSelectedIndex, -1, gvAceros.PageIndex);
        }
    }
}