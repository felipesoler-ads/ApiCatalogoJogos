using System;


namespace ApiCatalogoJogos.Exceptions
{
    public class JogoNaoCadastrado : Exception
    {
        public JogoNaoCadastrado()
            : base("Este jogo não foi cadastrado anteriormente")
        { }
    }
}
