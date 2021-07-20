using ApiCatalogoJogos.Entides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.Repositories;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoRepository : Ijogo_Repository
    {
        private static Dictionary<Guid, Jogo_Entides> jogos = new Dictionary<Guid, Jogo_Entides>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Jogo_Entides{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Fifa 21", Datalancamento = "09-11-2005", Preco = 60} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Jogo_Entides{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Fifa 20", Datalancamento ="30-12-2001", Preco = 70} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Jogo_Entides{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Fifa 19", Datalancamento = "20-01-1995", Preco = 90} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Jogo_Entides{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Fifa 18", Datalancamento = "17-08-2010", Preco = 10} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Jogo_Entides{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Street Fighter V", Datalancamento = "14-01-1997", Preco = 1000} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Jogo_Entides{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "Grand Theft Auto V", Datalancamento = "22-05-1995", Preco = 2000} }
        };

        public Task<List<Jogo_Entides>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo_Entides> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return Task.FromResult<Jogo_Entides>(null);

            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo_Entides>> Obter(string nome, String Datalancamento)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Datalancamento.Equals(Datalancamento)).ToList());
        }

        public Task<List<Jogo_Entides>> ObterSemLambda(string nome, String Datalancamento)
        {
            var retorno = new List<Jogo_Entides>();

            foreach (var jogo in jogos.Values)
            {
                if (jogo.Nome.Equals(nome) && jogo.Datalancamento.Equals(Datalancamento))
                    retorno.Add(jogo);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Jogo_Entides jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task Atualizar(Jogo_Entides jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}

