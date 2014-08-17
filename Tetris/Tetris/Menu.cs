/* Klass Menu skriven av Pontus Magnusson (pmn12003@student.mdh.se)
 * för projektarbete i Programmerings teknik i C# (DVA105) HT 2012
 * Klassen hanterar Menyn
 */

#region Usings
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Tetris
{
    static class Menu
    {
        #region Variables
        private static double milliSecondCounter = 0; // räknar millisekunder
        private static float logo_dual_scale = 1f; //håller skalan för dual sprite
        static List<MenuButton> buttonList = new List<MenuButton>(); // lista för att lagra knapparna
        public static int currentButtonSelected = 0; // lagrar den nuvarande valda knappen
        #endregion

        public static void CheckForButtonClick() // Hanterar klick på knapparna
        {
            for(int i = 0; i<buttonList.Count; i++) // loopa igenom alla knappar
            {
                if (buttonList[i].PressedButton()) //händelse att loopa: PressedButton() som känner av om knappen är tryckt på
                {
                    Game1.menuUpdate = true; //Gör så att menyn uppdateras varje gång ett val aktiveras (längre ner).
                    if (currentButtonSelected != i + 1) // kollar om du trycker på en knapp igen så den kan slås på och av som en lampknapp
                    { 
                        currentButtonSelected = i + 1; // +1 så att 0 inte är ett menyval
                        MenuBox.showBox = true; // visa menubox
                    }
                    else // om du tryckt på samma knapp två gånger
                    {
                        MenuBox.showBox = false; // göm MenuBox
                        currentButtonSelected = 0;  // sätt vald knapp till 0 (ingen knapp vald)
                    }
                }
            }
            if (Game1.menuUpdate)
            {
                MenuBox.UpdateBoxInfo(); // Updatera innehållet i menuBox
            }
        }

        public static void Initialize()
        {

            //Skapa knappar 
            MenuButton Start = new MenuButton("Start Game", new Vector2(40, 250), Color.Gold);
            MenuButton Highscore = new MenuButton("Highscore", new Vector2(40, 300));
            MenuButton Controls = new MenuButton("Controls", new Vector2(40, 350));
            MenuButton Audio = new MenuButton("Audio", new Vector2(40, 400));
            MenuButton Help = new MenuButton("Help", new Vector2(40, 450));
            MenuButton Exit = new MenuButton("Exit", new Vector2(40, 500));
            //lägg knappar i lista
            buttonList.Add(Start);
            buttonList.Add(Highscore);
            buttonList.Add(Controls);
            buttonList.Add(Audio);
            buttonList.Add(Help);
            buttonList.Add(Exit);
        }

        public static void HandleButtons() //hantera knappuppdateringar
        {
            foreach (MenuButton button in buttonList) // Loopar igenom alla knappar i listan
            {
                button.UpdateButton(); //anropa UpdateButton() för varje knapp i listan
                CheckForButtonClick(); // anropa CheckForButtonClick() för varje knapp i listan
            }
            
        }

        public static void MenuAnimationUpdate(GameTime gameTime) // hanterar animationen på Dual sprite
        {
            milliSecondCounter += gameTime.ElapsedGameTime.TotalMilliseconds / 100; // Styr hastigheten
            logo_dual_scale += (float)Math.Sin(milliSecondCounter) / 100; // styr hur "högt" den ska hoppa
        }

        public static void DrawMenu(SpriteBatch spriteBatch) // Ritar ut menyn
        {
            spriteBatch.Begin(); // Standard    

            spriteBatch.Draw(ContentHandler.backGroundTexture, Vector2.Zero, Color.White * 0.45f); // Ritar ut backgrunds bilden
            spriteBatch.Draw(ContentHandler.logo_tetrisTexture, new Vector2(150, 50), Color.White * 0.8f); // Ritar ut tetris loggan
            spriteBatch.Draw(ContentHandler.logo_dualTexture, new Vector2(40, 10), null, Color.White, 0f, Vector2.Zero, logo_dual_scale, SpriteEffects.None, 1f); //Draw the animated dual Ritar ut animerade dual sprite

            foreach (MenuButton b in buttonList) // Ritar ut varje knapp i listan
            {
                b.DrawButton(spriteBatch);
            }
            
            MenuBox.DrawMenuBox(spriteBatch); // Ritar ut MenuBox 

            spriteBatch.End();
        }
    }
}
