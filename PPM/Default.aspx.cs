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
        /// <summary>
        /// Evento automatico - carga inicial de pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ///Valida si la pagina no se esta cargando por algun evento disparado por el usuario
            if (!Page.IsPostBack)
            {
                ///Establece la fecha actual en el campo fecha
                txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
                ///Limpia de todos los valores la lista desplegable de estatus de prensa.
                lstEstatusPrensa.Items.Clear();
                //Establece una conexion a la base de datos
                using (PPMEntities db = new PPMEntities())
                {
                    //Consulta el catalogo de estatus de prensas y lo itera para agregr 1 a 1 en la lista desplegable
                    foreach (var item in db.EstatusPrensa.ToList())
                    {
                        //Agrega en la lista desplegable el estatus de la prensa de la iteracion acutal colocando el valor de id en el campo value y el valor de descripción en el campo Text
                        lstEstatusPrensa.Items.Add(new ListItem() { Value = item.id.ToString(), Text = item.descripcion, Enabled = item.activo });
                    }
                }
                //Se ejecuta el evento buscar para llenar la lista de prensas de pantalla inicial
                btnBuscar_Click(null, null);
            }
        }

        /// <summary>
        /// Metodo para enviar mensajes a la interface de usuario
        /// </summary>
        /// <param name="mensaje"></param>
        public void alerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), DateTime.Now.ToString(), "alert('" + mensaje.Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "").Replace("'", "") + "');", true);
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario da clic en el boton Buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Cuando el usuario da clic en el boton buscar se actualizan todos los elmentos de la pantalla.
                //Muestra la lista de prensas en pantalla
                load_prensas(-1, -1, 0);
                //Muestra la lista de aceros en pantalla
                load_aceros(-1, -1, 0);
                //Muestra la lista de estatus en pantalla
                load_estatus(-1, -1, 0);
                //Muestra la lista de estatus de prensa en pantalla
                load_estatus_prensa(-1, -1, 0);
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.Message);
            }
        }

        /// <summary>
        /// Metodo para mostrar en pantalla el listado de prensas
        /// </summary>
        /// <param name="selectedIndex"></param>
        /// <param name="editIndex"></param>
        /// <param name="pageIndex"></param>
        protected void load_prensas(int selectedIndex, int editIndex, int pageIndex)
        {
            //Establece una nueva conexion de base de datos 
            using (PPMEntities db = new PPMEntities())
            {
                //Consulta en base de datos la lista de prensa con el campo activo = true y lo muestra en pantalla
                rptPrensas.DataSource = db.Prensas.Where(_ => _.activo).ToList();
                rptPrensas.DataBind();
            }
        }

        /// <summary>
        /// Metodo para mostrar en pantalla el listado de aceros
        /// </summary>
        /// <param name="selectedIndex"></param>
        /// <param name="editIndex"></param>
        /// <param name="pageIndex"></param>
        protected void load_aceros(int selectedIndex, int editIndex, int pageIndex)
        {
            //Convierte la fecha capturada por el usuario en una campo de tipo fecha hora
            DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha);
            //Establece una nueva conexion de base de datos 
            using (PPMEntities db = new PPMEntities())
            {
                //Consulta en base de datos los registros de estatus de aceros segun la seleccion de fecha del usuario y lo ordena por fecha y despues por hora para mostrarlos en pantalla.
                gvAceros.DataSource = db.EstatusAceros.Where(_ => _.Fecha == _Fecha).OrderBy(_ => _.Fecha).ThenBy(_=>_.Hora).ToList();
                gvAceros.SelectedIndex = selectedIndex;
                gvAceros.EditIndex = editIndex;
                gvAceros.PageIndex = pageIndex;
                gvAceros.DataBind();
            }
        }

        /// <summary>
        /// Metodo para mostrar en pantalla el listado de Estatus
        /// </summary>
        /// <param name="selectedIndex"></param>
        /// <param name="editIndex"></param>
        /// <param name="pageIndex"></param>
        protected void load_estatus(int selectedIndex, int editIndex, int pageIndex)
        {
            //Establece una nueva conexion de base de datos 
            using (PPMEntities db = new PPMEntities())
            {
                //Consulta en base de datos la lista de Estatus con el campo activo = true y lo muestra en pantalla
                gvEstatus.DataSource = db.Estatus.Where(_ => _.activo).ToList();
                gvEstatus.SelectedIndex = selectedIndex;
                gvEstatus.EditIndex = editIndex;
                gvEstatus.PageIndex = pageIndex;
                gvEstatus.DataBind();
            }
        }

        /// <summary>
        /// Metodo para mostrar en pantalla el listado de estatus de las prensas
        /// </summary>
        /// <param name="selectedIndex"></param>
        /// <param name="editIndex"></param>
        /// <param name="pageIndex"></param>
        protected void load_estatus_prensa(int selectedIndex, int editIndex, int pageIndex)
        {
            //Establece una nueva conexion de base de datos 
            using (PPMEntities db = new PPMEntities())
            {
                //Consulta en base de datos la lista de Estatus de Prensa con el campo activo = treu y donde el campo id sea diferente de 0 (cero) y los muestra en pantalla
                gvEstatusPrensa.DataSource = db.EstatusPrensa.Where(_ => _.activo && _.id != 0).ToList();
                gvEstatusPrensa.SelectedIndex = selectedIndex;
                gvEstatusPrensa.EditIndex = editIndex;
                gvEstatusPrensa.PageIndex = pageIndex;
                gvEstatusPrensa.DataBind();
            }
        }

        /// <summary>
        /// Metodo que se ejecuta automaticamente cuando se pinta en pantlla un Registro del Listado de Prensas
        /// Muestra el programa de cada prensa listada en la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptPrensas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Convierte el el texto del campo fecha capturado por el usuario a una variable de tipo fecha hora
            DateTime.TryParse(txtFecha.Text.Trim(), out DateTime _Fecha);
            //Valida si el registro que se esta pintando en pantalla es de tipo Item o AlternatingItem
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Obtiene los datos del registro que se esta creando en pantalla
                var data = e.Item.DataItem as Prensas;
                //Crea una copia del control gridview que esta dentro del registro que se esta creando en pantalla
                GridView gv = e.Item.FindControl("gvPrograma") as GridView;
                //Valida si se pudo crear la copia del control gridview
                if (gv != null)
                {
                    //Establece una nueva conexion de base de datos 
                    using (PPMEntities db = new PPMEntities())
                    {
                        //Busca en base e datos los registro de Programa con la fecha seleccionada, la prensa del registro que se esta creando en pantalla, lo ordena ascendentemente por el valor de secuencia y muestra la información en pamtalla
                        gv.DataSource = db.Programa.Where(_=>_.Fecha == _Fecha && _.PrensaId == data.id).OrderBy(_=>_.Secuencia).ToList();
                        gv.DataBind();
                    }
                }
            }
        }

        /// <summary>
        /// Metodo que se ejecuta automaticamente para refrescar la pagina cada X tiempo indicado en la propiedad Interval del control Unamed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Unnamed_Tick(object sender, EventArgs e)
        {
            //Se reutilizar evento del boton buscar clic para no escribir tanto codigo y hacerlo mas entendible
            btnBuscar_Click(null, null);
        }

        /// <summary>
        /// Metodo que se dispara cuando un usuario da clic en el boton de acciones de una prensa
        /// Cambia el estatus de la prensa segun la seleccion del usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lstEstatusPrensa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Convierte el valor de estatus prensa seleccionado por el usuario en una variable de tipo numerico entera
                int.TryParse(lstEstatusPrensa.SelectedValue, out int _EstatusPrensaId);
                //Convierte el valor de programa seleccionado por el usuario en una veriable de tipo numerico entero
                int.TryParse(hdnProgramaId.Value.Trim(), out int _ProgramaId);
                //Establece una nueva conexion de base de datos 
                using (PPMEntities db = new PPMEntities())
                {
                    //Busca en base de datos el registro de programa seleccionado por el usuario
                    Programa prgm = db.Programa.Where(_ => _.id == _ProgramaId).FirstOrDefault();
                    //Valida si se encontro el registro de usuario en base de atos
                    if (prgm != null)
                    {
                        //Asgina al registro de programa el nuevo estatus de prensa seleccionada por el usuario.
                        prgm.EstatusPrensaId = _EstatusPrensaId;
                        //Asigna al registro de programa un estatus de modificado
                        db.Entry(prgm).State = System.Data.Entity.EntityState.Modified;
                        //Almacena los cambios en base de datos
                        db.SaveChanges();
                        //Actualiza la lista de prensas en pantalla con la nueva in´formación capturada por el usuario.
                        load_prensas(-1,-1,0);
                        //Reinicia la seleccion de estatus de prensa para que el usuario pueda seleccionarla nuevamente 
                        lstEstatusPrensa.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                //Encaso de que ocurra algun error muestra un mensaje en pantalla con el detalle del error ocurrido
                alerta(ex.Message);
            }
        }
    }
}