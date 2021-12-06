using LivrariaAPI.Models;
using LivrariaAPI.Persistense;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaAPI.Controllers
{
  [ApiController]
  [Route("api/livro")]
  public class LivroController : ControllerBase
  {
    private readonly LivrariaContext _context;

    public LivroController(LivrariaContext context)
    {
      _context = context;
      PopulateLivros();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Livro>>> GetLivro()
    {
      
      var livros = await _context.Livros.ToListAsync();
      return Ok(livros);
    }

    public void PopulateLivros()
    {
      _context.Livros.Add(new Livro { Id = 1, Nome = "Livro 1", Categoria = "Categoria 1", Preco = 25.5, Quantidade = 2, imagem = "img1" });
      _context.Livros.Add(new Livro { Id = 2, Nome = "Livro 2", Categoria = "Categoria 2", Preco = 20, Quantidade = 2, imagem = "img2" });
      _context.Livros.Add(new Livro { Id = 3, Nome = "Livro 3", Categoria = "Categoria 1", Preco = 28.7, Quantidade = 2, imagem = "img3" });
      _context.Livros.Add(new Livro { Id = 4, Nome = "Livro 4", Categoria = "Categoria 1", Preco = 30, Quantidade = 2, imagem = "img4" });
      _context.Livros.Add(new Livro { Id = 5, Nome = "Livro 5", Categoria = "Categoria 2", Preco = 32.75, Quantidade = 2, imagem = "img5" });

      _context.SaveChanges();
    }
  }
}
