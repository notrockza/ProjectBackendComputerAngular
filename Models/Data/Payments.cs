﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace BackendComputer.Models.Data
{
    public partial class Payments
    {
        public int Id { get; set; }
        public int? IdOrderr { get; set; }
        public DateTime? PayDate { get; set; }
        public string PaySlipimage { get; set; }

        public virtual Order IdOrderrNavigation { get; set; }
    }
}