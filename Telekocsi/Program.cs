using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace Telekocsi
{
    class autokAdat
    {
        public string Indulas { get; private set; }
        public string Cel { get; private set; }
        public string Rendszam { get; private set; }
        public string Telefonszam { get; private set; }
        public int Ferohely { get; private set; }
        public autokAdat(string sor)
        {
            string[] elemek = sor.Split(';');
            Indulas = elemek[0];
            Cel = elemek[1];
            Rendszam = elemek[2];
            Telefonszam = elemek[3];
            Ferohely = int.Parse(elemek[4]);
        }
    }
    class igenyekAdat
    {
        public string Azonosito { get; private set; }
        public string Indulas { get; private set; }
        public string Cel { get; private set; }
        public int Szemelyek { get; private set; }
        public igenyekAdat(string sor)
        {
            string[] elemek = sor.Split(';');
            Azonosito = elemek[0];
            Indulas = elemek[1];
            Cel = elemek[2];
            Szemelyek = int.Parse(elemek[3]);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<autokAdat> autok = new List<autokAdat>();
            foreach (var sor in File.ReadAllLines("autok.csv").Skip(1))
            {
                autok.Add(new autokAdat(sor));
            }

            List<igenyekAdat> igenyek = new List<igenyekAdat>();
            foreach (var sor in File.ReadAllLines("igenyek.csv").Skip(1))
            {
                igenyek.Add(new igenyekAdat(sor));
            }
            Console.WriteLine("2.Feladat");
            Console.WriteLine($"{autok.Count()} autós hirdet fuvar");
            int bpToMis = 0;
            for (int i = 0; i < autok.Count; i++)
            {
                if (autok[i].Indulas == "Budapest" && autok[i].Cel == "Miskolc")
                {
                    bpToMis++;
                }
            }
            Console.WriteLine("\n3.Feladat");
            Console.WriteLine($"Összesen {bpToMis} férőhelyet hirdettek az autósok Budapestről Miskolcra");

            Console.WriteLine("\n4.Feladat");

            int kulonbozoelemekszama = 0;
            string[] indcel = new string[1000];
            int[] ferohelyosszesen = new int[1000];
            int j;
            for (int i = 0; i < 1000; i++) ferohelyosszesen[i] = 0;
            for (int i = 0; i < autok.Count; i++)
            {
                j = 0;
                while ((j <= kulonbozoelemekszama) && (autok[i].Indulas + "-" + autok[i].Cel !=
               indcel[j]))
                {
                    j++;
                }
                if (j > kulonbozoelemekszama)
                {
                    kulonbozoelemekszama++;
                    indcel[kulonbozoelemekszama] = autok[i].Indulas + "-" + autok[i].Cel;
                }
            }

            for (int i = 0; i < autok.Count; i++)
            {
                for (int k = 0; k < kulonbozoelemekszama; k++)
                {
                    if (indcel[k] == autok[i].Indulas + "-" + autok[i].Cel)
                    {
                        ferohelyosszesen[k] += autok[i].Ferohely;
                    }
                }
            }

            int max = ferohelyosszesen[0];
            int maxi = 0;
            for (int k = 0; k < kulonbozoelemekszama; k++)
            {
                if (ferohelyosszesen[k] > max)
                {
                    max = ferohelyosszesen[k];
                    maxi = k;
                }
            }
            Console.WriteLine($"A  legtöbb  férőhelyet  ({ferohelyosszesen[maxi]}-t) a {indcel[maxi]})  a  {indcel[maxi]} útvonalonajánlották fel a hirdetők");
            Console.ReadKey();
        }
    }
}
