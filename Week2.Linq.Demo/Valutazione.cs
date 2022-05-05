using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2.Linq.Demo
{
    public enum Materia
    {
        Italiano,
        Storia,
        Geografia,
        Matematica
    }

    public class Valutazione
    {
        public string NomeStudente { get; set; }    
        public DateTime DataValutazione { get; set; }
        public Materia Materia { get; set; }
        public int Voto { get; set; }



        public override string ToString()
        {
            return $"{NomeStudente}\t\t{Materia}\t\t{Voto}\t\t{DataValutazione}";
        }
    }
}
