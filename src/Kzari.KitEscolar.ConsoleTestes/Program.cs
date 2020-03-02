using Kzari.KitEscolar.Infra.Data.DbContexts;
using Kzari.KitEscolar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kzari.KitEscolar.ConsoleTestes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var opt = new DbContextOptionsBuilder<MEContext>();
            opt.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MaterialEscolarDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            using (var contexto = new MEContext(opt.Options))
            {
                var prod1 = new Produto { Id = 1, Nome = "novo nome" };

                contexto.Entry(prod1).State = EntityState.Modified;
                contexto.SaveChanges();
            }
        }
    }
}
