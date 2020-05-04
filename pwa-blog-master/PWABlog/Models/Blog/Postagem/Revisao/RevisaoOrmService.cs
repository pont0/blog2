using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.Models.Blog.Postagem.Revisao
{
    public class RevisaoOrmService
    {

        private readonly DatabaseContext _databaseContext;
        public RevisaoOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }




        public RevisaoEntity ObterRevisao()
        {
            return _databaseContext.Revisao.Find(idRevisao);
        }
    }



}
