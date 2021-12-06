using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Persistense
{
  public class LivrariaContext : DbContext
  {
    public LivrariaContext(DbContextOptions<LivrariaContext> options) : base(options)
    {

    }

    public DbSet<Livro> Livros { get; set; }
  }
}
