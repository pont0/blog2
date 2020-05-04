using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.Models.Blog.Postagem.Comentario
{
    public class ComentarioOrmService
    {

        private readonly DatabaseContext _databaseContext;
        public ComentarioOrmService(DatabaseContext databaseContext)
            {
                _databaseContext = databaseContext;
            }

            


        public List<ComentarioEntity> ObterComentario()
            {
                return _databaseContext.Comentario
                    .Include(p => p.Categoria)
                    .Include(p => p.Revisoes)
                    .Include(p => p.Comentarios)
                    .ToList();
            }
    }


    
}
