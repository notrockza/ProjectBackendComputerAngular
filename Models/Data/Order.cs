﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace BackendComputer.Models.Data
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
            Payments = new HashSet<Payments>();
            Transport = new HashSet<Transport>();
        }

        public int Id { get; set; }
        public int? OrderUser { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? Total { get; set; }
        public int? OrderStatus { get; set; }

        public virtual User OrderUserNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
        public virtual ICollection<Transport> Transport { get; set; }
    }
}