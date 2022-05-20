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
using System.IO;
using System.Configuration;
namespace csharp_biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string publicEnv = Environment.GetEnvironmentVariable("PUBLIC");
            if (publicEnv != null)
            {
                Console.WriteLine("Valore: {0}", publicEnv);

            }

            if (Directory.Exists(publicEnv+"\\biblioteca"))
            {
                Console.WriteLine("Sto in {0}",Directory.GetCurrentDirectory());
                Console.WriteLine("La directory esiste");
                if (File.Exists(publicEnv + "\\biblioteca" + "\\biblioteca.txt"))
                {
                    Console.WriteLine("Directory già esistente");
                    //devo leggere biblioteca.txt che esiste e capire il file
                    //su cui devo lavorareS
                    //StreamReader sr = new StreamReader(publicEnv + "\\biblioteca" + "\\biblioteca.txt");
                    ////Console.WriteLine(sr.ReadToEnd());
                    //sr.Close();
                }
                else
                {
                    Console.WriteLine("Dov'è il file");
                    string sfile = Console.ReadLine();
                    if (sfile == "")
                    {
                        StreamWriter sw = new StreamWriter(publicEnv + "\\biblioteca" + "\\biblioteca.txt");//file è biblioteca.txt
                        sw.WriteLine("Section: fileConf");
                        sw.WriteLine(publicEnv + "\\biblioteca" + "\\biblioteca.txt");
                        sw.Close();
                    }
                    else
                    {
                        StreamWriter sw = new StreamWriter(publicEnv + "\\biblioteca" + "\\biblioteca.txt");// il file è quello che mi ha detto l'utente
                        sw.WriteLine("Section: fileConf");
                        sw.WriteLine(sfile);
                        sw.Close();
                    }

                }

            }
            else
            {
                Directory.CreateDirectory(publicEnv + "\\biblioteca");
                Console.WriteLine("Dove vuoi inserire il file biblioteca");
                string sfile = Console.ReadLine();
                //quando si mette stringa vuota viene autogestito
                if (sfile == "")
                {
                    StreamWriter sw = new StreamWriter(publicEnv + "\\biblioteca" + "\\biblioteca.txt");
                    sw.WriteLine("Section: fileConf");
                    sw.WriteLine(publicEnv + "\\biblioteca" + "\\biblioteca.txt");
                    sw.Close();
                }
                else
                {
                    StreamWriter sw = new StreamWriter(publicEnv + "\\biblioteca" + "\\biblioteca.txt");
                    sw.WriteLine("Section: fileConf");
                    sw.WriteLine(sfile);
                    sw.Close();

                }
            }

            AddUpdateAppSettings("Path", publicEnv + "\\biblioteca" + "\\biblioteca.txt");
            ReadAllSettings();
           

            
           

            //biblioteca
            biblioteca b = new biblioteca("Biblioteca Comunale");


           




            Console.WriteLine("Benvenuti nella nostra biblioteca");
            Console.WriteLine("nome  {0}", b.Nome);
            //scaffale
            scaffale scaffale = new scaffale("S001");
            //libri
            libro l1 = new libro("i1i1i1i1", "Libro misterioso", 2009, "Storia", 220);
            //creo autore
            autore a1 = new autore("Nome 1", "Cognome 1");

            //si aggiunge l'autore alla lista degli autori 
            l1.Autori.Add(a1);
            //si aggiungono i libri alla lista documenti in biblioteca
            b.Documenti.Add(l1);
           
            //aggiungo libro in scaffale 
            l1.Scaffale = scaffale;


            //esempio creazione di un dvd
            dvd dvd1 = new dvd("Codice1", "Titolo 3", 2019, "Storia", 130);
            b.Documenti.Add(dvd1);

            //registra utente
            Console.WriteLine("Inserisci il tuo nome");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci il tuo cognome");
            string cognome = Console.ReadLine();
            Console.WriteLine("Inserisci il tuo telefono");
            string tel = Console.ReadLine();
            Console.WriteLine("Inserisci email");
            string mail = Console.ReadLine();
            Console.WriteLine("Inserisci pass");
            string pass = Console.ReadLine();

            //utente registrato
            utente u1 = new utente(nome, cognome, tel , mail, pass);
            b.Utenti.Add(u1);
            //per prima cosa leggo gli utenti da file
            b.RestoreUtenti(publicEnv + "\\biblioteca" + "\\biblioteca.txt", u1);
           
           
            //prestito utente registrato
            prestito p1 = new prestito("P00001", new DateTime(2022, 5, 18), new DateTime(2022, 5, 19), u1, l1);

            b.Prestiti.Add(p1);

            Console.WriteLine("\n\nfiltro ricerca del codice : i1i1i1i1\n\n");

            List<documento> results = b.SearchByCodice("i1i1i1i1");

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

            // Come ultima istruzione del programma oppure ogni volta che si
            //aggiunge un nuovo utente si salvano gli utenti su file
            b.SaveUtenti(publicEnv + "\\biblioteca" + "\\biblioteca.txt");
        
           

            Console.WriteLine("\n\nSearchPrestiti per : inserire nome , inserire cognome\n\n");

            //inserisci un nome  e cognome di un utente registrato (Inserirli correttamente)
            string inserireNome = Console.ReadLine();
            string inserireCognome = Console.ReadLine();

            //prova ad inserire nome e cognome di utente1
            List<prestito> prestiti = b.SearchPrestiti(inserireNome, inserireCognome);

            foreach (prestito p in prestiti)
            {
                Console.WriteLine(p.ToString());
                Console.WriteLine("--------------------------");
            }

           


        }

        static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

       

        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return "";
            }
        }
        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }




    }




}
