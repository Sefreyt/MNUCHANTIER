using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Configuration;

namespace Models
{
    public class CompagnieContext : DbContext
    {
        public DbSet<Compagnie> Compagnies { get; set; }
        public DbSet<Contrat> Contrats { get; set; }
        public DbSet<Element> Elements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
        }
    }

    public class Compagnie
    {
        public int CompagnieID { get; set; }
        public string Nom { get; set; }

        public List<Contrat> Contrats { get; set; }
    }

    public class Contrat
    {
        public int ContratID { get; set; }
        public string Nom { get; set; }
        public double MtnSoumission { get; set; }

        public int CompagnieID { get; set; }
        public Compagnie Compagnie { get; set; }
    }

    public class Element
    {
        public int ElementID { get; set; }
        public string Nom { get; set; }
        public DateTime DateFabrication { get; set; }

        public int ContratID { get; set; }
        public Contrat Contrat { get; set; }
    }
}
