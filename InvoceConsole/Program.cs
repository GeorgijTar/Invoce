using InvoceDb;
using System;
using System.Linq;

namespace InvoceConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var db = new InvoceDbContext();
           int t= db.Status.Count();

            Console.WriteLine(t);

            Console.ReadKey();

        }
    }
}
