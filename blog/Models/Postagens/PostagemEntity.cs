using blog.Models.Autor;
using blog.Models.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Postagens
{
    public class PostagemEntity
    {
        public string Titulo { get; set; }
        public AutorEntity Autor { get; set; }

        public CategoriaEntity Categoria { get; set; }
    }
}
