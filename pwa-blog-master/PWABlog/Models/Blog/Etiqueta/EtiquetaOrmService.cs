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
    }
}