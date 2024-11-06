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
        /// <summary>
        /// Evento automatico - carga inicial de pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ///Valida si la pagina es carga inicial
            if (!Page.IsPostBack)
            {
                ///Establece la fecha actual en el campo fecha
                txtFecha.Text = DateTime.Now.ToString("yyyy/MM/dd");
                try
                {
                    //Establece una conexion a la base de datos
                    using (PPMEntities db = new PPMEntities())
                    {
                        //Verifica los permisos del usuario, en caso de que no sea administrador lo redirecciona a la pantalla default.
                        var user = db.administradores.Where(_ => _.UserName == HttpContext.Current.User.Identity.Name.Trim()).FirstOrDefault();
                        if (user == null || !user.activo)
                        {
                            Response.Redirect("~/Default.aspx", true);
                        }
                    }
                }
                catch (Exception)
                {
                    //En caso de que falle la conexino a la BD redirecciona a la pantalla default
                    Response.Redirect("~/Default.aspx", true);
                }
                //Se manda llamar evento de buscar para cargar la pagina por defaul inicialmente
                lnkSearch_Click(null, null);
            }
        }

        //Metodo para mostrar mensajes en la interface de usuario
        public void alerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), DateTime.Now.ToString(), "alert('" + mensaje.Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "") + "');", true);
        }

        /// <summary>
        /// Actualiza el listado de aceros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAcerosSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida si el usuario capturo una fecha valida.
                if (DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha))
                {
                    //Muestra la lista de aceros en pantalla
                    load_aceros(-1, -1, 0);
                }
                else
                {
                    //Si el usuario no capturo una fecha valida se muestra un mensaje al usuario indicando que Seleccione una Fecha
                    throw new Exception("Selecciona una Fecha");
                }

            }
            catch (Exception ex)
            {
                //Encaso de que halla algun error muestra un mensaje en pantalla
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        //Actualiza el listado de aceros desde el campo de buscqueda
        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            //Muestra la lista de aceros en pantalla
            load_aceros(-1, -1, 0);
        }

        //Metodo para obtener el listado de aceros desde base de datos
        protected void load_aceros(int selectedIndex, int editIndex, int pageIndex)
        {
            //Convierte el texto capturado en el campo txtFecha a una veria de tipo fecha
            DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha);
            //asigna el valor capturado en la campo Search a una varia de tipo string
            string search = txtAcerosSearch.Text.Trim();
            //Establece una conexion a la base de datos
            using (PPMEntities db = new PPMEntities())
            {
                //Consulta el listado de aceros y lo asigna al objeto datasource del gridview de aceros
                gvAceros.DataSource = db.EstatusAceros.Where(_ =>  _.Fecha == _Fecha && _.Rollo.Contains(search)).OrderBy(_ => _.Hora).ToList();
                gvAceros.SelectedIndex = selectedIndex;
                gvAceros.EditIndex = editIndex;
                gvAceros.PageIndex = pageIndex;
                gvAceros.DataBind();
            }
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario da clic en el boton agregar
        /// Muestra una ventana emergente para que el usuario capture los valores necesarios para crear el registro de Acero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAceros_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida si el usuario selecciono una fecha valida y posterior al año 1900
                if (DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha) && _Fecha.Year > 1900)
                {
                    //Asigna al nuevo registo de usuario un valor de 0 como identificador
                    hdnAcero_id.Value = "0";
                    //Asigna al nuevo registro un valor en blanco para el campo de responsable
                    txtAcero_Responsable.Text = "";
                    //Asigna al nuevo registro un valor en blanco para el campo de acero/rollo
                    txtAcero_Rollo.Text = "";
                    //Asigna al nuevo registro el valor de la hora actual en campo de hora
                    txtAcero_Hora.Text = DateTime.Now.ToString("HH:mm");
                    //Muestra un recuadro en pantalla para que el usuario capture los datos del acero.
                    mdlAceros.Show();
                }
                else
                    //Si el usuaio no selecciono una fecha valida envia un mensaje al usuario indicandole que Seleccione una fecha valida.
                    throw new Exception("Selecciona una Fecha");

            }
            catch (Exception ex)
            {
                //Encaso de que halla algun error muestra un mensaje en pantalla
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario da clic en el boton eliminar de uno de los registro de acero.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAceros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //Obtiene el identificador del acero seleccionado
                int.TryParse(gvAceros.DataKeys[e.RowIndex].Value.ToString(), out int _idAcero);
                //Crea una conexion a la base de datos
                using (PPMEntities db = new PPMEntities())
                {
                    //Busca si el acero seleccionado existe en base de datos
                    var item = db.EstatusAceros.Where(_ => _.id == _idAcero).FirstOrDefault();
                    //Valida si encontro alguna acero
                    if (item != null)
                    {
                        //En caso de que hlla encontrado el acero en base de datos, establece su valor en eliminado
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        //Elimina el acero y guarda los cambios en la base de datos.
                        db.SaveChanges();
                    }
                    //Deespues de eliminar el acero, actualiza la información de pantalla, ya sin el acero eliminado
                    load_aceros(-1, -1, 0);
                }
            }
            catch (Exception ex)
            {
                //Encaso de que halla algun error muestra un mensaje en pantalla
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario da clic en el boton editar de uno de los aceros de la lista
        /// Consulta la información del acero seleccioando y la muestra en pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAceros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                //identifica el acero seleccionado
                int.TryParse(gvAceros.DataKeys[e.NewEditIndex].Value.ToString(), out int _idAcero);
                //Crea una conexion a la base de datos
                using (PPMEntities db = new PPMEntities())
                {
                    //Busca el acero en la base de datos
                    var item = db.EstatusAceros.Where(_ => _.id == _idAcero).FirstOrDefault();
                    //Valida si se encontro el acero
                    if (item != null)
                    {
                        //Establece los valores del acero encontrado en los controles de la interface de usuario
                        hdnAcero_id.Value = item.id.ToString();
                        txtAcero_Responsable.Text = item.Responsable.Trim();
                        txtAcero_Rollo.Text = item.Rollo;
                        txtAcero_Hora.Text = item.Hora;
                    }
                    //Actualiza la lista de aceros indicando el indice y pagina que se estan editando.
                    load_aceros(-1, e.NewEditIndex, gvAceros.PageIndex);
                    //Muestra la información del acero seleccionado en pantalla.
                    mdlAceros.Show();
                }
            }
            catch (Exception ex)
            {
                //Si ocurre algun error muestra un mensaje al usuario con el texto del error
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        /// <summary>
        /// Metodo que se ejecuta cuando el usuario da clic en el boton Guardar durante la edicion o agregado de un acero.
        /// Alamcena, en base de datos, la información capturada por el usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAcero_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Identifica el acero que se esta editando para saber si es nuevo o un registro existente
                int.TryParse(hdnAcero_id.Value, out int _idAcero);
                //Valida que el usuario halla capturado un responsable
                if (txtAcero_Responsable.Text.Trim() == "")
                {
                    //Si el usuario no capturo un responsable establece el foco en el control indicado y envia un mensaje de error al usuario para que capture el dato.
                    txtAcero_Responsable.Focus();
                    throw new Exception("Captura un responsable");
                }
                //Valida que el usuario halla capturado un acero
                if (txtAcero_Rollo.Text.Trim() == "")
                {
                    //Si el usuario no capturo un acero establece el foco en el control indicado y envia un mensaje de error al usuario para que capture el dato.
                    txtAcero_Rollo.Focus();
                    throw new Exception("Captura un acero");
                }
                //Valida que el usuario halla capturado una hora
                if (!TimeSpan.TryParse(txtAcero_Hora.Text, out TimeSpan _hora))
                {
                    //Si el usuario no capturo una hora valida establece el foco en el control indicado y envia un mensaje de error al usuario para que capture el dato.
                    txtAcero_Hora.Focus();
                    throw new Exception("Captura una hora valida");
                }
                //Valida que el usuario halla capturado una fecha
                if (DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _fecha))
                {
                    //Establece una conexion a la base de datos 
                    using (PPMEntities db = new PPMEntities())
                    {
                        //Busca en base de datos el acero identificado
                        var item = db.EstatusAceros.Where(_ => _.id == _idAcero).FirstOrDefault();
                        //Si no encuentra el registro del acero en base de datos, crea uno nuevo con el valor de fecha por default
                        item = item != null ? item : new EstatusAceros() { Fecha = _fecha };
                        //Asigna al registro el valor de responsable capturado por el usuario
                        item.Responsable = txtAcero_Responsable.Text.Trim();
                        //Asigna al registro el valor de acero capturado por el usuario
                        item.Rollo = txtAcero_Rollo.Text.Trim();
                        //Asigna al registro el valor de hora capturado por el usuario
                        item.Hora = _hora.ToString("hh\\:mm");
                        //Asigna al registro el valor de usuario segun el usuario logeado en windows.
                        item.usuario_captura = User.Identity.Name;
                        //Asgina al registro el valor de fecha hora actual
                        item.fecha_captura = DateTime.Now;
                        //Si el registro es existente establece el estatus como actualizado, sino lo establece como agregado
                        db.Entry(item).State = item.id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
                        //Guarda en base de datos la captura del usuario.
                        db.SaveChanges();
                    }
                    //Actualiza la información de aceros en pantalla segun la captura del usuario.
                    load_aceros(-1, -1, gvAceros.PageIndex);
                }
                else
                {
                    //Si el usuario no capturo una fecha establece el foco en el control indicado y envia un mensaje de error al usuario para que capture el dato.
                    alerta("Fecha no valida");
                    txtFecha.Focus();
                }
            }
            catch (Exception ex)
            {
                //Si ocurre algun error muestra un mensaje al usuario con el texto del error
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
                mdlAceros.Show();
            }
        }

        /// <summary>
        /// Evento de selecciona sin utilizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvEstatus_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        /// <summary>
        /// Evento que se dispara cuando el usuario selecciona un registro de la lista de aceros.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAceros_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Muestra la lista de aceros en pantalla
            load_aceros(e.NewSelectedIndex, -1, gvAceros.PageIndex);
        }
    }
}