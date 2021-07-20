using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.Inputmodel;
using ApiCatalogoJogos.Viewmodel;

namespace ApiCatalogoJogos.Serv
{
    public interface Ijogo
    {
        Task<List<JogoView>> Obter(int pagina, int quantidade);
        Task<JogoView> Obter(Guid id);
        Task<JogoView> Inserir(Input jogo);
        Task Atualizar(Guid id, Input jogo);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}
