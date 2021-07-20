using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.Entides;

namespace ApiCatalogoJogos.Repositories
{
    public interface Ijogo_Repository
    {
        Task<List<Jogo_Entides>> Obter(int pagina, int quantidade);
        Task<Jogo_Entides> Obter(Guid id);
        Task<List<Jogo_Entides>> Obter(String jogo, String Datalancamento);
        Task Inserir(Jogo_Entides jogo);
        Task Atualizar(Jogo_Entides jogo);
        Task Remover(Guid id);
        void Dispose();
    }
}
