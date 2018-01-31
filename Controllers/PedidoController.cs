using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LojaWebEF.Dados;
using LojaWebEF.Models;

namespace PrimeiroEF.Controllers
{
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        Produto produto = new Produto();
        readonly LojaContexto contexto;

        public PedidoController(LojaContexto contexto){
            this.contexto = contexto;
        }

        [HttpGet]
        public IEnumerable<Pedido> Listar(){
            return contexto.Pedido.ToList();
        }

        [HttpGet("{id}")]
        public Pedido Listar(int id){
            return contexto.Pedido.Where( x => x.IdPedido == id).FirstOrDefault();
        }

        [HttpPost]
        public void Cadastrar([FromBody] Pedido ped){
            contexto.Pedido.Add(ped);
            contexto.SaveChanges();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id,[FromBody] Pedido pedido){
            if(pedido == null || pedido.IdPedido != id){
                return BadRequest();
            }
            var ped = contexto.Pedido.FirstOrDefault(x => x.IdProduto == id);
            if(ped == null){
                return NotFound();
            }

            ped.IdPedido   = pedido.IdPedido;
            ped.IdCliente  = pedido.IdPedido;
            ped.IdProduto  = pedido.IdProduto;
            ped.Quantidade = pedido.Quantidade; 

            contexto.Pedido.Update(ped);
            int rs = contexto.SaveChanges();

            if(rs > 0)
            return Ok();
            else
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public IActionResult Apagar(int id){
            var pedido = contexto.Pedido.Where( x => x.IdPedido == id).FirstOrDefault();
            if(pedido == null){
                return NotFound();
            }

            contexto.Pedido.Remove(pedido);    
            int rs = contexto.SaveChanges();

            if(rs > 0)
            return Ok();
            else
            return BadRequest();
        }
        

    }
}