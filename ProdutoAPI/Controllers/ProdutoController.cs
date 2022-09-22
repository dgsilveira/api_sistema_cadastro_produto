using Microsoft.AspNetCore.Mvc;
using ProdutoAPI.Data;
using ProdutoAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProdutoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private ProdutoContext _context;

        public ProdutoController(ProdutoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionaProduto([FromBody] Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaProdutoPorId), new { Id = produto.Id }, produto);
        }

        [HttpGet]
        public IEnumerable<Produto> RecuperaProduto() => _context.Produtos;

        [HttpGet("{id}")]
        public IActionResult RecuperaProdutoPorId(int id)
        {
            var produto = _context.Produtos.
                FirstOrDefault(produto => produto.Id == id);
            if (produto != null)
            {
                return Ok(produto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaProduto(int id, [FromBody] Produto produtoNovo)
        {
            var produto = _context.Produtos.
                FirstOrDefault(produto => produto.Id == id);
            if(produto == null)
            {
                return NotFound();
            }
            produto.Descricao = produtoNovo.Descricao;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaProduto(int id)
        {
            var produto = _context.Produtos.
                FirstOrDefault(produto => produto.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            _context.Remove(produto);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
