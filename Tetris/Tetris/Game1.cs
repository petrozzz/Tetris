/* Klass Game1 skriven av:
 * Pontus Magnusson
 * Vilhelm Beijer
 * Yvonne Schudeck
 * Niklas Carlsson
 * Niklas Sjöqvist
 * Axel Gunnarsson
 * för projektarbete i Programmerings teknik i C# (DVA105) HT 2012
 * Klassen hanterar spelets huvudloopar och funktioner och 
 * är standard i xna
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.VisualBasic;

namespace Tetris
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static bool Quit = false; // public static bool that can be reached from any class so game1 class can use its precious shutting down command
        public static bool DrawMenu = true; // check if in meny or playing
        public static bool GameOver = false; // Check for gameover
        public static bool menuUpdate = true; // En bool för att sköta uppdateringarna i spelmenyn
        bool PlayingSound = false; // Kollar om ett ljud spelas ( slut ljudet )
        float redTimeCounter = 0;
        float blueTimeCounter = 0;

        

        public Game1() // Standard konstruktor för Game1 klassen.
        {
            graphics = new GraphicsDeviceManager(this); // Skapar ny GraphicsDeviceManager
            Content.RootDirectory = "Content"; // pekar innehållsmappen till en map med namnet "Content"
        }

        protected override void Initialize() // Händer när spelet startar
        {
            this.IsMouseVisible = true; // Show the mousecursor
            graphics.PreferredBackBufferHeight = 600; //Set screenheight to 600
            graphics.PreferredBackBufferWidth = 800; //set screenwidth to 800
            graphics.ApplyChanges(); // Apply the screen size changes
            BlockLists.Initialize(); //Initierar röda blocket
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentHandler.LoadAllContent(Content); //hanterar allt innehåll, måste laddas först i LoadContent metoden
            ljudklass.LoadAudioContent(); //Laddar in ljudfilerna
            Menu.Initialize(); // Initierar menyn ( måste göras efter innehållet laddats )
            ljudklass.StartLjud(); //Ljud när programmet startas
            ljudklass.MenyMusik(); //Spelar upp musiken i menyn <- kan behöva ses över senare
        }

        protected override void UnloadContent() // Standard funktion som vi valt att inte använda
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (DrawMenu) // om satsen är sann ska menylogiken uppdateras
            {
                Menu.HandleButtons(); //Hanterar knapplogik
                Menu.MenuAnimationUpdate(gameTime); // hanterar animationslogik i menyn
            }
            else if (!GameOver) //Annars, om man inte fått gameover och menyn inte ska visas
            {
                redTimeCounter += (int)gameTime.ElapsedGameTime.Milliseconds; //timeCounter får värdet för hur många milisekunder det gått i spelet.
                blueTimeCounter += (int)gameTime.ElapsedGameTime.Milliseconds;

                if (blueTimeCounter >= Score.BlueBlockSpeed) //Om timeCounter är större eller lika med miliTimer så faller blocket.
                {
                    BlueBlock.oldBlockPosY = BlueBlock.blockPosY; // Lagrar blockets förra position.
                    BlueBlock.blockPosY += ContentHandler.blueBlockTexture.Height; // Blocket faller positionen i y-led ökar.
                    blueTimeCounter = 0; // Räknaren blir noll
                    
                }
                if (redTimeCounter >= Score.RedBlockSpeed)
                {
                    RedBlock.oldBlockPosY = RedBlock.blockPosY; // Lagrar blockets förra position.
                    RedBlock.blockPosY += ContentHandler.blueBlockTexture.Height; // Blocket faller positionen i y-led ökar.
                    redTimeCounter = 0; // Räknaren blir noll
                }
                Score.RedBlockSpeed = 700 - (100 * Score.Level);
                Score.BlueBlockSpeed = 700 - (100 * Score.Level);
                Kollision.RotationsKollision(); // Kolla rotationskollision med spelplanens väggar
                Kollision.BlueLeftRightCollision(); // kolla höger och vänster kollision med blått block
                Kollision.RedLeftRightCollision(); // kolla höger och vänster kollision med rött block
                BlueBlock.UpdateBlockMovement(gameTime);//Uppdatera blått blocks rörelse 
                BlueBlock.UpdateBlockRotation();//uppdatera blått blocks rotation
                RedBlock.UpdateBlockMovement(gameTime); // Rött blocks rörelse-uppdaterings metod
                RedBlock.UpdateBlockRotation(); // rött blocks rotations-uppdaterings metod
                Kollision.Collision(); // Kolla bottenkollision och blockkollision för båda blocken
                Score.UpdateLevel();
                GameField.CheckGameOver(); //Kolla om vi fått gameover
                GameField.RemoveRow(); //Kolla om en rad är full, ta bort rad
            }
            else //Om gameover
            {
                ljudklass.CurrentSong.Stop(AudioStopOptions.AsAuthored); // Stoppa musiken från spelet
                if (!PlayingSound) // om ljudet inte spelas.
                {
                    ljudklass.SpelSlutLjud(); // Spela upp ett ljud ( Spelelt är slut ljud * kriterie * )
                    PlayingSound = true; // Nu spelas ett ljud 
                }
                ljudklass.CurrentSong.Stop(AudioStopOptions.AsAuthored);
                ljudklass.SpelSlutMusik();
                DrawMenu = true; // menyn kan ritas ut igen
                string name = Interaction.InputBox("Enter your name for highscore", "Game Over", "Enter Name Here"); // läs in namn till highscore
                if (name.Length > 9) name = name.Substring(0, 9); // korta av namnet om det är över nio tecken lång så den får plats i rutan
                HighScoreClass.CheckIfHighScore(name, Score.score); // Stoppa in score och namn i highscore listan
                MenuBox.content = HighScoreClass.GetHighScore(); // läs in nya highscore listan i highscoreboxen.
                MenuBox.title = "Highscore"; //  ändra boxtiteln till highscore
                Menu.currentButtonSelected = 2; //Återställ currentselectedbutton i menyn till highscore.
                ljudklass.CurrentSong.Stop(AudioStopOptions.AsAuthored); // Stoppa musiken från spelet
                ljudklass.MenyMusik(); //slutmusik för spelet
                GameField.Clear(); // Rensa spelfältet
            }


            if (Quit) // vid avslut
            {
                this.Exit();//Avsluta spelet
            }
            base.Update(gameTime); // grundfunktion för att uppdatera all logik i Update metoden
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black); // Standardmetod som täcker skärmen i utvaldfärg varje uppdatering
            
            if (DrawMenu) // Om menyn får ritas ut
            {
                Menu.DrawMenu(spriteBatch); // Rita ut menyn
            }
            else if (!GameOver) // annars om menyn inte får ritas ut, och det inte är gameover (i spelet)
            {
                spriteBatch.Begin(); // börja rita med spritebatch();
                GameField.DrawSpelplanBlock(spriteBatch); // Rita ut spelplanen
                spriteBatch.End(); // sluta rita med spritebatch();
            }


            base.Draw(gameTime); // Grundläggande funktion för att rita ut allt som finns i Draw metoden
        }
    }
}
