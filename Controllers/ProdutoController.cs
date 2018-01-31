using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LojaWebEF.Dados;
using LojaWebEF.Models;

namespace PrimeiroEF.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        Produto produto = new Produto();
        readonly LojaContexto contexto;

        public ProdutoController(LojaContexto contexto){
            this.contexto = contexto;
        }

        [HttpGet]
        public IEnumerable<Produto> Listar(){
            return contexto.Produto.ToList();
        }

        [HttpGet("{id}")]
        public Produto Listar(int id){
            return contexto.Produto.Where( x => x.IdProduto == id).FirstOrDefault();
        }

        [HttpPost]
        public void Cadastrar([FromBody] Produto prod){
            contexto.Produto.Add(prod);
            contexto.SaveChanges();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id,[FromBody] Produto produto){
            if(produto == null || produto.IdProduto != id){
                return BadRequest();
            }
            var prod = contexto.Produto.FirstOrDefault(x => x.IdProduto == id);
            if(prod == null){
                return NotFound();
            }

            prod.IdProduto   = produto.IdProduto;
            prod.NomeProduto = produto.NomeProduto;
            prod.Descricao   = produto.Descricao;
            prod.Preco       = produto.Preco;
            prod.Quantidade  = produto.Quantidade;

            contexto.Produto.Update(prod);
            int rs = contexto.SaveChanges();

            if(rs > 0)
            return Ok();
            else
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public IActionResult Apagar(int id){
            var produto = contexto.Produto.Where( x => x.IdProduto == id).FirstOrDefault();
            if(produto == null){
                return NotFound();
            }

            contexto.Produto.Remove(produto);    
            int rs = contexto.SaveChanges();

            if(rs > 0)
            return Ok();
            else
            return BadRequest();
        }
        

    }
}