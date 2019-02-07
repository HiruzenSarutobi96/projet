using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easy_transport
{
    class Program
    {
        public class Constante
        {
            public const int poidsMaxCar = 500;
            public const int maxClient = 15;
            public const int poidsMaxPers = 50;
            public const int ageMin = 7;
            public const int prixtiket717 = 3000;
            public const int prixtiket1835 = 3500;
            public const int prixtiket36 = 4000;
            public const int prixbagage110 = 300;
            public const int prixbagage1130 = 800;
            public const int prixbagage3150 = 1500;
        }
        public struct Client
        {
            public Int16 codeClient;
            public Int16 ageClient;
            public Int16 poidBagage;
            public Int16 totalClient;

            public Client(short code, short age, short poid,short total)
            {
                codeClient = code;
                ageClient = age;
                poidBagage = poid;
                totalClient = total;
            }
        }

        static bool verif_bagage(Client[] tabclient, short poid, short tailleTab)
        {
            int totalPoids = 0;
            bool rep;
            Int16 answer;
            if (tailleTab == 0)
            {
                rep = true;
            }
            else
            {
                for (int i = 0; i < tailleTab; i++)
                {
                    totalPoids += tabclient[i].poidBagage;
                }
                totalPoids += poid;
                if (totalPoids <= Constante.poidsMaxCar)
                {
                    rep = true;
                }
                else
                {
                    do
                    {
                        Console.WriteLine("Poids Trop eleve");
                        Console.WriteLine("que souhaitez vous faire");
                        Console.WriteLine("1: Sortir");
                        Console.WriteLine("2: Voyager sans bagage");
                        answer = Convert.ToInt16(Console.ReadLine());
                    } while (answer < 1 || answer > 2);
                    if (answer == 1)
                    {
                        rep = false;
                    }
                    else
                    {
                        poid = 0;
                        rep = true;
                    }
                }
            }
            return rep;
        }


        static Client new_client(Client[] tableauDeClient, Int16 tailleTab, ref bool reponse)
        {
            Client client;
            client.totalClient = 0;
            int ans;
            Console.WriteLine("Nouveau Voyageur");
            Console.Write("Code Voyageur : ");
            client.codeClient = Convert.ToInt16(Console.ReadLine());
            do
            {
                Console.Write("Age du voyageur : ");
                client.ageClient = Convert.ToInt16(Console.ReadLine());
            } while (client.ageClient < Constante.ageMin);

            if (client.ageClient >= 7 || client.ageClient <= 17)
            {
                client.totalClient += Constante.prixtiket717;
            }
            else if (client.ageClient >= 18 || client.ageClient <= 35)
            {
                client.totalClient += Constante.prixtiket36;
            }
            else if (client.ageClient >= 36)
            {
                client.totalClient += Constante.prixbagage3150;
            }

            do
            {
                Console.WriteLine("Avez vous des bagages");
                Console.WriteLine("1: Oui");
                Console.WriteLine("2: Non");
                ans = Convert.ToInt16(Console.ReadLine());
            } while (ans < 1 || ans > 2);
            if (ans == 1)
            {
                do
                {
                    Console.WriteLine("Entrer le poids des bagages en KG.(Vous avez droit a 50kg max)");
                    client.poidBagage = Convert.ToInt16(Console.ReadLine());
                } while (client.poidBagage > Constante.poidsMaxPers || client.poidBagage < 0);
                reponse = verif_bagage(tableauDeClient, client.poidBagage, tailleTab);

                if (client.poidBagage >= 1 || client.poidBagage <= 10)
                {
                    client.totalClient += Constante.prixbagage110;
                }else if(client.poidBagage >= 11 || client.poidBagage <= 30)
                {
                    client.totalClient += Constante.prixbagage1130;
                }else if(client.poidBagage >= 31 || client.poidBagage <= 50)
                {
                    client.totalClient += Constante.prixbagage3150;
                }
            }
            else
            {
                client.poidBagage = 0;
                client.totalClient += 0;
            }
            return client;
        }

        static void Main(string[] args)
        {
            bool rep = false;
            Int16 i = 0;
            Int16 total = 0;
            Client[] tableauDeClient = new Client[Constante.maxClient];
            while(i < Constante.maxClient)
            {
                tableauDeClient[i] = new_client(tableauDeClient, i,ref rep);
                Console.WriteLine("la reponse du client {0}",rep);
                if(rep == true)
                {
                    Console.WriteLine("Le total pour ce Client est {0} f", tableauDeClient[i].totalClient);
                    total += tableauDeClient[i].totalClient;
                    i++;
                }
            }
            Console.WriteLine("le Total pour se voyage est {0}", total);
            Console.ReadKey();
        }
    }
}
