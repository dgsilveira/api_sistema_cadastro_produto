using Microsoft.AspNetCore.Mvc;
using ProdutoAPI.Data;
using ProdutoAPI.Data.Dto;
using ProdutoAPI.Models;
using System;
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
        public IActionResult AdicionaProduto([FromBody] CreateProdutoDto createProdutoDto)
        {
            var produto = new Produto{ Descricao = createProdutoDto.Descricao};
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
                var readProdutoDto = new ReadProdutoDto
                {
                    Id = produto.Id,
                    Descricao = produto.Descricao,
                    HoraDaConsulta = DateTime.Now
                };
                return Ok(readProdutoDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaProduto(int id, [FromBody] UpdateProdutoDto updateProdutoDto)
        {
            var produto = _context.Produtos.
                FirstOrDefault(produto => produto.Id == id);
            if(produto == null)
            {
                return NotFound();
            }
            produto.Descricao = updateProdutoDto.Descricao;
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
