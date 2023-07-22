// See https://aka.ms/new-console-template for more information
using FT.Domain.Models;
using System.Data.Services.Client;

namespace FT.Domain.Services
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, World!");

            var serviceRoot = "https://oda.ft.dk/api/";
            var context = new Container(new Uri(serviceRoot));

            context.SendingRequest2 += (s, e) =>
            {
                Console.WriteLine("{0} {1}", e.RequestMessage.Method, e.RequestMessage.Url);
            };
            //var query = from p in context.Afstemning
            //            where p.vedtaget == false
            //            select p;

            var query = context.Afstemning;

            DataServiceQuery<Afstemning> afstemninger = (DataServiceQuery<Afstemning>)query;
            TaskFactory<IEnumerable<Afstemning>> tf = new();
            var list = tf.FromAsync(afstemninger.BeginExecute(null, null),
                                       iar => afstemninger.EndExecute(iar)).Result;

            foreach (Afstemning o in list)
            {
                Console.WriteLine($"{o.id}, {o.opdateringsdato}");
            }
        }
    }
}