using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;

namespace NHibernateTest
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Aggiungo una tupla alla tabella del db partendo da una istanza dell'oggetto");

            // Create an object that will be inserted inside table
            Product p = new Product
            {
                Name = "Prodotto 02",
                Category = "Software"
                // Id is an incremental integer
            };

            Console.Write(p.Name + " " + p.Category + " " + p.Id);

            // Save object to table
            try
            {
                using (ISession session = OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(p);
                        transaction.Commit();
                    }
                    Console.WriteLine("Object is saved to db");
                }
            }catch(Exception e){
                Console.WriteLine(e);
            }


            Console.WriteLine("Vado a leggere dal db le tuple");

            // Read Table
            try
            {
                using (ISession session = OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        // Execute Query
                        IQuery query = session.CreateQuery("FROM Product");
                        IList<Product> products = query.List<Product>();

                        Console.WriteLine("Num Prodotti presenti: " + products.Count);
                        products.ToList().ForEach(prod => Console.WriteLine(prod.Id + " -- " + prod.Name + " -- " + prod.Category));
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }



            Console.ReadLine();
            

        }


        /*
            NHibernate has six basic APIs:
            - ISession : is the interface for handle session
            - ISessionFactory : we need to create one instance per database access
            - IConfiguration : configure NHibernate 
            - ITransaction : is used to control transactional state of operations
            - IQuery : is used inside session and allows to do queries
            - ICriteria : allows you to do object-oriented queries
         */
        static ISessionFactory SessionFactory;
        static ISession OpenSession()
        {
            if (SessionFactory == null)
            {
                Configuration configuration = new Configuration();

                Assembly thisAssembly = typeof(Product).Assembly;
                configuration.AddAssembly(thisAssembly);

                //configuration.AddAssembly(Assembly.GetCallingAssembly());
                SessionFactory = configuration.BuildSessionFactory();
            }
            return SessionFactory.OpenSession();
        }
    }
}
