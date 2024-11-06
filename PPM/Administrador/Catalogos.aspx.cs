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
            ///Valida si la pagina no se esta cargando por algun evento disparado por el usuario
            if (!Page.IsPostBack)
            {
                try
                {
                    //Establece una nueva conexion de base de datos 
                    using (PPMEntities db = new PPMEntities())
                    {
                        //Verifica los permisos del usuario, en caso de que sea administrador habilita la pestaña para administrar los catalogos.
                        var user = db.administradores.Where(_ => _.UserName == HttpContext.Current.User.Identity.Name.Trim()).FirstOrDefault();
                        //Valida si el usuario existe y esta activo
                        if (user == null || !user.activo)
                        {
                            //En caso de que el usuario no exista o no este activo redirecciona al usuario a la pagina default del sistema.
                            Response.Redirect("~/Default.aspx", true);
                        }
                    }
                }
                catch (Exception)
                {
                    //Si ocurre algun error durante la validacion del usuario el sistema redirecciona a la pagina default del sistema para no comprometer la seguridad.
                    Response.Redirect("~/Default.aspx", true);
                }
                //Se manda llamar el evento de buscar administradores para mostrar inicialmente la lista de administradores por default
                lnkAdministradorSearch_Click(null, null);
                //Se manda llamar el evento de busqueda de prensas para mostrar inicialmente la lista de prensas por default
                lnkPrensaSearch_Click(null, null);
            }
        }

        /// <summary>
        /// Metodo para mostrar mensajes en la interface de usuario.
        /// </summary>
        /// <param name="mensaje"></param>
        public void alerta(string mensaje)
        {
            mensaje = mensaje.Replace(Environment.NewLine, "").Replace(Environment.NewLine, "").Replace(Environment.NewLine, "").Replace(Environment.NewLine, "").Replace(Environment.NewLine, "").Replace(Environment.NewLine, "").Replace(Environment.NewLine, "");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), DateTime.Now.ToString(), "alert('" + mensaje.Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "") + "');", true);
        }

        /// <summary>
        /// Actualiza la lista de administradores en pantalla
        /// </summary>
        /// <param name="selectedIndex"></param>
        /// <param name="editIndex"></param>
        /// <param name="pageIndex"></param>
        protected void load_registros(int selectedIndex, int editIndex, int pageIndex)
        {
            //Asigna el valor capturado por el usuario en el campo buscar a una variable de tipo string
            string search = txtAdministradorSearch.Text.Trim();
            //Establece una nueva conexion de base de datos 
            using (PPMEntities db = new PPMEntities())
            {
                //Busca en base de datos la lista de administradores en los que el campo UserName coincida con lo capturado por el usuario en el campo buscar y los muestra en pantalla.
                gvAdministradores.DataSource = db.administradores.Where(_ => _.UserName.Contains(search) || _.nombre.Contains(search)).OrderBy(_ => _.nombre).ToList();
                gvAdministradores.SelectedIndex = selectedIndex;
                gvAdministradores.EditIndex = editIndex;
                gvAdministradores.PageIndex = pageIndex;
                gvAdministradores.DataBind();
            }
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario da clic en el boton de buscar de la seccion de administradores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAdministradorSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //Actualiza la lista de administradores en pantalla
                load_registros(-1, -1, 0);
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario da clic en el boton agregar de la lista de administradores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAdministrador_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                //Se habilita el campo UserName de la sección de captura de administradores.
                txtAdministrador_Username.Enabled = true;
                //Se establece en blanco el campo UserName de la sección de captura de administradores
                txtAdministrador_Username.Text = "";
                //Se establece en blanco el campo Nombre de la sección de captura de administradores
                txtAdministrador_Nombre.Text = "";
                //Se establece en falso el campo Activo de la sección de captura de administradores
                chkAdministrador_Activo.Checked = false;
                //Se muestra en pantalla la sección de captura de administradores.
                mdlAdministrador.Show();
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario da clic en el boton eliminar de algun registro de la lista de administradores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAdministradores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //Se asigna a una variable de tipo strin el identificador del administrador seleccionado.
                string _username = gvAdministradores.DataKeys[e.RowIndex].Value.ToString();
                //Establece una nueva conexion de base de datos 
                using (PPMEntities db = new PPMEntities())
                {
                    //Buscaen base de datos el administrador seleccionado.
                    var item = db.administradores.Where(_ => _.UserName == _username).FirstOrDefault();
                    //Valida si se encontro el administrador seleccionado.
                    if (item != null)
                    {
                        //En caso de que se halla encontrado el administrador seleccionado se le establece un valor de eliminado al registro.
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        //Se guardan los cambios en base de datos.
                        db.SaveChanges();
                    }
                    //Se actualiza la lista de administradores ya sin el administrador eliminado
                    load_registros(-1, -1, 0);
                }
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario da clic en el boton editar de la lista de administradores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAdministradores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                //Se asigna aun variable de tipo string el identificador del administrador seleccionado.
                string _username = gvAdministradores.DataKeys[e.NewEditIndex].Value.ToString();
                //Establece una nueva conexion de base de datos 
                using (PPMEntities db = new PPMEntities())
                {
                    //Busca en base de datos el administrador seleccionado.
                    var item = db.administradores.Where(_ => _.UserName == _username).FirstOrDefault();
                    //Valida si se encontro el administrador seleccionado en base de datos.
                    if (item != null)
                    {
                        //Establece el valor de UserName del registro de administrador en el campo necesario
                        txtAdministrador_Username.Text = item.UserName.Trim();
                        //Establece el valor de Nombre del registro de administrador en el campo necesario
                        txtAdministrador_Nombre.Text = item.nombre.Trim();
                        //Establece el valor de Activo del registro de administrador en el campo necesario
                        chkAdministrador_Activo.Checked = item.activo;
                    }
                    //Actualiza la listad de administradores indicando el indice y pagina seleccionados por el usuario.
                    load_registros(-1, e.NewEditIndex, gvAdministradores.PageIndex);
                    //Deshabilita el campo UserName del registro de administrador para que no se pueda repetir la captura.
                    txtAdministrador_Username.Enabled = false;
                    //Muestra en pantalla los datos del administrador seleccionado.
                    mdlAdministrador.Show();
                }
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario da clic en el boton guardar de la sección de captura de administrador.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdministrador_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida si el usuario capturó el valor UserNmae en el registro de administrador
                if (txtAdministrador_Username.Text.Trim() == "")
                {
                    //Muestra un mensaje en pantalla indicando al usuario que capture el dato de username
                    alerta("Captura username");
                    //Establece el foco en el control para captura de UserNmae
                    txtAdministrador_Username.Focus();
                    //Mantiene la pantalla para captura de administrador
                    mdlAdministrador.Show();
                    //Termina el evento sin guardar la info
                    return;
                }
                if (txtAdministrador_Nombre.Text.Trim() == "")
                {
                    alerta("Captura nombre");
                    //Establece el foco en el control para captura de UserNmae
                    txtAdministrador_Nombre.Focus();
                    //Mantiene la pantalla para captura de administrador
                    mdlAdministrador.Show();
                    //Termina el evento sin guardar la info
                    return;
                }
                //Establece una nueva conexion de base de datos 
                using (PPMEntities db = new PPMEntities())
                {
                    //Buscar en base de datos los datos del administraro para verificar si esta repetido o no
                    var item = db.administradores.Where(_ => _.UserName == txtAdministrador_Username.Text.Trim()).FirstOrDefault();
                    bool existe = item != null;
                    //Si no existe el administrador en base de datos crea un nuevo registro de administrador con el username capturado
                    item = item != null ? item : new administradores() { UserName = txtAdministrador_Username.Text.Trim() };
                    //Asigna el nombre del registro de administrador capturado por el usuario.
                    item.nombre = txtAdministrador_Nombre.Text.Trim();
                    //Asigna el estaus activo al registro, este dato tambien es capturado por el usuario.
                    item.activo = chkAdministrador_Activo.Checked;
                    //Si el registro ya existia estable el estatus del registro en modificado, en caso contraro lo establece en agregado.
                    db.Entry(item).State = existe ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
                    //Guarda los cambios en base de datos.
                    db.SaveChanges();
                }
                //Actualiza la lista de administradores en pantalla.
                load_registros(-1, -1, gvAdministradores.PageIndex);
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
                //Mantiene en pantalla la seccion de captura/edicion de administrador.
                mdlAdministrador.Show();
            }
        }

        //Actualiza el listado de prensas en pantalla
        protected void load_prensas(int selectedIndex, int editIndex, int pageIndex)
        {
            string search = txtPrensaSearch.Text.Trim();
            //Establece una nueva conexion de base de datos 
            using (PPMEntities db = new PPMEntities())
            {
                //Consulta la lista de prensas en las que el campo nombre conincide con la busqueda capturada por el usuario y la ordena por id, despues las muestra en pantalla
                gvPrensas.DataSource = db.Prensas.Where(_ => _.nombre.Contains(search)).OrderBy(_ => _.id).ToList();
                gvPrensas.SelectedIndex = selectedIndex;
                gvPrensas.EditIndex = editIndex;
                gvPrensas.PageIndex = pageIndex;
                gvPrensas.DataBind();
            }
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario da clic en el boton buscar de la sección de prensas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkPrensaSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //Actualiza la lista de prensas en pantalla.
                load_prensas(-1, -1, 0);
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario da clic en el boton agregar de la sección de prensas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkPrensa_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                //Habilita el campo Id para captura
                txtPrensa_Id.Enabled = true;
                txtPrensa_Id.ReadOnly = false;
                //Establece el campo Id de la Prensa en blanco
                txtPrensa_Id.Text = "";
                //Establece el campo nombre de la prensa en blanco
                txtPrensa_Nombre.Text = "";
                //txtPrensa_Descripcion.Text = "";
                chkPrensa_Activo.Checked = false;
                //Muestra en pantalla la sección para captura de un nuevo registro de prensa.
                mdlPrensa.Show();
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario da clic en un boton de eliminar en la lista de prensas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPrensas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //Identifica la prensa seleccionada
                int.TryParse(gvPrensas.DataKeys[e.RowIndex].Value.ToString(), out int _idPrensa);
                //Establece una nueva conexion de base de datos 
                using (PPMEntities db = new PPMEntities())
                {
                    //Busca en base de datos la prensa seleccionada
                    var item = db.Prensas.Where(_ => _.id == _idPrensa).FirstOrDefault();
                    //Valida si la prensas seleccionada se encuentra en base de datos
                    if (item != null)
                    {
                        //Si se encuentra la prensas le establece al registro un valor de eliminado
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        //Guarda los cambios en base de datos.
                        db.SaveChanges();
                    }
                    //Actualiza la lista de prensas en pantalla, ya sin la prensa eliminada
                    load_prensas(-1, -1, 0);
                }
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        /// <summary>
        /// Evento que se dispara cuando un usuario da clic en un boton editar de la lista de registros de prensas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPrensas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                //Identifica la prensas seleccionada
                int.TryParse(gvPrensas.DataKeys[e.NewEditIndex].Value.ToString(), out int _idPrensa);
                //Establece una nueva conexion de base de datos 
                using (PPMEntities db = new PPMEntities())
                {
                    //Busca en base de datos la prensa seleccionada
                    var item = db.Prensas.Where(_ => _.id == _idPrensa).FirstOrDefault();
                    //Valida si la prensa se encontro en base de datos.
                    if (item != null)
                    {
                        //Habilita el campo id de prensa para captura
                        txtPrensa_Id.Enabled = false;
                        txtPrensa_Id.ReadOnly = true;
                        //Establece el campo de id de prensa en blanco
                        txtPrensa_Id.Text = item.id.ToString();
                        //Establece el campo nombre de la prensa en blanco
                        txtPrensa_Nombre.Text = item.nombre.Trim();
                        //txtPrensa_Descripcion.Text = item.descripcion.Trim();
                        chkPrensa_Activo.Checked = item.activo;
                    }
                    //Actualiza la lista de prensas en pantalla con el indice y la pagina seleccionados por el usuario.
                    load_prensas(-1, e.NewEditIndex, gvPrensas.PageIndex);
                    //Muestra en pantalla la sección para editar el registro de prensa seleccionado.
                    mdlPrensa.Show();
                }
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
            }
        }

        /// <summary>
        /// Evento que se dispara cuando un usuario da clic en el boton guardar de la sección de captura de prensas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrensa_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida si el usuario capturo el campo nombre para la prensa.
                if (txtPrensa_Nombre.Text.Trim() == "")
                    //En caso que el usuario no halla capturado un nombre para la prensas se muestra un mensaje de error indicando al usuario que es necesario captura el nombre de la prensa.
                    throw new Exception("Captura el nombre de la Prensa");

                //Valida si el usuario capturo un id para la prensa o si hay un prensa seleccionada previamente
                int.TryParse(txtPrensa_Id.Text, out int _idPrensa);
                if (_idPrensa > 0)
                {
                    //Establece una nueva conexion de base de datos 
                    using (PPMEntities db = new PPMEntities())
                    {
                        //Busca la prensa en base de datos segnu el id capturado o seleccionado.
                        var item = db.Prensas.Where(_ => _.id == _idPrensa).FirstOrDefault();
                        //Si no se encuentra la prensas en base de datos se crea un nuevo registro de prensa
                        item = item != null ? item : new Prensas() { };
                        //Establece los valores de la prensa.
                        item.id = _idPrensa;
                        item.nombre = txtPrensa_Nombre.Text.Trim();
                        //item.descripcion = txtPrensa_Descripcion.Text.Trim();
                        item.activo = chkPrensa_Activo.Checked;
                        //item.fecha_captura = DateTime.Now;
                        //item.usuario_captura = User.Identity.Name;
                        //Si la prensa ya existia previamente establece el estado del registro en modificado, en caso contraro lo establece en agregado.
                        db.Entry(item).State = !txtPrensa_Id.Enabled ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
                        //Guarda los cambios en base de datos.
                        db.SaveChanges();
                    }
                    //Actualiza la lista de prensas en pantalla.
                    load_prensas(-1, -1, gvPrensas.PageIndex);
                }
                else
                    //En caso de que el usuario no halla capturado o seleccionado correctametne un id de prensa se muestra un mensaje de error indicando al usuario que de capturar un id de pantalla
                    throw new Exception("Captura el Id de la prensa");
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.InnerException == null ? ex.Message : (ex.InnerException.InnerException == null ? ex.InnerException.Message : ex.InnerException.InnerException.Message));
                //Mantiene en pantalla la sección de captura de prensa
                mdlPrensa.Show();
            }
        }

        /// <summary>
        /// Evento que se dispara automaticamente cuando el usuario selecciona un registro de prensa de la sección de prensas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPrensas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Identifica la prensa seleccionda por el usuario.
            txtPrensa_Id.Text = gvPrensas.DataKeys[e.NewSelectedIndex].Value.ToString();
            //Actualiza la lista de prensas para que sea notoria la prensa seleccionada.
            load_prensas(e.NewSelectedIndex, -1, gvPrensas.PageIndex);
        }
    }
}