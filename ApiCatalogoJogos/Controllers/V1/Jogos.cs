using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.Inputmodel;
using ApiCatalogoJogos.Viewmodel;
using ApiCatalogoJogos.Serv;
using ApiCatalogoJogos.Exceptions;
using ExemploApiCatalogoJogos.Exceptions;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/V1[controller]")]
    [ApiController]
    public class Jogos : ControllerBase
    {
        private readonly Ijogo _jogo;

        public Jogos(Ijogo jogo)
        {
            _jogo = jogo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoView>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogo.Obter(pagina, quantidade);

            if (jogos.Count() == 0)
                return NotFound("Jogo não existente");

            return Ok(jogos);
        }
        [HttpGet("{idjogo}")]
        public async Task<ActionResult<JogoView>> Obter([FromRoute] Guid idjogo)
        {
            var jogos = await _jogo.Obter(idjogo);
            if (jogos == null)
            return NotFound("Não existe este jogo");

            return Ok(jogos);
        }

        [HttpPost]
        public async Task<ActionResult<JogoView>> inserirjogo([FromBody] Input Jogo)
        {
            try
            {
                var jogo = await _jogo.Inserir(Jogo);

                return Ok(Jogo);
            }

            catch(JogoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este data de lançamento");
            }
        }

        [HttpPut("{idjogo:guid}")]

        public async Task<ActionResult> Atualizarjogo([FromRoute] Guid idJogo, [FromBody] Input Input)
            {
                try
                {
                    await _jogo.Atualizar(idJogo, Input);

                    return Ok();
                }

                catch (JogoNaoCadastrado ex)
                {
                    return NotFound("Não existe este jogo");
                }
            }

        [HttpPatch("{idjogo}")]
        public async Task<ActionResult> Atualizarjogo([FromRoute] Guid idjogo, [FromBody] Double preco)
        {
            try
            {
                await _jogo.Atualizar(idjogo, preco);

                return Ok();
            }

            catch (JogoNaoCadastrado ex)
            {
                return NotFound("Não existe este jogo");
            }
        }
        [HttpDelete("{idjogo:guid}")]

        public async Task<ActionResult> Removerjogo([FromRoute] Guid idjogo)
        {
            try
            {
                await _jogo.Remover(idjogo);

                return Ok();
            }

            catch (JogoNaoCadastrado ex)
            {
                return NotFound("Não existe este jogo");
            }
        }
    }


}
