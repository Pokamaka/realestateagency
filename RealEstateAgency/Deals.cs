//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RealEstateAgency
{
    using System;
    using System.Collections.Generic;
    
    public partial class Deals
    {
        public System.Guid ID { get; set; }
        public int Number { get; set; }
        public System.Guid UserID { get; set; }
        public System.Guid AgentID { get; set; }
        public System.Guid ObjectID { get; set; }
        public System.Guid StatusID { get; set; }
        public double Summ { get; set; }
        public double Procent { get; set; }
        public System.DateTime DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public System.DateTime CD { get; set; }
    }
}
