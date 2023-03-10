// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace BackendComputer.Models.Data
{
    public partial class Products
    {
        public Products()
        {
            DetailsProducts = new HashSet<DetailsProducts>();
            OrderDetail = new HashSet<OrderDetail>();
            Stock = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int? IdType { get; set; }
        public double? ProductPrice { get; set; }
        public int? PdStock { get; set; }
        public string Image { get; set; }
        public string ProductDetail { get; set; }
        public string DetailSpecifics { get; set; }

        public virtual Type IdTypeNavigation { get; set; }
        public virtual ICollection<DetailsProducts> DetailsProducts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<Stock> Stock { get; set; }
    }
}