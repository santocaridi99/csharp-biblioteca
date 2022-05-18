//si vuole progettare un sistema per la gestione di una biblioteca.
//gli utenti registrati al sistema, fornendo cognome, nome, email, password, recapito telefonico,
//possono effettuare dei prestiti sui documenti che sono di vario tipo (libri, dvd).
//i documenti sono caratterizzati da un codice identificativo di tipo stringa (isbn per i libri, numero seriale per i dvd), titolo, anno, settore(storia, matematica, economia, …),
//stato(in prestito, disponibile), uno scaffale in cui è posizionato, un elenco di autori (nome, cognome).
//per i libri si ha in aggiunta il numero di pagine, mentre per i dvd la durata.
//l’utente deve poter eseguire delle ricerche per codice o per titolo e, eventualmente, effettuare dei prestiti registrando il periodo (dal/al) del prestito e il documento.
//il sistema per ogni prestito determina un numero progressivo di tipo alfanumerico.
//deve essere possibile effettuare la ricerca dei prestiti dato nome e cognome di un utente.
//segno_spunta_bianco
//occhi
//mani_alzate
using System;
namespace csharp_biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //biblioteca
            biblioteca b = new biblioteca("Biblioteca Comunale");
            //scaffale
            scaffale scaffale = new scaffale("S001");
            //libri
            libro l1 = new libro("i1i1i1i1", "Libro misterioso", 2009, "Storia", 220);
            autore a1 = new autore("Nome 1", "Cognome 1");
            l1.Autori.Add(a1);
            b.Documenti.Add(l1);
            l1.Scaffale = scaffale;

            dvd dvd1 = new dvd("Codice1", "Titolo 3", 2019, "Storia", 130);
            b.Documenti.Add(dvd1);
           
            utente u1 = new utente("Ciro", "Immobile", "Telefono 1", "Email 1", "Password 1");

            b.Utenti.Add(u1);

            prestito p1 = new prestito("P00001", new DateTime(2022, 5, 18), new DateTime(2022, 5, 19), u1, l1);

            b.Prestiti.Add(p1);

            Console.WriteLine("\n\nSearchByCodice: ISBN1\n\n");

            List<documento> results = b.SearchByCodice("ISBN1");

            foreach (documento doc in results)
            {
                Console.WriteLine(doc.ToString());

                if (doc.Autori.Count > 0)
                {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Autori");
                    Console.WriteLine("--------------------------");
                    foreach (autore a in doc.Autori)
                    {
                        Console.WriteLine(a.ToString());
                        Console.WriteLine("--------------------------");
                    }
                }
            }

            Console.WriteLine("\n\nSearchPrestiti per : Nome 1, Cognome 1\n\n");

            List<prestito> prestiti = b.SearchPrestiti("Ciro", "Immobile");

            foreach (prestito p in prestiti)
            {
                Console.WriteLine(p.ToString());
                Console.WriteLine("--------------------------");
            }


        }
    }
}
