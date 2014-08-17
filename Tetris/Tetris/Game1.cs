/* Klass Game1 skriven av:
 * Pontus Magnusson
 * Vilhelm Beijer
 * Yvonne Schudeck
 * Niklas Carlsson
 * Niklas Sj�qvist
 * Axel Gunnarsson
 * f�r projektarbete i Programmerings teknik i C# (DVA105) HT 2012
 * Klassen hanterar spelets huvudloopar och funktioner och 
 * �r standard i xna
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
        public static bool menuUpdate = true; // En bool f�r att sk�ta uppdateringarna i spelmenyn
        bool PlayingSound = false; // Kollar om ett ljud spelas ( slut ljudet )
        float redTimeCounter = 0;
        float blueTimeCounter = 0;

        

        public Game1() // Standard konstruktor f�r Game1 klassen.
        {
            graphics = new GraphicsDeviceManager(this); // Skapar ny GraphicsDeviceManager
            Content.RootDirectory = "Content"; // pekar inneh�llsmappen till en map med namnet "Content"
        }

        protected override void Initialize() // H�nder n�r spelet startar
        {
            this.IsMouseVisible = true; // Show the mousecursor
            graphics.PreferredBackBufferHeight = 600; //Set screenheight to 600
            graphics.PreferredBackBufferWidth = 800; //set screenwidth to 800
            graphics.ApplyChanges(); // Apply the screen size changes
            BlockLists.Initialize(); //Initierar r�da blocket
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentHandler.LoadAllContent(Content); //hanterar allt inneh�ll, m�ste laddas f�rst i LoadContent metoden
            ljudklass.LoadAudioContent(); //Laddar in ljudfilerna
            Menu.Initialize(); // Initierar menyn ( m�ste g�ras efter inneh�llet laddats )
            ljudklass.StartLjud(); //Ljud n�r programmet startas
            ljudklass.MenyMusik(); //Spelar upp musiken i menyn <- kan beh�va ses �ver senare
        }

        protected override void UnloadContent() // Standard funktion som vi valt att inte anv�nda
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (DrawMenu) // om satsen �r sann ska menylogiken uppdateras
            {
                Menu.HandleButtons(); //Hanterar knapplogik
                Menu.MenuAnimationUpdate(gameTime); // hanterar animationslogik i menyn
            }
            else if (!GameOver) //Annars, om man inte f�tt gameover och menyn inte ska visas
            {
                redTimeCounter += (int)gameTime.ElapsedGameTime.Milliseconds; //timeCounter f�r v�rdet f�r hur m�nga milisekunder det g�tt i spelet.
                blueTimeCounter += (int)gameTime.ElapsedGameTime.Milliseconds;

                if (blueTimeCounter >= Score.BlueBlockSpeed) //Om timeCounter �r st�rre eller lika med miliTimer s� faller blocket.
                {
                    BlueBlock.oldBlockPosY = BlueBlock.blockPosY; // Lagrar blockets f�rra position.
                    BlueBlock.blockPosY += ContentHandler.blueBlockTexture.Height; // Blocket faller positionen i y-led �kar.
                    blueTimeCounter = 0; // R�knaren blir noll
                    
                }
                if (redTimeCounter >= Score.RedBlockSpeed)
                {
                    RedBlock.oldBlockPosY = RedBlock.blockPosY; // Lagrar blockets f�rra position.
                    RedBlock.blockPosY += ContentHandler.blueBlockTexture.Height; // Blocket faller positionen i y-led �kar.
                    redTimeCounter = 0; // R�knaren blir noll
                }
                Score.RedBlockSpeed = 700 - (100 * Score.Level);
                Score.BlueBlockSpeed = 700 - (100 * Score.Level);
                Kollision.RotationsKollision(); // Kolla rotationskollision med spelplanens v�ggar
                Kollision.BlueLeftRightCollision(); // kolla h�ger och v�nster kollision med bl�tt block
                Kollision.RedLeftRightCollision(); // kolla h�ger och v�nster kollision med r�tt block
                BlueBlock.UpdateBlockMovement(gameTime);//Uppdatera bl�tt blocks r�relse 
                BlueBlock.UpdateBlockRotation();//uppdatera bl�tt blocks rotation
                RedBlock.UpdateBlockMovement(gameTime); // R�tt blocks r�relse-uppdaterings metod
                RedBlock.UpdateBlockRotation(); // r�tt blocks rotations-uppdaterings metod
                Kollision.Collision(); // Kolla bottenkollision och blockkollision f�r b�da blocken
                Score.UpdateLevel();
                GameField.CheckGameOver(); //Kolla om vi f�tt gameover
                GameField.RemoveRow(); //Kolla om en rad �r full, ta bort rad
            }
            else //Om gameover
            {
                ljudklass.CurrentSong.Stop(AudioStopOptions.AsAuthored); // Stoppa musiken fr�n spelet
                if (!PlayingSound) // om ljudet inte spelas.
                {
                    ljudklass.SpelSlutLjud(); // Spela upp ett ljud ( Spelelt �r slut ljud * kriterie * )
                    PlayingSound = true; // Nu spelas ett ljud 
                }
                ljudklass.CurrentSong.Stop(AudioStopOptions.AsAuthored);
                ljudklass.SpelSlutMusik();
                DrawMenu = true; // menyn kan ritas ut igen
                string name = Interaction.InputBox("Enter your name for highscore", "Game Over", "Enter Name Here"); // l�s in namn till highscore
                if (name.Length > 9) name = name.Substring(0, 9); // korta av namnet om det �r �ver nio tecken l�ng s� den f�r plats i rutan
                HighScoreClass.CheckIfHighScore(name, Score.score); // Stoppa in score och namn i highscore listan
                MenuBox.content = HighScoreClass.GetHighScore(); // l�s in nya highscore listan i highscoreboxen.
                MenuBox.title = "Highscore"; //  �ndra boxtiteln till highscore
                Menu.currentButtonSelected = 2; //�terst�ll currentselectedbutton i menyn till highscore.
                ljudklass.CurrentSong.Stop(AudioStopOptions.AsAuthored); // Stoppa musiken fr�n spelet
                ljudklass.MenyMusik(); //slutmusik f�r spelet
                GameField.Clear(); // Rensa spelf�ltet
            }


            if (Quit) // vid avslut
            {
                this.Exit();//Avsluta spelet
            }
            base.Update(gameTime); // grundfunktion f�r att uppdatera all logik i Update metoden
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black); // Standardmetod som t�cker sk�rmen i utvaldf�rg varje uppdatering
            
            if (DrawMenu) // Om menyn f�r ritas ut
            {
                Menu.DrawMenu(spriteBatch); // Rita ut menyn
            }
            else if (!GameOver) // annars om menyn inte f�r ritas ut, och det inte �r gameover (i spelet)
            {
                spriteBatch.Begin(); // b�rja rita med spritebatch();
                GameField.DrawSpelplanBlock(spriteBatch); // Rita ut spelplanen
                spriteBatch.End(); // sluta rita med spritebatch();
            }


            base.Draw(gameTime); // Grundl�ggande funktion f�r att rita ut allt som finns i Draw metoden
        }
    }
}
