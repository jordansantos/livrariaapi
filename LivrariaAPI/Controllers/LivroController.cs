using LivrariaAPI.Models;
using LivrariaAPI.Persistense;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
    public async Task<ActionResult<IEnumerable<Livro>>> BuscarLivros()
    {
      var livros = await _context.Livros.ToListAsync();
      return Ok(livros);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Livro>> BurcarLivroPorId(int id)
    {
      var livro = await _context.Livros.FindAsync(id);

      if (livro == null)
      {
        return NotFound();
      }

      return Ok(livro);
    }

    [HttpPost]
    public async Task<ActionResult> InserirLivro([FromBody] Livro livro)
    {
      var livros = await _context.Livros.OrderBy(it => it.Id).ToListAsync();
      var ultimoItemLista = livros.Last();
      
        var livroInsert = new Livro { Id = ultimoItemLista.Id + 1, Nome = livro.Nome, Categoria = livro.Categoria, Preco = livro.Preco, imagem = livro.imagem };
      _context.Livros.Add(livro);
      
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> AtualizaLivro(int id, [FromBody] Livro livro)
    {
      var livroBase = await _context.Livros.FindAsync(id);

      if (livroBase == null)
      {
        return NotFound();
      }

      if (livro.Nome != null && livro.Nome != livroBase.Nome)
      {
        livroBase.Nome = livro.Nome;
      }

      if (livro.Preco != 0 && livro.Preco != livroBase.Preco)
      {
        livroBase.Preco = livro.Preco;
      }

      if (livro.Quantidade != 0 && livro.Quantidade != livroBase.Quantidade)
      {
        livroBase.Quantidade = livro.Quantidade;
      }

      if (livro.Categoria != null && livro.Categoria != livroBase.Categoria)
      {
        livroBase.Categoria = livro.Categoria;
      }

      if (livro.imagem != null && livro.imagem != livroBase.imagem)
      {
        livroBase.imagem = livro.imagem;
      }

      _context.Livros.Update(livroBase);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletaLivro(int id)
    {
      var livro = await _context.Livros.FindAsync(id);

      if (livro == null)
      {
        return NotFound();
      }

      _context.Livros.Remove(livro);
      await _context.SaveChangesAsync();
      return NoContent();
    }

    public void PopulateLivros()
    {
      var livros = _context.Livros;

      if (livros.Count() == 0)
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
}
