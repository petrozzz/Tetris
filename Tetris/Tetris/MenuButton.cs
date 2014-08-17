/* Klass MenuButton skriven av Pontus Magnusson (pmn12003@student.mdh.se)
 * för projektarbete i Programmerings teknik i C# (DVA105) HT 2012
 * Klassen hanterar Menyns knappar
 */

#region Usings
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Tetris
{
    class MenuButton
    {
        #region Variables
        Vector2 buttonPosition; // Knapp positioin
        public string buttonText; // Knapp test
        float buttonScale = 1f; // Knapp skala
        bool buttonPressed = false; // bool för att kollan om man tryckt på musen så koden inte loopar när du håller ner musknappen 
        int textHeight; // lagrar texthöjd
        Color buttonColor = Color.Khaki; // Standard knapptextfärg
        #endregion

        //Två konstruktorer
        //Den första tar text och position som argument
        //Den andra tar även färg utöver dens första två argument
        public MenuButton(string ButtonText, Vector2 ButtonPosition)
        {
            buttonText = ButtonText;
            buttonPosition = ButtonPosition;
            textHeight = (int)ContentHandler.fontSketchBlock.MeasureString("abcdefghijklmnopqrstuvwxyz").Y;
        }
        public MenuButton(string ButtonText, Vector2 ButtonPosition, Color Color)
        {
            buttonColor = Color;
            buttonText = ButtonText;
            buttonPosition = ButtonPosition;
            textHeight = (int)ContentHandler.fontSketchBlock.MeasureString("abcdefghijklmnopqrstuvwxyz").Y;
        }

        public int MeasureText(string text) // Metod som mäter textbredd i fonten "Sketch Block"
        {
            return (int)ContentHandler.fontSketchBlock.MeasureString(buttonText).Length(); //Returnerar textbredd i pixlar
        }

        public void UpdateButton() //kollar "MouseOver" och "MouseDown" För visuella effekter
        {
            //om musen är över knappens yta
            if (Mouse.GetState().X > buttonPosition.X && Mouse.GetState().X < buttonPosition.X + MeasureText(buttonText)
                && Mouse.GetState().Y > buttonPosition.Y && Mouse.GetState().Y < buttonPosition.Y + textHeight
                && Mouse.GetState().LeftButton == ButtonState.Released)
            {
                buttonScale = 1.05f; // öka storleken på knappen liiiite
            }
                //annars om den är över ytan och vänster musknapp är nedtryckt
            else if (Mouse.GetState().X > buttonPosition.X && Mouse.GetState().X < buttonPosition.X + MeasureText(buttonText)
                && Mouse.GetState().Y > buttonPosition.Y && Mouse.GetState().Y < buttonPosition.Y + textHeight
                && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                buttonScale = 0.95f; //Minska storleken liite
            }
            else
            {
                buttonScale = 1f; // Annars standard storlek
            }
        }

        public bool PressedButton() // Kollar om användaren tryckt på en knapp
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !buttonPressed)
            {
                if (Mouse.GetState().X > buttonPosition.X && Mouse.GetState().X < buttonPosition.X + MeasureText(buttonText)
                && Mouse.GetState().Y > buttonPosition.Y && Mouse.GetState().Y < buttonPosition.Y + textHeight)
                {
                    buttonPressed = true;
                    return true; // Returnera sant. Ja vi har tryckt på knappen
                }
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                buttonPressed = false;
            }

            return false; // Standard är falskt, du har inte tryckt på knappisen
        }

        public void DrawButton(SpriteBatch spriteBatch) // Ritar ut knappen
        {
            spriteBatch.DrawString(ContentHandler.fontSketchBlock, buttonText, buttonPosition, buttonColor, 0f, Vector2.Zero, buttonScale, SpriteEffects.None, 1f);
        }
    }
}
