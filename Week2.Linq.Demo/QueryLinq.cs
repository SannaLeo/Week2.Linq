using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2.Linq.Demo
{
    internal class QueryLinq
    {
        #region

        static List<Valutazione> valutazioni = new()
        {
            new Valutazione
            {
                NomeStudente = "Mirko",
                DataValutazione = new DateTime(2022, 5, 5),
                Materia = Materia.Italiano,
                Voto = 7,
            },
            new Valutazione
            {
                NomeStudente = "Giorgio",
                DataValutazione = new DateTime(2022, 4, 29),
                Materia = Materia.Matematica,
                Voto = 8
            },
            new Valutazione
            {
                NomeStudente = "Mirko",
                DataValutazione = DateTime.Now,
                Materia = Materia.Matematica,
                Voto = 10
            },
            new Valutazione
            {
                NomeStudente = "Antonia",
                DataValutazione = DateTime.Now,
                Materia = Materia.Matematica,
                Voto = 10
            },
            new Valutazione
            {
                NomeStudente = "Pietro",
                DataValutazione = new DateTime(2022, 5, 2),
                Materia = Materia.Geografia,
                Voto = 3
            }
        };

        #endregion

        public static void EsecuzioneQuery()
        {
            //Tutte le valutazione di mirko
            //Senza linq
            List<Valutazione> list = new List<Valutazione>();
            foreach(Valutazione valutazione in valutazioni)
            {
                if(valutazione.NomeStudente == "Mirko")
                {
                    list.Add(valutazione);
                    Console.WriteLine(valutazione);
                }
            }

            //Con linq
            //query expression
            IEnumerable<Valutazione> listMirkoQuery = 
                from valutazione in list
                where valutazione.NomeStudente == "Mirko"
                select valutazione;


            //Con linq
            //con metodi linq e delegati
            IEnumerable<Valutazione> listMirkoDelegate = valutazioni.Where(ValutazioniMirko);
            StampaLista<Valutazione>(listMirkoDelegate);

            //Con linq
            //con lamda exp
            IEnumerable<Valutazione> listMirkoLambda = valutazioni.Where(v => v.NomeStudente == "Mirko");

            Console.Clear();
            //Selezioniamo tutte le valutazioni di italiano odinate per data e per nome studente (crescente)
            //Query exp
            IEnumerable<Valutazione> votiItaExp = 
                from valutazione in valutazioni
                where valutazione.Materia == Materia.Italiano
                orderby valutazione.DataValutazione, valutazione.NomeStudente
                select valutazione;

            //Selezioniamo tutte le valutazioni di italiano odinate per data e per nome studente (crescente)
            //LambdaExp
            IEnumerable<Valutazione> votiItaLambda = valutazioni
                .Where(v => v.Materia == Materia.Italiano)
                .OrderBy(val => val.DataValutazione).ThenByDescending(val => val.NomeStudente);

            StampaLista<Valutazione>(votiItaLambda);

            Console.Clear();

            //selezioni nomi e voticon valutazione insufficente

            var votiInsuff =
                from voto in valutazioni
                where voto.Voto < 6
                select new { Nome = voto.NomeStudente, Voto = voto.Voto };

            foreach (var voto in votiInsuff)
            {
                Console.WriteLine($"nome:\t\t{voto.Nome}\t\tvoto:\t\t{voto.Voto}"); 
            }


            //media dei voti per studente
            //Out:  Nome, Media
            //query exp

            var mediaVotiStudQExp =
                from voto in valutazioni
                group voto by voto.NomeStudente into grp
                select new
                {
                    NomeStudente = grp.Key, //chiave di raggruppamento
                    MediaVoti = grp.Average(v => v.Voto)
                };


            foreach (var voto in mediaVotiStudQExp)
            {
                Console.WriteLine($"nome:\t\t{voto.NomeStudente}\t\tvoto:\t\t{voto.MediaVoti}");
            }

            Console.WriteLine("-----------------------------------------------------------");

            var mediavotiLamba = valutazioni.GroupBy(v => v.NomeStudente,
                (key, grp) => new
                {
                    NomeStudente = key,
                    MediaVoti = grp.Average(v => v.Voto)
                });

            foreach (var voto in mediavotiLamba)
            {
                Console.WriteLine($"nome:\t\t{voto.NomeStudente}\t\tvoto:\t\t{voto.MediaVoti}");
            }
        }

        public static bool ValutazioniMirko(Valutazione v)
        {
            return v.NomeStudente == "Mirko";
        }

        public static void StampaLista<T>(IEnumerable<T> lista ) where T : class
        {
            foreach(T valutazione in lista)
            {
                Console.WriteLine(valutazione);
            }
        }


    }
}
