/* Klass Score skriven av Pontus Magnusson (pmn12003@student.mdh.se)
 * för projektarbete i Programmerings teknik i C# (DVA105) HT 2012
 * Klassen hanterar poäng.
 */

namespace Tetris
{
    public static class Score
    {
        public static uint score = 0; // Poäng ( uint för lite högre poäng )
        public static int Lines = 0; // antal rader
        public static int Level = 0; // svårighetsgrad
        public static int PointForLine = 1000;
        public static int PointForDunk = 15;
        public static float RedBlockSpeed = 700 - (100 * Score.Level);
        public static float BlueBlockSpeed = 700 - (100 * Score.Level);

        public static void UpdateLevel() // Uppdatera svårighetsgrad
        {
             Level = (Lines / 5);
        }
    }
}
