namespace PWABlog.RequestModels.AdminPostagens
{
    public class AdminPostagensCriarRequestModel
    {
        public string Nome { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AutorEntity Autor { get; set; }
        public CategoriaEntity Categoria { get; set; }
    }
}