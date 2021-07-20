using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.Inputmodel;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.Serv;
using ApiCatalogoJogos.Viewmodel;
using ApiCatalogoJogos.Entides;
using ExemploApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.Exceptions;

namespace ApiCatalogoJogos.Serv
{
    public class Jogo_Service : Ijogo
    {
        private readonly Ijogo_Repository _jogo_Repository;

        public Jogo_Service(Ijogo_Repository jogoRepository)
        {
            _jogo_Repository = jogoRepository;
        }

        public async Task<List<JogoView>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogo_Repository.Obter(pagina, quantidade);

            return jogos.Select(jogo => new JogoView
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Datalancamento = jogo.Datalancamento,
                Preco = jogo.Preco
            })
                .ToList();
        }

        public async Task<JogoView> Obter(Guid id)
        {
            var jogo = await _jogo_Repository.Obter(id);

            if (jogo == null)
                return null;

            return new JogoView
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Datalancamento = jogo.Datalancamento,
                Preco = jogo.Preco
            };
        }

        public async Task<JogoView> Inserir(Input jogo)
        {
            var entidadeJogo = await _jogo_Repository.Obter(jogo.Nome, jogo.Datalancamento);

            if (entidadeJogo.Count > 0)
                throw new JogoJaCadastradoException();

            var jogoInsert = new Jogo_Entides
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Datalancamento = jogo.Datalancamento,
                Preco = jogo.Preco
            };

            await _jogo_Repository.Inserir(jogoInsert);

            return new JogoView
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Datalancamento = jogo.Datalancamento,
                Preco = jogo.Preco
            };
        }

        public async Task Atualizar(Guid id, Input jogo)
        {
            var entidadeJogo = await _jogo_Repository.Obter(id);

            if (entidadeJogo == null)
            throw new JogoNaoCadastrado();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Datalancamento = jogo.Datalancamento;
            entidadeJogo.Preco = jogo.Preco;

            await _jogo_Repository.Atualizar(entidadeJogo);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _jogo_Repository.Obter(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastrado();

            entidadeJogo.Preco = preco;

            await _jogo_Repository.Atualizar(entidadeJogo);
        }

        public async Task Remover(Guid id)
        {
            var jogo = await _jogo_Repository.Obter(id);

            if (jogo == null)
                throw new JogoNaoCadastrado();

            await _jogo_Repository.Remover(id);
        }

        public void Dispose()
        {
            _jogo_Repository?.Dispose();
        }
    }
}
