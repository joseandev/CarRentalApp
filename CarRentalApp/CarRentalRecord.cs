//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRentalApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class CarRentalRecord
    {
        public int id { get; set; }
        public string customerName { get; set; }
        public Nullable<System.DateTime> dateRented { get; set; }
        public Nullable<System.DateTime> dateReturned { get; set; }
        public Nullable<decimal> cost { get; set; }
        public Nullable<int> typeOfCar { get; set; }
    
        public virtual TypesOfCar TypesOfCar { get; set; }
    }
}
