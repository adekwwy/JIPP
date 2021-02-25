using System;
using System.Data.Entity;
using System.Linq;

namespace WindowsFormsApp1
{
    public class Context : DbContext
    {
        public Context()
            : base("name=ContactBook")
        {
        }

         public virtual DbSet<Contact> Contacts { get; set; }
    }
}