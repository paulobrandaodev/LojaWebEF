using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LojaWebEF.Dados;
using LojaWebEF.Models;

namespace PrimeiroEF.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        Cliente cliente = new Cliente();
        readonly LojaContexto contexto;

        public ClienteController(LojaContexto contexto){
            this.contexto = contexto;
        }

        [HttpGet]
        public IEnumerable<Cliente> Listar(){
            return contexto.Cliente.ToList();
        }

        [HttpGet("{id}")]
        public Cliente Listar(int id){
            return contexto.Cliente.Where( x => x.IdCliente == id).FirstOrDefault();
        }

        [HttpPost]
        public void Cadastrar([FromBody] Cliente cli){
            contexto.Cliente.Add(cli);
            contexto.SaveChanges();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id,[FromBody] Cliente cliente){
            if(cliente == null || cliente.IdCliente != id){
                return BadRequest();
            }
            var cli = contexto.Cliente.FirstOrDefault(x => x.IdCliente == id);
            if(cli == null){
                return NotFound();
            }

            cli.IdCliente    = cliente.IdCliente;
            cli.Nome         = cliente.Nome;
            cli.Email        = cliente.Email;
            cli.Idade        = cliente.Idade;

            contexto.Cliente.Update(cli);
            int rs = contexto.SaveChanges();

            if(rs > 0)
            return Ok();
            else
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public IActionResult Apagar(int id){
            var cliente = contexto.Cliente.Where( x => x.IdCliente == id).FirstOrDefault();
            if(cliente == null){
                return NotFound();
            }

            contexto.Cliente.Remove(cliente);    
            int rs = contexto.SaveChanges();

            if(rs > 0)
            return Ok();
            else
            return BadRequest();
        }
        

    }
}