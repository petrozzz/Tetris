//Klassen skriven av Vilhelm Beijer (9310090619)
//för projektarbete i Programmerings teknik i C# (dva105)
//Höstterminen 2012

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    static class ContentHandler
    {
        //Static class för inte kunna initieras till flera objekt

        //initiera alla texturer här:
        public static Texture2D backGroundTexture,
                                logo_dualTexture,
                                logo_tetrisTexture,
                                boxTexture,
                                lineTexture,
                                blueBlockTexture,
                                redBlockTexture,
                                noBlockTexture,
                                backgroundTexture,
                                sliderTexture,
                                plusButtonTexture,
                                minusButtonTexture,
                                inGameOverlayTexture;
                                
        //initiera alla fonts här
        public static SpriteFont fontDebug,
                                 fontSketchBlock,
                                 fontSketchSmall;


        //En static void som körs från LoadContent i game1.cs
        public static void LoadAllContent(ContentManager contentManager)
        {
            #region Class Menu, Menubox, MenuButton, Reglage
            backgroundTexture = contentManager.Load<Texture2D>("slidebg");
            sliderTexture = contentManager.Load<Texture2D>("slider");
            plusButtonTexture = contentManager.Load<Texture2D>("sliderRight");
            minusButtonTexture = contentManager.Load<Texture2D>("sliderLeft");


            
            backGroundTexture = contentManager.Load<Texture2D>("bg800x600");
            logo_dualTexture = contentManager.Load<Texture2D>("logo_dual800x600");
            logo_tetrisTexture = contentManager.Load<Texture2D>("logo_tetris800x600");

            boxTexture = contentManager.Load<Texture2D>("Box");
            lineTexture = contentManager.Load<Texture2D>("Line");
            #endregion

            inGameOverlayTexture = contentManager.Load<Texture2D>("inGameOverlay");

            redBlockTexture = contentManager.Load<Texture2D>("redblock");
            blueBlockTexture = contentManager.Load<Texture2D>("block");
            noBlockTexture = contentManager.Load<Texture2D>("noBlock");

            #region Fonts
            fontDebug = contentManager.Load<SpriteFont>("debugFont");
            fontSketchBlock = contentManager.Load<SpriteFont>("SketchBlockFont");
            fontSketchSmall = contentManager.Load<SpriteFont>("SketchBlockSmall");
            #endregion
        }
    }
}
