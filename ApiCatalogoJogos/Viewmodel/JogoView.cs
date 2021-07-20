using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Viewmodel
{
    public class JogoView
    {
        public Guid Id { get; set; }

        public String Nome { get; set; }

        public String Datalancamento { get; set; }

        public double Preco { get; set; }
    }
}
