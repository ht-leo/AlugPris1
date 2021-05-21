using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlugPris1.Models
{
    public class imovel
    {
        [Key]
        public int Codigo { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public string Detalhes { get; set; }
        public string Anunciante { get; set; }
        public string Telefone { get; set; }
    }
}