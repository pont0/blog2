using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.Models.Blog.Etiqueta
{
    public class EtiquetaOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public EtiquetaOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public EtiquetaEntity ObterEtiqueta()
        {
            return _databaseContext.Etiqueta.Find(idEtiqueta);

        }

        public EtiquetaEntity CriarEtiqueta(string nome, Categoria categoria)
        {
            var novaEtiqueta = new EtiquetaEntity { Nome = nome, Categoria = categoria};
            _databaseContext.Etiquetas.Add(novaEtiqueta);
            _databaseContext.SaveChanges();

            return novaEtiqueta;
        }

        public EtiquetaEntity EditarEtiqueta(int id, string nome)
        {
            var etiqueta = _databaseContext.Etiquetas.Find(id);

            if (etiqueta == null)
            {
                throw new Exception("Etiqueta não encontrada!");
            }

            etiqueta.Nome = nome;
            _databaseContext.SaveChanges();

            return etiqueta;
        }

        public bool RemoverEtiqueta(int id)
        {
            var etiqueta = _databaseContext.Etiquetas.Find(id);

            if (etiqueta == null)
            {
                throw new Exception("etiqueta não encontrada!");
            }

            _databaseContext.Etiquetas.Remove(etiqueta);
            _databaseContext.SaveChanges();

            return true;
        }
    }
}