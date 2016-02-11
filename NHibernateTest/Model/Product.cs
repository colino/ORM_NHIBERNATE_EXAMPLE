using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateTest
{
    public class Product
    {
        virtual public string Name { get; set; }
        virtual public string Category { get; set; }
        //public bool Discontinued { get; set; }
        virtual public int Id { get; set; }

        // Definisco la chiave primaria
        //public Guid Id { get; set; }
    }
}
