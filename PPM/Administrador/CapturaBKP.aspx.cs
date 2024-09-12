using PPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPM.Administrador
{
    public partial class CapturaBKP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
                    ddlPrensas.Items.Clear();
                    ddlTurno.Items.Clear();
                    ddlEstatus.Items.Clear();
                    ddlPrograma_Prensa.Items.Clear();
                    ddlPrograma_Turno.Items.Clear();
                    ddlPrograma_Estatus.Items.Clear();

                    using (PPMEntities db = new PPMEntities())
                    {
                        var user = db.administradores.Where(_ => _.UserName == HttpContext.Current.User.Identity.Name.Trim()).FirstOrDefault();
                        if (user == null || !user.activo)
                        {
                            Response.Redirect("~/Default.aspx", true);
                        }

                        foreach (var item in db.Prensas)
                            ddlPrensas.Items.Add(new ListItem() { Value = item.id.ToString(), Text = item.nombre, Enabled = item.activo });
                        ddlPrensas.Items.Insert(0, new ListItem() { Value = "0", Text = "TODAS" });

                        foreach (var item in db.Estatus)
                        {
                            ListItem option = new ListItem() { Value = item.id.ToString(), Text = item.nombre, Enabled = item.activo };
                            //option.Attributes.Add("class", item.color);
                            ddlEstatus.Items.Add(option);
                        }
                        ddlEstatus.Items.Insert(0, new ListItem() { Value = "0", Text = "TODOS" });

                        foreach (var item in db.Prensas)
                            ddlPrograma_Prensa.Items.Add(new ListItem() { Value = item.id.ToString(), Text = item.nombre, Enabled = item.activo });
                        ddlPrograma_Prensa.Items.Insert(0, new ListItem() { Value = "0", Text = "SELECCIONAR" });

                        foreach (var item in db.Estatus)
                        {
                            ListItem option = new ListItem() { Value = item.id.ToString(), Text = item.nombre, Enabled = item.activo };
                            //option.Attributes.Add("class", item.color);
                            ddlPrograma_Estatus.Items.Add(option);
                        }

                        //ddlPrograma_Estatus.Items.Insert(0, new ListItem() { Value = "0", Text = "SELECCIONAR" });
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Default.aspx", true);
                }
                lnkProgramaSearch_Click(null, null);
            }
        }

        public void alerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), DateTime.Now.ToString(), "alert('" + mensaje.Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "") + "');", true);
        }

        protected void ddlPrensa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                alerta("Ocurrio un problema al cargar la lista de ...");
            }
        }

        protected void ddlPrograma_Prensa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                alerta("Ocurrio un problema al cargar la lista de ...");
            }
        }

        protected void load_programa(int selectedIndex, int editIndex, int pageIndex)
        {
            using (PPMEntities db = new PPMEntities())
            {
                int.TryParse(ddlPrensas.SelectedValue, out int prensaId);
                int.TryParse(ddlTurno.SelectedValue, out int Turno);
                DateTime.TryParse(txtFecha.Text.Trim(), out DateTime fecha);
                int.TryParse(ddlEstatus.SelectedValue, out int estatusId);
                //gvPrograma.DataSource = db.GetList_Manual_Captura(prensaId, Turno, fecha, estatusId).ToList();
                gvPrograma.SelectedIndex = selectedIndex;
                gvPrograma.EditIndex = editIndex;
                gvPrograma.PageIndex = pageIndex;
                gvPrograma.DataBind();
            }
        }

        protected void lnkProgramaSearch_Click(object sender, EventArgs e)
        {
            try
            {
                load_programa(-1, -1, 0);
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
                hdnPrograma_id.Value = "0";
                ddlPrograma_Prensa.SelectedIndex = -1;
                ddlPrograma_Turno.SelectedIndex = -1;
                ddlPrograma_Estatus.SelectedIndex = -1;
                txtPrograma_Fecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
                txtPrograma_Hora.Text = DateTime.Today.ToString("HH:mm");
                txtPrograma_NoParte.Text = "";
                ddlPrograma_Prensa_SelectedIndexChanged(null, null);

            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvPrograma_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int.TryParse(gvPrograma.DataKeys[e.RowIndex].Value.ToString(), out int _id);
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.Programa.Where(_ => _.id == _id).FirstOrDefault();
                    if (item != null)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                    }
                    load_programa(-1, -1, 0);
                }
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvPrograma_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                int.TryParse(gvPrograma.DataKeys[e.NewEditIndex].Value.ToString(), out int _id);
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.Programa.Where(_ => _.id == _id).FirstOrDefault();
                    if (item != null)
                    {
                        hdnPrograma_id.Value = item.id.ToString();
                        ddlPrograma_Prensa.SelectedValue = item.PrensaId.ToString();
                        ddlPrograma_Prensa_SelectedIndexChanged(null, null);
                        ddlPrograma_Turno.SelectedValue = item.Turno.ToString();
                        ddlPrograma_Estatus.SelectedValue = item.EstatusId.ToString();
                        txtPrograma_Fecha.Text = item.Fecha.ToString("yyyy/MM/dd");
                        //txtPrograma_Hora.Text = new DateTime(item.FechaHora.Ticks).ToString("HH:mm");
                        txtPrograma_NoParte.Text = item.NoParte.ToString();
                    }
                    load_programa(-1, e.NewEditIndex, gvPrograma.PageIndex);
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

                int.TryParse(hdnPrograma_id.Value, out int _id);
                if (!int.TryParse(ddlPrograma_Prensa.SelectedValue, out int prensaId) || prensaId <= 0)
                    throw new Exception("Selecciona una Prensa");
                if (!int.TryParse(ddlPrograma_Turno.SelectedValue, out int Turno) || Turno <= 0)
                    throw new Exception("Selecciona un Turno");
                int.TryParse(ddlPrograma_Estatus.SelectedValue, out int estatusId);
                if (!DateTime.TryParse(txtPrograma_Fecha.Text.Trim(), out DateTime fecha))
                    throw new Exception("Selecciona una fecha valida");
                if (!TimeSpan.TryParse(txtPrograma_Hora.Text.Trim(), out TimeSpan hora))
                    throw new Exception("Selecciona una hora valida");
                if (!double.TryParse(txtPrograma_NoParte.Text.Trim(), out double programa))
                    throw new Exception("Captura un programa valido");
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.Programa.Where(_ => _.id == _id).FirstOrDefault();
                    item = item != null ? item : new Programa() { };
                    item.PrensaId = prensaId;
                    item.Turno = Turno;
                    item.EstatusId = estatusId;
                    item.Fecha = fecha;
                    //item.hora = hora;
                    item.NoParte = txtPrograma_NoParte.Text.Trim();
                    item.fecha_captura = DateTime.Now;
                    item.usuario_captura = User.Identity.Name;
                    db.Entry(item).State = item.id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }
                load_programa(-1, -1, gvPrograma.PageIndex);
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
                mdlPrograma.Show();
            }
        }
    }
}