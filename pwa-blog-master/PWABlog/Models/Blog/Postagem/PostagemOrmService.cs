using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PWABlog.Models.Blog.Postagem
{
    public class PostagemOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public PostagemOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<PostagemEntity> ObterPostagens()
        {
            return _databaseContext.Postagens
                .Include(p => p.Postagem)
                .Include(p => p.Revisoes)
                .Include(p => p.Comentarios)
                .ToList();
        }

        public List<PostagemEntity> ObterPostagensPopulares()
        {
            return _databaseContext.Postagens
                .Include(a => a.Autor)
                .OrderByDescending(c => c.Comentarios.Count)
                .Take(4)
                .ToList();
        }
        public PostagemEntity CriarPostagem(string nome, string titulo, string descricao, AutorEntity autor, CategoriaEntity categoria)
        {
            var novaPostagem = new PostagemEntity { Nome = nome, Titulo = titulo, Descricao = descricao, Autor = autor, Categoria = categoria};
            _databaseContext.Postagens.Add(novaPostagem);
            _databaseContext.SaveChanges();

            return novaPostagem;
        }

        public PostagemEntity EditarPostagem(int id, string nome)
        {
            var postagem = _databaseContext.Postagens.Find(id);

            if (postagem == null)
            {
                throw new Exception("postagem não encontrada!");
            }

            postagem.Nome = nome;
            _databaseContext.SaveChanges();

            return postagem;
        }

        public bool RemoverPostagem(int id)
        {
            var postagem = _databaseContext.Postagens.Find(id);

            if (postagem == null)
            {
                throw new Exception("postagem não encontrada!");
            }

            _databaseContext.Postagens.Remove(postagem);
            _databaseContext.SaveChanges();

            return true;
        }
    }
}