/* Klass HighScoreClass är skriven av Axel Gunnarsson (AGN12013@student.mdh.se)
 * Kurs: Programmeringsteknik i c# (DVA105)
 * Klassen hanterar Highscorelistan, med skrivningar, läsning och reset. 
 */

using System;
using System.IO;

namespace Tetris
{
    class HighScoreClass
    {
        public static void CheckIfHighScore(string NameScoreHolder, uint CurrentScore)
        {
            string HighScoreText;                               // Skapar en string som håller hela texten medans den arbetas på.
            uint[] Highscore = new uint[6];                     // Skapar 6 uint i en array, 6 stycken för att lätt veta highscoreNr 1, 0 används inte.
            string[] HighscoreString = new string[6];           // Skapar 6 string i en array 6 stycken för att lätt veta highscoreNr 1, 0 används inte.
            bool oneTimeOnly = false;                           // Skapar en bool, så den inte går in i någon if sats mer än en gång och därigenom fyller hela med det nya högre värdet.
            FileStream HighScoreFileStream = new FileStream("Content\\HighScores.txt", FileMode.OpenOrCreate);   // öppnar Highscore.txt för att komma åt den senare.
            StreamReader HighScoreStreamReader = new StreamReader(HighScoreFileStream);                 // Skapar en StreamReader för att kunna läsa texten i HighScore.txt

            HighScoreText = HighScoreStreamReader.ReadToEnd();  // Sätter HighScoreText till vad HighScore.txt innehåller

            Highscore[1] = uint.Parse(HighScoreText.Substring(12, 9));  // använder substring för att sätta Highscore1 till numret som börjar på plats 12 i HighScoreText och slutar på plats 12+9
            Highscore[2] = uint.Parse(HighScoreText.Substring(35, 9));  
            Highscore[3] = uint.Parse(HighScoreText.Substring(58, 9));  
            Highscore[4] = uint.Parse(HighScoreText.Substring(81, 9));  
            Highscore[5] = uint.Parse(HighScoreText.Substring(104, 9)); 

            HighscoreString[1] = HighScoreText.Substring(0, 21);        // Tar in hela raden, Namn och poäng för att senare kunna skriva ut det lättare i den uppdaterade highscore.txt
            HighscoreString[2] = HighScoreText.Substring(23, 21);
            HighscoreString[3] = HighScoreText.Substring(46, 21);
            HighscoreString[4] = HighScoreText.Substring(69, 21);
            HighscoreString[5] = HighScoreText.Substring(92, 21);

            for (int c = 1; c < 6; c++)         // For loop för att kolla om den inskickade poängen är högre än något, och i såfall lägga in det i listan på rätt plats.
            {
                if (uint.Parse(HighscoreString[c].Substring(12, 9)) < CurrentScore && oneTimeOnly == false)     //Vilket highscore är den högre än
                {
                    if (c == 5)     // om den bara är högre än sista, ersätt sista och säg att den är använd.
                    {
                        HighscoreString[c] = String.Format("{0,9} - {1,9}", NameScoreHolder, CurrentScore);     // Formarterar strängen så det stämmer med resten av highscoren.
                        oneTimeOnly = true;
                    }
                    else if (c == 4)    // om den är högre än näst sista, ersätt sista med näst sista och sätt in på rätt plats och säg att den är använd.
                    {
                        HighscoreString[5] = HighscoreString[4];
                        HighscoreString[c] = String.Format("{0,9} - {1,9}", NameScoreHolder, CurrentScore);
                        oneTimeOnly = true;
                    }
                    else if (c == 3)
                    {
                        HighscoreString[5] = HighscoreString[4];
                        HighscoreString[4] = HighscoreString[3];
                        HighscoreString[c] = String.Format("{0,9} - {1,9}", NameScoreHolder, CurrentScore);
                        oneTimeOnly = true;
                    }
                    else if (c == 2)
                    {
                        HighscoreString[5] = HighscoreString[4];
                        HighscoreString[4] = HighscoreString[3];
                        HighscoreString[3] = HighscoreString[2];
                        HighscoreString[c] = String.Format("{0,9} - {1,9}", NameScoreHolder, CurrentScore);
                        oneTimeOnly = true;
                    }
                    else if (c == 1)
                    {
                        HighscoreString[5] = HighscoreString[4];
                        HighscoreString[4] = HighscoreString[3];
                        HighscoreString[3] = HighscoreString[2];
                        HighscoreString[2] = HighscoreString[1];
                        HighscoreString[c] = String.Format("{0,9} - {1,9}", NameScoreHolder, CurrentScore);
                        oneTimeOnly = true;
                    }                           //Så är det hela vägen, bara ändras vilka den ersätter och sätts in.
                }
            }

            HighScoreStreamReader.Close();      // stänger användningen av filerna.
            HighScoreFileStream.Close();

            //Rensa skriv ut nya! 

            FileStream HighScoreFileStreamNew = new FileStream("Content\\HighScores.txt", FileMode.Create);     // Skapar en ny HighScores.txt för att det ska stämma och bli ersatt rätt.
            StreamWriter HighScoreStreamWriterNew = new StreamWriter(HighScoreFileStreamNew);

            HighScoreStreamWriterNew.WriteLine(HighscoreString[1]);     // Skriver in highscore 1-5 på rätt plats.
            HighScoreStreamWriterNew.WriteLine(HighscoreString[2]);
            HighScoreStreamWriterNew.WriteLine(HighscoreString[3]);
            HighScoreStreamWriterNew.WriteLine(HighscoreString[4]);
            HighScoreStreamWriterNew.WriteLine(HighscoreString[5]);

            //HighScoreStreamWriter.WriteLine("{0,9}  -  {1,20}", NameScoreHolder, Score);

            HighScoreStreamWriterNew.Close();       //Stänger dom nya.
            HighScoreFileStreamNew.Close();

        }

        public static string GetHighScore()     // vill man bara hämta Highsvcoret från filen som en sträng använd denna.
        {
            FileStream HighScoreFileStream = new FileStream("Content\\HighScores.txt", FileMode.OpenOrCreate);  // öppnar filen
            StreamReader HighScoreStreamReader = new StreamReader(HighScoreFileStream);     // Läser filen.

            string HighScoreToReturn;       // Skapar strängen som ska retuneras.
            HighScoreToReturn = HighScoreStreamReader.ReadToEnd();      // Sätter in hela highscor listan i strängen

            HighScoreStreamReader.Close();      // Stänger läs och fil strömen.
            HighScoreFileStream.Close();

            return HighScoreToReturn;       // returnerar strängen.
        }

        public static void ResetAll()       //Använd om du vill reseta Highscorelistan.
        {
            FileStream HighScoreFileStream = new FileStream("Content\\HighScores.txt", FileMode.Create);    //Ersätter den gamla highscore med en ny tom.
            StreamWriter HighScoreStreamWriter = new StreamWriter(HighScoreFileStream);         // Öpnar ström för att kunna skriva.

            for (int x = 0; x < 5; x++)     // Skriver ut 5 nya highscores i form av namn : noll, och poäng 0.
            {
                HighScoreStreamWriter.WriteLine("{0,9} - {1,9}", "noll", "0");
            }

            //HighScoreStreamWriter.WriteLine("{0,9}  -  {1,9}", NameScoreHolder, Score);

            HighScoreStreamWriter.Close();      // stänger fil och skriv strömmen.
            HighScoreFileStream.Close();        
        }
    }
}
