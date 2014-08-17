/* Klass Reglage skriven av Pontus Magnusson (pmn12003@student.mdh.se)
 * för projektarbete i Programmerings teknik i C# (DVA105) HT 2012
 * Klassen hanterar reglagen som finns i menyn och styr volym.
 */
#region Usings
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion
namespace Tetris
{
    class Reglage
    {
        #region Variables
        Vector2 sliderBackgroundPosition, sliderPosition, plusButtonPos, minusButtonPos; // Hanterar position för de olika spritesen
        public float slideValue = 1.0f; // värdet på slide-elementet
        bool clicked = false; //bool för att hantera klick
        bool fxVolume = false; //bool för att kolla om du kontrollerar FX eller Musik (true = FX, false = musik)
        #endregion

        public Reglage(Vector2 Position, bool Control) // konstruktor som läser in position och typ av kontrol ( med bool )
        {
            fxVolume = Control;
            sliderBackgroundPosition = Position;
            sliderPosition = new Vector2(slideValue * 180 + sliderBackgroundPosition.X - 10, sliderBackgroundPosition.Y);
            plusButtonPos = new Vector2(sliderBackgroundPosition.X + ContentHandler.backgroundTexture.Width - (ContentHandler.plusButtonTexture.Width/2), sliderBackgroundPosition.Y);
            minusButtonPos = new Vector2(sliderBackgroundPosition.X - ContentHandler.minusButtonTexture.Width + 10, sliderBackgroundPosition.Y);
        }

        public void DrawSlide(SpriteBatch spriteBatch) // Ritar ut reglaget
        {
            spriteBatch.Draw(ContentHandler.backgroundTexture, sliderBackgroundPosition, Color.White);
            spriteBatch.Draw(ContentHandler.plusButtonTexture, plusButtonPos, Color.White);
            spriteBatch.Draw(ContentHandler.minusButtonTexture, minusButtonPos, Color.White);
            spriteBatch.Draw(ContentHandler.sliderTexture, sliderPosition, Color.White);
        }

        public void Increase() // Funktion för att öka värdet på reglaget
        {
            if (Mouse.GetState().X > plusButtonPos.X && Mouse.GetState().X < plusButtonPos.X + ContentHandler.plusButtonTexture.Width)
            {
                if (Mouse.GetState().Y > plusButtonPos.Y && Mouse.GetState().Y < plusButtonPos.Y + ContentHandler.plusButtonTexture.Height)
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && !clicked)
                    {
                        if (slideValue < 1.0f)
                            slideValue += 0.1f;
                        if (fxVolume)
                            ljudklass.MusikVolym(slideValue); //uppdaterar önskad musikvolym
                        else
                            ljudklass.EffektVolym(slideValue);//uppdaterar önskad musikvolym
                        clicked = true;
                    }
                }
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                clicked = false;
            }
            sliderPosition = new Vector2(slideValue * 180 + sliderBackgroundPosition.X - 10, sliderBackgroundPosition.Y);
        }

        public void Decrease() // funktion för att minska reglagevärdet
        {
            if (Mouse.GetState().X > minusButtonPos.X && Mouse.GetState().X < minusButtonPos.X + ContentHandler.minusButtonTexture.Width)
            {
                if (Mouse.GetState().Y > minusButtonPos.Y && Mouse.GetState().Y < minusButtonPos.Y + ContentHandler.minusButtonTexture.Height)
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && !clicked)
                    {
                        if (slideValue >= 0.1f)
                            slideValue -= 0.1f;
                        if (fxVolume)
                            ljudklass.MusikVolym(slideValue); //uppdaterar önskad musikvolym
                        else
                            ljudklass.EffektVolym(slideValue);//uppdaterar önskad musikvolym
                        clicked = true;
                    }
                    
                }
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                clicked = false;
            }
            sliderPosition = new Vector2(slideValue * 180 + sliderBackgroundPosition.X - 10, sliderBackgroundPosition.Y);
        }

    }
}
