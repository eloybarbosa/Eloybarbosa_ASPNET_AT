using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eloybarbosa_ASPNET_AT.Models
{
    public class Aniversariante
    {
        public int Id { get; set; }

        public String Nome { get; set; }

        public String Sobrenome { get; set; }

        public DateTime Nascimento { get; set; }


        public int CalculoProximoAniversario()
        {
            DateTime today = DateTime.Today;
            DateTime niver = new DateTime(today.Year, Nascimento.Month, Nascimento.Day);

            if (niver < today)
            {
                niver = niver.AddYears(1);
            }

            int diasRestantes = (niver - today).Days;
            return diasRestantes;
        }
    }

    
}
