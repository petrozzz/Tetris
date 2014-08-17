using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    class BlockLists
    {
        public static List<Point> olista = new List<Point>();
        public static List<Point> jlista = new List<Point>();
        public static List<Point> llista = new List<Point>();
        public static List<Point> ilista = new List<Point>();
        public static List<Point> slista = new List<Point>();
        public static List<Point> zlista = new List<Point>();
        public static List<Point> tlista = new List<Point>();
        public static List<List<Point>> allaBlockLista = new List<List<Point>>(); //Lista med alla blocklistor!

        public static void AllaBlock()//Lägger alla listor med punkter i en enda stor lista som heter allaBlockLista
        {
            allaBlockLista.Add(olista);
            allaBlockLista.Add(jlista);
            allaBlockLista.Add(llista);
            allaBlockLista.Add(ilista);
            allaBlockLista.Add(slista);
            allaBlockLista.Add(zlista);
            allaBlockLista.Add(tlista);
        }

        public static void Initialize()
        {
            MakeI();
            MakeJ();
            MakeO();
            MakeT();
            MakeL();
            MakeS();
            MakeZ();
            AllaBlock();
        } //Initierar alla block funktioner samt AllaBlock funktionen.

        #region Block Making Functions
        public static void MakeO() //Lista för O-blockets rotation
        {
            olista.Add(new Point(0, 0)); //Rotera runt här
            olista.Add(new Point(0, 1));
            olista.Add(new Point(1, 0));
            olista.Add(new Point(1, 1));

            olista.Add(new Point(0, 0)); //Rotera runt här
            olista.Add(new Point(0, 1));
            olista.Add(new Point(1, 0));
            olista.Add(new Point(1, 1));

            olista.Add(new Point(0, 0)); //Rotera runt här
            olista.Add(new Point(0, 1));
            olista.Add(new Point(1, 0));
            olista.Add(new Point(1, 1));

            olista.Add(new Point(0, 0)); //Rotera runt här
            olista.Add(new Point(0, 1));
            olista.Add(new Point(1, 0));
            olista.Add(new Point(1, 1));
        }
        public static void MakeJ() //Lista för J-blockets rotation
        {
            jlista.Add(new Point(1, 1)); //Rotera runt här
            jlista.Add(new Point(0, 0));
            jlista.Add(new Point(0, 1));
            jlista.Add(new Point(2, 1));

            jlista.Add(new Point(1, 1)); //Rotera runt här
            jlista.Add(new Point(1, 0));
            jlista.Add(new Point(2, 0));
            jlista.Add(new Point(1, 2));

            jlista.Add(new Point(1, 1)); //Rotera runt här
            jlista.Add(new Point(2, 1));
            jlista.Add(new Point(2, 2));
            jlista.Add(new Point(0, 1));

            jlista.Add(new Point(1, 1)); //Rotera runt här
            jlista.Add(new Point(1, 2));
            jlista.Add(new Point(0, 2));
            jlista.Add(new Point(1, 0));
        }
        public static void MakeL() //Lista för L-blockets rotation
        {
            llista.Add(new Point(1, 1)); //Rotera runt här
            llista.Add(new Point(2, 1));
            llista.Add(new Point(0, 1));
            llista.Add(new Point(0, 2));

            llista.Add(new Point(1, 1)); //Rotera runt här
            llista.Add(new Point(1, 2));
            llista.Add(new Point(1, 0));
            llista.Add(new Point(0, 0));

            llista.Add(new Point(1, 1)); //Rotera runt här
            llista.Add(new Point(2, 0));
            llista.Add(new Point(2, 1));
            llista.Add(new Point(0, 1));

            llista.Add(new Point(1, 1)); //Rotera runt här
            llista.Add(new Point(1, 0));
            llista.Add(new Point(2, 2));
            llista.Add(new Point(1, 2));
        }
        public static void MakeI() //Lista för I-blockets rotation
        {
            ilista.Add(new Point(1, 1)); //Rotera runt här
            ilista.Add(new Point(1, 0));
            ilista.Add(new Point(1, 2));
            ilista.Add(new Point(1, 3));

            ilista.Add(new Point(0, 2)); //Rotera runt här
            ilista.Add(new Point(1, 2));
            ilista.Add(new Point(2, 2));
            ilista.Add(new Point(3, 2));

            ilista.Add(new Point(2, 0)); //Rotera runt här
            ilista.Add(new Point(2, 1));
            ilista.Add(new Point(2, 2));
            ilista.Add(new Point(2, 3));

            ilista.Add(new Point(0, 1)); //Rotera runt här
            ilista.Add(new Point(1, 1));
            ilista.Add(new Point(2, 1));
            ilista.Add(new Point(3, 1));
        }
        public static void MakeS() //Lista för S-blockets rotation
        {
            slista.Add(new Point(1, 1)); //Rotera runt här
            slista.Add(new Point(1, 0));
            slista.Add(new Point(2, 0));
            slista.Add(new Point(0, 1));

            slista.Add(new Point(1, 1)); //Rotera runt här
            slista.Add(new Point(2, 2));
            slista.Add(new Point(2, 1));
            slista.Add(new Point(1, 0));

            slista.Add(new Point(1, 1)); //Rotera runt här
            slista.Add(new Point(1, 0));
            slista.Add(new Point(2, 0));
            slista.Add(new Point(0, 1));

            slista.Add(new Point(1, 1)); //Rotera runt här
            slista.Add(new Point(2, 2));
            slista.Add(new Point(2, 1));
            slista.Add(new Point(1, 0));
        }
        public static void MakeZ() //Lista för Z-blockets rotation
        {
            zlista.Add(new Point(1, 1)); //Rotera runt här
            zlista.Add(new Point(2, 2));
            zlista.Add(new Point(1, 2));
            zlista.Add(new Point(0, 1));

            zlista.Add(new Point(1, 1)); //Rotera runt här
            zlista.Add(new Point(1, 0));
            zlista.Add(new Point(0, 1));
            zlista.Add(new Point(0, 2));

            zlista.Add(new Point(1, 1)); //Rotera runt här
            zlista.Add(new Point(2, 2));
            zlista.Add(new Point(1, 2));
            zlista.Add(new Point(0, 1));

            zlista.Add(new Point(1, 1)); //Rotera runt här
            zlista.Add(new Point(1, 0));
            zlista.Add(new Point(0, 1));
            zlista.Add(new Point(0, 2));
        }
        public static void MakeT() //Lista för T-blockets rotation
        {
            tlista.Add(new Point(1, 0));
            tlista.Add(new Point(0, 1));
            tlista.Add(new Point(1, 1)); //Rotera runt här
            tlista.Add(new Point(2, 1));

            tlista.Add(new Point(1, 0));
            tlista.Add(new Point(1, 1)); //Rotera runt här
            tlista.Add(new Point(1, 2));
            tlista.Add(new Point(2, 1));

            tlista.Add(new Point(0, 1));
            tlista.Add(new Point(1, 1)); //Rotera runt här
            tlista.Add(new Point(2, 1));
            tlista.Add(new Point(1, 2));

            tlista.Add(new Point(0, 1));
            tlista.Add(new Point(1, 1)); //Rotera runt här
            tlista.Add(new Point(1, 0));
            tlista.Add(new Point(1, 2));
        }
        #endregion
    }
}
