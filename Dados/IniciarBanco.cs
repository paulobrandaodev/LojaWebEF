using System.Linq;
using LojaWebEF.Models;

namespace LojaWebEF.Dados
{
    public class IniciarBanco
    {
        public static void Inicializar(LojaContexto contexto){
            contexto.Database.EnsureCreated();
            if(contexto.Cliente.Any()){
                return;
            }
            var cliente = new Cliente()
                {Nome="Pedro",Email="pedro@terra.com.br",Idade=23};
                contexto.Cliente.Add(cliente);

            var produto = new Produto()
                {NomeProduto="Mouse",Descricao="Mouse Microsoft",Preco=20,Quantidade=10};
                contexto.Produto.Add(produto);

            var pedido = new Pedido()
                {IdCliente=cliente.IdCliente, IdProduto=produto.IdProduto,Quantidade=1};
                contexto.Pedido.Add(pedido);

            contexto.SaveChanges();        
        }
    }
}