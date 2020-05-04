using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.Models.Blog.Postagem.Classificacao
{
    public class ClassificacaoOrmService
    {

        private readonly DatabaseContext _databaseContext;
        public ClassificacaoOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }




        public ClassificacaoEntity ObterClassificacao()
        {
            return _databaseContext.Classificacao.Find(idClassificacao);
        }
    }



}
