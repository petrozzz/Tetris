/* Klass MenyBox skriven av Pontus Magnusson (pmn12003@student.mdh.se)
 * för projektarbete i Programmerings teknik i C# (DVA105) HT 2012
 * Klassen hanterar informationsrutan i menyn
 */

#region Usings
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Tetris
{
    static class MenuBox
    {
        #region Variables
        public static string title = ""; // Rutans titel
        public static string content = ""; // Rutans textinnehåll
        public static Rectangle box = new Rectangle(420, 220, 300, 300); //Rutans storlek och position
        public static bool showBox = false; // bool för att visa box eller ej
        public static Reglage volume = new Reglage(new Vector2(box.X + 45, box.Y + 40), true); // skapa ett reglag för att hantera volym
        public static Reglage fx = new Reglage(new Vector2(box.X + 45, box.Y + 130), false); // ett reglage för att hantera volym
        public static bool showSlides = false; // bool för att visa reglagen
        #endregion

        public static void UpdateBoxInfo() // Update the infobox
        {
           
            showSlides = false; // Standard är att inte visa slides
            volume.Increase(); // Uppdaterar reglage kontroller
            volume.Decrease(); // Uppdaterar reglage kontroller
            fx.Increase(); // Uppdaterar reglage kontroller
            fx.Decrease(); // Uppdaterar reglage kontroller
            switch (Menu.currentButtonSelected) //En switch sats för att hantera vilken knapp man tryckt på
            {
                case 1: // Start game
                    Game1.DrawMenu = false;
                    ljudklass.StartLjud(); //Ljud för att markera val i menyn, finns på alla alternativ.
                    ljudklass.CurrentSong.Stop(AudioStopOptions.AsAuthored); // Stoppa menymusiken
                    ljudklass.SpelMusik(); // spela bakgroundsmusik
                    Score.Lines = 0; // återställ antal linjer
                    Score.score = 0; // återställ poäng
                    Game1.GameOver = false;
                    break;
                case 2: // Highscore
                    title = "Highscores";
                    content = HighScoreClass.GetHighScore(); // Skickar in highscore från texten.
                    break;
                case 3: // Controls
                    title = "Controls";
                    content = "Control the left block with WASD, and the right block with IJKL. \nSmash with space,";
                    break;
                case 4: // audio
                    title = "Music \n\nFX\n\nSong\n";
                    content = "\n\n\n\n\n\n\n" + ljudklass.CurrentSong.Name;
                    showSlides = true;
                    break;
                case 5: // Help
                    title = "Help";
                    content = "This is a Tetris clone with a twist: Dual Blocks! You can control both blocks individually although the smash key works for both blocks. Good luck";
                    break;
                case 6: // Exit
                    Game1.Quit = true;
                    Menu.currentButtonSelected = 5;
                    break;
                case 7:
                    HighScoreClass.ResetAll();
                    Menu.currentButtonSelected = 2;
                    break;
                default: // Om inget, eller 0
                    showBox = false;
                    break;
            }
           
            Game1.menuUpdate = false;  //Stoppar uppdateringen av menyboxen
        }

        public static string parseText(string text, int width) //Metod för att kapa meningar i rätt längd så de passar i en ruta
        {
            string line = ""; // Håller varje rad
            string returntext = ""; // Håller den returnerade texten
            string[] words = text.Split(' '); //Ett fält som delar upp texten i ord

            foreach (string word in words) // För varje ord i arrayen words
            {
                if (ContentHandler.fontDebug.MeasureString(line + word).Length() > width) // om ord längden är längre än bredden(int width)
                {
                    returntext = returntext + line + '\n'; //så ta texten, lägg den i return text, och lägg till ny rad med "\n"
                    line = string.Empty; // töm line för att kunna göra om tills texten är slut
                }
                line = line + word + ' '; //lägger till ord och ett mellanrum i line
            }
            return returntext + line;// returnera den uppdelade texten
        }

        public static void DrawMenuBox(SpriteBatch spriteBatch) // Rita ut rutan
        {
            if (showBox) // Rita bara ut rutan om showBox är sann
            {
                spriteBatch.Draw(ContentHandler.lineTexture, new Vector2(40, Menu.currentButtonSelected * 50 + 240), Color.White); // Ritar ut en linje propertioneligt till vilken knapp som är vald
                spriteBatch.Draw(ContentHandler.boxTexture, new Vector2(box.X, box.Y), Color.White); //Ritar ut rutan
                spriteBatch.DrawString(ContentHandler.fontSketchBlock, title,
                                       new Vector2(5 + box.X + 
                                                  (box.Width / 2 - 
                                                  (ContentHandler.fontSketchBlock.MeasureString(title)
                                                  .Length()/2)), box.Y), 
                                                  Color.Sienna); // Ritar ut titeln överst i rutan, centrerat
                spriteBatch.DrawString(ContentHandler.fontDebug, parseText(content, box.Width - 10), new Vector2(box.X + 10, box.Y + 50), Color.Tomato); // skriver ut innehållstexten
                if (showSlides) // Om reglagen ska visas ( när audio menyvalet är valt )
                {
                    fx.DrawSlide(spriteBatch); // Rita ut reglaget
                    volume.DrawSlide(spriteBatch); // rita ut reglaget
                    Game1.menuUpdate = true; //för att uppdatera reglaget
                }
            }
        }
    }
}
