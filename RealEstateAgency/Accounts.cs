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
    
    public partial class Accounts
    {
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Passport { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public System.DateTime DateBrth { get; set; }
        public System.Guid UserRole { get; set; }
        public System.Guid UserStatus { get; set; }
        public System.Guid Author { get; set; }
        public System.DateTime CD { get; set; }
    }
}