using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlugPris1.Models
{
    public class contexto : DbContext
    {
        public DbSet<imovel> Imovel { get; set; }
    }
}