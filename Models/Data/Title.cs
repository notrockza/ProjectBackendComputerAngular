// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace BackendComputer.Models.Data
{
    public partial class Title
    {
        public Title()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string TitleName { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}