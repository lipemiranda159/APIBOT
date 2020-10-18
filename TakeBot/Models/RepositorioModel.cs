using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeAPI.Models
{
    public class RepositorioModel
    {
        public string Imagem { get; set; }
        public string NomeCompleto { get; set; }
        public string DescricaoRepositorio { get; set; }
        public DateTime DataCriacao { get; set; }
        public string linguagemRepositorio { get; set; }
    }
}
