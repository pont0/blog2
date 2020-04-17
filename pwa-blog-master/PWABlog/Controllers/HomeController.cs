﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PWABlog.Models;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.Models.Blog.Postagem;
using PWABlog.Models.Blog.Postagem.Revisao;
using PWABlog.ViewModels.Home;

namespace PWABlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoriaOrmService _categoriaOrmService;
        private readonly Models.Blog.Postagem.PostagemOrmService _postagemOrmService;
        private readonly Models.Blog.Postagem.Comentario.ComentarioOrmService _comentarioOrmService;
        private readonly Models.Blog.Postagem.Revisao.RevisaoOrmService _revisaoOrmService;
        private readonly Models.Blog.Autor.AutorOrmService _autorOrmService;
        private readonly Models.Blog.Etiqueta.EtiquetaOrmService _etiquetaOrmService;
        private readonly Models.Blog.Postagem.Classificacao.ClassificacaoOrmService _classificacaoOrmService;


        public HomeController(
            ILogger<HomeController> logger,
            CategoriaOrmService categoriaOrmService,
            PostagemOrmService postagemOrmService,
            ComentarioOrmService comentarioOrmService,
            RevisaoOrmService revisaoOrmService,
            AutorOrmService autorOrmService,
            EtiquetaOrmService etiquetaOrmService,
            ClassificacaoOrmService classificacaoOrmService

        )
        {
            _logger = logger;
            _categoriaOrmService = categoriaOrmService;
            _postagemOrmService = postagemOrmService;
            _comentarioOrmService = comentarioOrmService;
            _revisaoOrmService = revisaoOrmService;
            _autorOrmService = autorOrmService;
            _etiquetaOrmService = etiquetaOrmService;
            _classificacaoOrmService = classificacaoOrmService;



        }



        public IActionResult Index()
        {   
            // Instanciar a ViewModel
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.TituloPagina = "Página Home";
            
            // Alimentar a lista de postagens que serão exibidas na view
            List<PostagemEntity> listaPostagens = _postagemOrmService.ObterPostagens();
            
            foreach (PostagemEntity postagem in listaPostagens)
            {
                PostagemHomeIndex postagemHomeIndex = new PostagemHomeIndex();
                postagemHomeIndex.Titulo = postagem.Titulo;
                postagemHomeIndex.Descricao = postagem.Descricao;
                postagemHomeIndex.Categoria = postagem.Categoria.Nome;
                postagemHomeIndex.NumeroComentarios = postagem.Comentarios.Count.ToString();
                postagemHomeIndex.PostagemId = postagem.Id.ToString();
                
                // Obter última revisão
                RevisaoEntity ultimaRevisao = postagem.Revisoes.OrderByDescending(o => o.DataCriacao).FirstOrDefault();
                if (ultimaRevisao != null)
                {
                    postagemHomeIndex.Data = ultimaRevisao.DataCriacao.ToLongDateString();
                }
                
                model.Postagens.Add(postagemHomeIndex);
            }

            // Alimentar a lista de categorias que serão exibidas na view
            List<CategoriaEntity> listaCategorias = _categoriaOrmService.ObterCategorias();

            foreach (CategoriaEntity categoria in listaCategorias)
            {
                CategoriaHomeIndex categoriaHomeIndex = new CategoriaHomeIndex();
                categoriaHomeIndex.Nome = categoria.Nome;
                categoriaHomeIndex.CategoriaId = categoria.Id.ToString();
                
                model.Categorias.Add(categoriaHomeIndex);
            
                // Alimentar a lista de etiquetas que serão exibidas na view, a partir das etiquetas da categoria
                foreach (EtiquetaEntity etiqueta in categoria.Etiquetas)
                {
                    EtiquetaHomeIndex etiquetaHomeIndex = new EtiquetaHomeIndex();
                    etiquetaHomeIndex.Nome = etiqueta.Nome;
                    etiquetaHomeIndex.EtiquetaId = etiqueta.Id.ToString();
                
                    model.Etiquetas.Add(etiquetaHomeIndex);
                }
            }


            // Alimentar a lista de postagens populares que serão exibidas na view

            List<PostagemEntity> listaPostagensPopulares = _postagemOrmService.ObterPostagensPopulares();
            foreach (PostagemEntity postagemPopular in listaPostagensPopulares)
            {
                PostagemPopularHomeIndex postagemPopularHomeIndex = new PostagemPopularHomeIndex();
                postagemPopularHomeIndex.Titulo = postagemPopular.Titulo;
                postagemPopularHomeIndex.PostagemId = postagemPopular.Id;
                postagemPopularHomeIndex.Categoria = postagemPopular.Categoria.Nome;

                model.PostagensPopulares.Add(postagemPopularHomeIndex);
            }


            // TODO Obter lista de postagens populares


            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}