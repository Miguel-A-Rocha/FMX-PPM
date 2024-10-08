//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PPM.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Programa
    {
        public int id { get; set; }
        public System.DateTime Fecha { get; set; }
        public Nullable<System.TimeSpan> Hora { get; set; }
        public int PrensaId { get; set; }
        public Nullable<int> Secuencia { get; set; }
        public string NoParte { get; set; }
        public int EstatusId { get; set; }
        public Nullable<System.DateTime> FechaHora { get; set; }
        public string usuario_captura { get; set; }
        public System.DateTime fecha_captura { get; set; }
        public Nullable<double> cantidad_programada { get; set; }
        public Nullable<double> cantidad_corrida { get; set; }
        public int EstatusPrensaId { get; set; }
    
        public virtual Estatus Estatus { get; set; }
        public virtual EstatusPrensa EstatusPrensa { get; set; }
        public virtual Prensas Prensas { get; set; }
    }
}
