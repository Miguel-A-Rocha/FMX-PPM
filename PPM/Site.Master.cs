using PPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPM
{
    public partial class Site : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Evento automatico - carga inicial de pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ///Muestra el nombre de usuario que esta actualmente con una sesión iniciado en windows.
            lnkAdministrador.Text = HttpContext.Current.User.Identity.Name;
            //Establece una conexion a la base de datos
            using (PPMEntities db = new PPMEntities())
            {
                //Verifica los permisos del usuario, en caso de que sea administrador habilita la pestaña para administrar los catalogos.
                var user = db.administradores.Where(_ => _.UserName == HttpContext.Current.User.Identity.Name.Trim()).FirstOrDefault();
                //Valida si el usuario existe y esta activo
                if (user == null || !user.activo)
                {
                    //En caso de que el usuario exita o no este activo deshabilta la funcionalidad de administrador
                    liAdministrador.Visible = false;
                }
            }
        }
    }
}