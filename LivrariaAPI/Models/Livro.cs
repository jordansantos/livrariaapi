namespace LivrariaAPI.Models
{
  public class Livro
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public double Preco { get; set; }
    public int Quantidade { get; set; }
    public string Categoria { get; set; }
    public string imagem { get; set; }
  }
}
