using PPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPM.Administrador
{
    public partial class Catalogos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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
                lnkAdministradorSearch_Click(null, null);
                lnkPrensaSearch_Click(null, null);
            }
        }

        public void alerta(string mensaje)
        {
            mensaje = mensaje.Replace(Environment.NewLine, "").Replace(Environment.NewLine, "").Replace(Environment.NewLine, "").Replace(Environment.NewLine, "").Replace(Environment.NewLine, "").Replace(Environment.NewLine, "").Replace(Environment.NewLine, "");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), DateTime.Now.ToString(), "alert('" + mensaje.Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "") + "');", true);
        }

        protected void load_registros(int selectedIndex, int editIndex, int pageIndex)
        {
            string search = txtAdministradorSearch.Text.Trim();
            using (PPMEntities db = new PPMEntities())
            {
                gvAdministradores.DataSource = db.administradores.Where(_ => _.UserName.Contains(search) || _.nombre.Contains(search)).OrderBy(_ => _.nombre).ToList();
                gvAdministradores.SelectedIndex = selectedIndex;
                gvAdministradores.EditIndex = editIndex;
                gvAdministradores.PageIndex = pageIndex;
                gvAdministradores.DataBind();
            }
        }

        protected void lnkAdministradorSearch_Click(object sender, EventArgs e)
        {
            try
            {
                load_registros(-1, -1, 0);
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void lnkAdministrador_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                txtAdministrador_Username.Enabled = true;
                txtAdministrador_Username.Text = "";
                txtAdministrador_Nombre.Text = "";
                chkAdministrador_Activo.Checked = false;
                mdlAdministrador.Show();
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvAdministradores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string _username = gvAdministradores.DataKeys[e.RowIndex].Value.ToString();
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.administradores.Where(_ => _.UserName == _username).FirstOrDefault();
                    if (item != null)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                    }
                    load_registros(-1, -1, 0);
                }
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvAdministradores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                string _username = gvAdministradores.DataKeys[e.NewEditIndex].Value.ToString();
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.administradores.Where(_ => _.UserName == _username).FirstOrDefault();
                    if (item != null)
                    {
                        txtAdministrador_Username.Text = item.UserName.Trim();
                        txtAdministrador_Nombre.Text = item.nombre.Trim();
                        chkAdministrador_Activo.Checked = item.activo;
                    }
                    load_registros(-1, e.NewEditIndex, gvAdministradores.PageIndex);
                    txtAdministrador_Username.Enabled = false;
                    mdlAdministrador.Show();
                }
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void btnAdministrador_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdministrador_Username.Text.Trim() == "")
                {
                    alerta("Catura username");
                    txtAdministrador_Username.Focus();
                    mdlAdministrador.Show();
                    return;
                }
                if (txtAdministrador_Nombre.Text.Trim() == "")
                {
                    alerta("Catura nombre");
                    txtAdministrador_Username.Focus();
                    mdlAdministrador.Show();
                    return;
                }
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.administradores.Where(_ => _.UserName == txtAdministrador_Username.Text.Trim()).FirstOrDefault();
                    bool existe = item != null;
                    item = item != null ? item : new administradores() { UserName = txtAdministrador_Username.Text.Trim() };
                    item.nombre = txtAdministrador_Nombre.Text.Trim();
                    item.activo = chkAdministrador_Activo.Checked;
                    db.Entry(item).State = existe ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }
                load_registros(-1, -1, gvAdministradores.PageIndex);
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
                mdlAdministrador.Show();
            }
        }


        protected void load_prensas(int selectedIndex, int editIndex, int pageIndex)
        {
            string search = txtPrensaSearch.Text.Trim();
            using (PPMEntities db = new PPMEntities())
            {
                gvPrensas.DataSource = db.Prensas.Where(_ => _.nombre.Contains(search)).OrderBy(_ => _.id).ToList();
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

        protected void lnkPrensa_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                txtPrensa_Id.Enabled = true;
                txtPrensa_Id.ReadOnly = false;
                txtPrensa_Id.Text = "";
                txtPrensa_Nombre.Text = "";
                //txtPrensa_Descripcion.Text = "";
                chkPrensa_Activo.Checked = false;
                mdlPrensa.Show();
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvPrensas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int.TryParse(gvPrensas.DataKeys[e.RowIndex].Value.ToString(), out int _idPrensa);
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.Prensas.Where(_ => _.id == _idPrensa).FirstOrDefault();
                    if (item != null)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                    }
                    load_prensas(-1, -1, 0);
                }
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvPrensas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                int.TryParse(gvPrensas.DataKeys[e.NewEditIndex].Value.ToString(), out int _idPrensa);
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.Prensas.Where(_ => _.id == _idPrensa).FirstOrDefault();
                    if (item != null)
                    {
                        txtPrensa_Id.Enabled = false;
                        txtPrensa_Id.ReadOnly = true;
                        txtPrensa_Id.Text = item.id.ToString();
                        txtPrensa_Nombre.Text = item.nombre.Trim();
                        //txtPrensa_Descripcion.Text = item.descripcion.Trim();
                        chkPrensa_Activo.Checked = item.activo;
                    }
                    load_prensas(-1, e.NewEditIndex, gvPrensas.PageIndex);
                    mdlPrensa.Show();
                }
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void btnPrensa_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPrensa_Nombre.Text.Trim() == "")
                    throw new Exception("Captura el nombre de la Prensa");

                int.TryParse(txtPrensa_Id.Text, out int _idPrensa);
                if (_idPrensa > 0)
                {
                    using (PPMEntities db = new PPMEntities())
                    {
                        var item = db.Prensas.Where(_ => _.id == _idPrensa).FirstOrDefault();
                        item = item != null ? item : new Prensas() { };
                        item.id = _idPrensa;
                        item.nombre = txtPrensa_Nombre.Text.Trim();
                        //item.descripcion = txtPrensa_Descripcion.Text.Trim();
                        item.activo = chkPrensa_Activo.Checked;
                        //item.fecha_captura = DateTime.Now;
                        //item.usuario_captura = User.Identity.Name;
                        //db.Entry(item).State = item.id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
                        db.Entry(item).State = !txtPrensa_Id.Enabled ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                    }
                    load_prensas(-1, -1, gvPrensas.PageIndex);
                }
                else
                    throw new Exception("Captura el Id de la prensa");
            }
            catch (Exception ex)
            {
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
                mdlPrensa.Show();
            }
        }

        protected void gvPrensas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            txtPrensa_Id.Text = gvPrensas.DataKeys[e.NewSelectedIndex].Value.ToString();
            load_prensas(e.NewSelectedIndex, -1, gvPrensas.PageIndex);
        }
    }
}