using Microsoft.EntityFrameworkCore;
using ProdutoAPI.Models;

namespace ProdutoAPI.Data
{
    public class ProdutoContext : DbContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> opt) : base(opt)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
    }
}
