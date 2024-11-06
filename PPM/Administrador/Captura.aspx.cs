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
            ///Valida si la pagina no se esta cargando por algun evento disparado por el usuario
            if (!Page.IsPostBack)
            {
                txtFecha.Text = DateTime.Now.ToString("yyyy/MM/dd");
                try
                {
                    //Establece una nueva conexion de base de datos 
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

                        ddlPrograma_EstatusPrensa.Items.Clear();
                        foreach (var estatus in db.EstatusPrensa.ToList())
                        {
                            ddlPrograma_EstatusPrensa.Items.Add(new ListItem() { Value = estatus.id.ToString(), Text = estatus.descripcion.Trim(), Enabled = estatus.activo });
                        }
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Default.aspx", true);
                }
                load_prensas(-1, -1, 0);
                load_estatus(-1, -1, 0);
                load_estatus_prensa(-1, -1, 0);
                lnkSearch_Click(null, null);
            }
        }

        public void alerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), DateTime.Now.ToString(), "alert('" + mensaje.Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "") + "');", true);
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            load_programas(-1, -1, 0);
        }

        protected void load_prensas(int selectedIndex, int editIndex, int pageIndex)
        {
            string search = txtPrensaSearch.Text.Trim();
            //Establece una nueva conexion de base de datos 
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
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvPrensas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            hdnPrensa_id.Value = gvPrensas.DataKeys[e.NewSelectedIndex].Value.ToString();
            load_prensas(e.NewSelectedIndex, -1, gvPrensas.PageIndex);
            load_programas(-1, -1, 0);
        }


        protected void load_programas(int selectedIndex, int editIndex, int pageIndex)
        {
            Int32.TryParse(hdnPrensa_id.Value.Trim(), out Int32 _PrensaId);
            DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha);
            string search = txtProgramaSearch.Text.Trim();
            //Establece una nueva conexion de base de datos 
            using (PPMEntities db = new PPMEntities())
            {
                gvProgramas.DataSource = db.Programa.Where(_ => _.PrensaId == _PrensaId && _.Fecha == _Fecha && _.NoParte.Contains(search)).OrderBy(_ => _.NoParte).OrderBy(_=>_.Fecha).ThenBy(_=>_.Hora).ThenBy(_=>_.Secuencia).ToList();
                gvProgramas.SelectedIndex = selectedIndex;
                gvProgramas.EditIndex = editIndex;
                gvProgramas.PageIndex = pageIndex;
                gvProgramas.DataBind();
            }
        }

        protected void load_estatus(int selectedIndex, int editIndex, int pageIndex)
        {
            //Establece una nueva conexion de base de datos 
            using (PPMEntities db = new PPMEntities())
            {
                gvEstatus.DataSource = db.Estatus.Where(_=>_.activo).ToList();
                gvEstatus.SelectedIndex = selectedIndex;
                gvEstatus.EditIndex = editIndex;
                gvEstatus.PageIndex = pageIndex;
                gvEstatus.DataBind();
            }
        }

        protected void load_estatus_prensa(int selectedIndex, int editIndex, int pageIndex)
        {
            //Establece una nueva conexion de base de datos 
            using (PPMEntities db = new PPMEntities())
            {
                gvEstatusPrensa.DataSource = db.EstatusPrensa.Where(_ => _.activo).ToList();
                gvEstatusPrensa.SelectedIndex = selectedIndex;
                gvEstatusPrensa.EditIndex = editIndex;
                gvEstatusPrensa.PageIndex = pageIndex;
                gvEstatusPrensa.DataBind();
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
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void lnkPrograma_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Int32.TryParse(hdnPrensa_id.Value.Trim(), out Int32 _PrensaId) && _PrensaId > 0)
                {
                    if (DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha) && _Fecha.Year > 1900)
                    {
                        hdnPrograma_id.Value = "0";
                        txtPrograma_Hora.Text = DateTime.Now.ToString("HH:mm");
                        txtPrograma_NoParte.Text = "";
                        txtPrograma_CantidadProgramada.Text = "";
                        txtPrograma_CantidadCorrida.Text = "";
                        ddlPrograma_Estatus.SelectedIndex = -1; //.SelectedValue = "1";//Programado
                        ddlPrograma_EstatusPrensa.SelectedIndex = -1; //.SelectedValue = "0";
                        mdlPrograma.Show();
                    }
                    else
                        throw new Exception("Selecciona una Fecha");
                }
                else
                    throw new Exception("Selecciona una Prensa");

            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvProgramas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int.TryParse(gvProgramas.DataKeys[e.RowIndex].Value.ToString(), out int _idPrograma);
                //Establece una nueva conexion de base de datos 
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
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        protected void gvProgramas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                int.TryParse(gvProgramas.DataKeys[e.NewEditIndex].Value.ToString(), out int _idPrograma);
                //Establece una nueva conexion de base de datos 
                using (PPMEntities db = new PPMEntities())
                {
                    var item = db.Programa.Where(_ => _.id == _idPrograma).FirstOrDefault();
                    if (item != null)
                    {
                        hdnPrograma_id.Value = item.id.ToString();
                        txtPrograma_Hora.Text = item.Hora != null ? item.Hora.Value.ToString("hh\\:mm") : "";
                        txtPrograma_NoParte.Text = item.NoParte.Trim();
                        ddlPrograma_Estatus.SelectedValue = item.EstatusId.ToString();
                        txtPrograma_CantidadProgramada.Text = item.cantidad_programada != null ? item.cantidad_programada.ToString() : "";
                        txtPrograma_CantidadCorrida.Text = item.cantidad_corrida != null ? item.cantidad_corrida.ToString() : "";
                        ddlPrograma_EstatusPrensa.SelectedValue = item.EstatusPrensaId.ToString();
                    }
                    load_programas(-1, e.NewEditIndex, gvProgramas.PageIndex);
                    mdlPrograma.Show();
                }
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
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
                int.TryParse(ddlPrograma_EstatusPrensa.SelectedValue, out int _EstatusPrensaId);
                TimeSpan? _hora = null;
                if (TimeSpan.TryParse(txtPrograma_Hora.Text, out TimeSpan hora))
                {
                    _hora= hora;
                }
                double? _cantidad_programada = null, _cantidad_corrida = null;
                if (double.TryParse(txtPrograma_CantidadProgramada.Text.Trim(), out double cantidad_programada))
                {
                    _cantidad_programada = cantidad_programada;
                }
                if (double.TryParse(txtPrograma_CantidadCorrida.Text.Trim(), out double cantidad_corrida))
                {
                    _cantidad_corrida = cantidad_corrida;
                }
                if (DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _fecha))
                {
                    //Establece una nueva conexion de base de datos 
                    using (PPMEntities db = new PPMEntities())
                    {
                        int? _secuencia = db.Programa.Where(_ => _.PrensaId == _PrensaId && _.Fecha == _fecha).Max(_ => _.Secuencia).GetValueOrDefault() + 1;
                        var item = db.Programa.Where(_ => _.id == _idPrograma).FirstOrDefault();
                        item = item != null ? item : new Programa() { };
                        item.PrensaId = _PrensaId;
                        item.NoParte = txtPrograma_NoParte.Text.Trim();
                        item.EstatusId = _EstatusId;
                        item.Fecha = item.id > 0 ? item.Fecha : _fecha;
                        item.Hora = _hora;
                        item.Secuencia = item.id > 0 ? item.Secuencia : (_secuencia ?? 1);
                        item.EstatusPrensaId = _EstatusPrensaId;
                        item.cantidad_programada = _cantidad_programada;
                        item.cantidad_corrida = _cantidad_corrida;
                        item.usuario_captura = User.Identity.Name;
                        item.fecha_captura = DateTime.Now;
                        db.Entry(item).State = item.id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                    }
                    load_programas(-1, -1, gvProgramas.PageIndex);
                }
                else
                {
                    alerta("Fecha no valida");
                    txtFecha.Focus();
                }
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
                mdlPrograma.Show();
            }
        }

        protected void gvEstatus_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void gvProgramas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            load_programas(e.NewSelectedIndex, -1, gvProgramas.PageIndex);
        }

        protected void gvProgramas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Int32.TryParse(gvProgramas.DataKeys[row.RowIndex].Value.ToString(), out Int32 _Id);
                if (row.RowIndex == 0 && e.CommandName == "subir") return;
                if (row.RowIndex == (gvProgramas.Rows.Count - 1) && e.CommandName == "bajar") return;
                if (int.TryParse(e.CommandArgument.ToString(), out int secuencia) && secuencia >= 0)
                {
                    //Establece una nueva conexion de base de datos 
                    using (PPMEntities db = new PPMEntities())
                    {
                        Programa programa = db.Programa.Where(_ => _.id == _Id).FirstOrDefault();
                        if (programa != null)
                        {
                            Programa aux = null;
                            switch (e.CommandName)
                            {
                                case "subir":
                                    aux = db.Programa.Where(_ => _.PrensaId == programa.PrensaId && _.Fecha == programa.Fecha && _.Secuencia < secuencia).OrderByDescending(_ => _.Secuencia).FirstOrDefault();
                                    break;
                                case "bajar":
                                    aux = db.Programa.Where(_ => _.PrensaId == programa.PrensaId && _.Fecha == programa.Fecha && _.Secuencia > secuencia).OrderBy(_ => _.Secuencia).FirstOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            if (aux != null)
                            {
                                programa.Secuencia = aux.Secuencia;
                                aux.Secuencia = secuencia;
                                db.SaveChanges();
                                load_programas(-1, -1, gvProgramas.PageIndex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla indicando que ocurrio un error al ordenar el programa
                alerta("Ocurrio un problema al ordenar el programa");
            }
        }
    }
}