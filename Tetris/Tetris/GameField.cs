//Klassen skriven av Vilhelm Beijer (9310090619)
//för projektarbete i Programmerings teknik i C# (dva105)
//Höstterminen 2012

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public static class GameField
    {
        public static int Width = 15; //bredden på spelplanen
        public static int Height = 21; //höjden på spelplanen
        public static int[,] playfield = new int[Width, Height]; //hela spelplanen, dvs ett fält

        public static void RemoveRow() //metoden för att ta bort en rad
        {
            
            int xCounter = 0;
            for (int yCounter = 0; yCounter < GameField.Height - 1; yCounter++) // loopar igenom höjdvärden i spelplanen
            {
                for (xCounter = 0; xCounter < GameField.Width - 1 && GameField.playfield[xCounter, yCounter] > 0; xCounter++) ; //Kollar om raden innehåller endast innehåller block

                if (xCounter == GameField.Width - 1)
                {
                    ljudklass.Rad(); // Spela upp ljud när en rad tas bort
                    Score.Lines += 1; // Öka antal lines
                    Score.score += 1000; // Öka score med 1000 per line
                    for (xCounter = 0; xCounter < GameField.Width - 1; xCounter++) // Loopa hela raden för att tömma raden
                    {
                        GameField.playfield[xCounter, yCounter] = 0; // Sätt värdena på raden till 0
                    }
                    for (; yCounter > 0; yCounter--) //Loopa nedflyttning av allt över raden
                    {
                        for (xCounter = 0; xCounter < GameField.Width - 1; xCounter++) // Loopa raden
                        {
                            GameField.playfield[xCounter, yCounter] = GameField.playfield[xCounter, yCounter - 1]; // flytta ner övre raden
                        }
                    }
                }
            }
        }

        public static void DrawSpelplanBlock(SpriteBatch sb) //Ritar ut spelplanen och blocken.
        {
            // Följande loop ritar ut gråa block över hela spelplanen
            for (int x = 0; x < GameField.Width - 1; x++)
            {
                for (int y = 0; y < GameField.Height - 1; y++)
                {
                    //260 = skärmbredd(800)/2 - spelplansbredd(280)/2
                    //100 = skärmhöjd(600)/2 - spelplanshöjd(400)/2
                    sb.Draw(ContentHandler.noBlockTexture, new Vector2(260 + x * ContentHandler.blueBlockTexture.Width, 100 + y * ContentHandler.blueBlockTexture.Height), Color.White);
                }
            }

            for (int xCounter = 0; xCounter < GameField.Width - 1; xCounter++) //loop för X
            {
                for (int yCounter = 0; yCounter < GameField.Height - 1; yCounter++)//loop för Y
                {
                    if (GameField.playfield[xCounter, yCounter] == 1) // Ritar ut blått block om värdet på spelplanen är över 0
                    {
                        //260 = skärmbredd(800)/2 - spelplansbredd(280)/2
                        //100 = skärmhöjd(600)/2 - spelplanshöjd(400)/2
                        sb.Draw(ContentHandler.blueBlockTexture, new Vector2(260 + xCounter * ContentHandler.blueBlockTexture.Width, 100 + yCounter * ContentHandler.blueBlockTexture.Height), Color.White); //ritar block på ettor
                    }
                    if (GameField.playfield[xCounter, yCounter] == 2) // Ritar ut blått block om värdet på spelplanen är över 0
                    {
                        //260 = skärmbredd(800)/2 - spelplansbredd(280)/2
                        //100 = skärmhöjd(600)/2 - spelplanshöjd(400)/2
                        sb.Draw(ContentHandler.redBlockTexture, new Vector2(260 + xCounter * ContentHandler.blueBlockTexture.Width, 100 + yCounter * ContentHandler.blueBlockTexture.Height), Color.White); //ritar block på ettor
                    }
                }
            }
            BlueBlock.DrawBlock(sb); // Draw the blue block
            RedBlock.DrawBlock(sb); // Draw the red block

            sb.Draw(ContentHandler.inGameOverlayTexture, Vector2.Zero, Color.White); //Ritar ut overlay-bilden

            //Rita ut det blå förhandsgranskningsblocket
            for (int i = 0; i < 4; i++)
            {
                //260 = skärmbredd(800)/2 - spelplansbredd(280)/2
                //100 = skärmhöjd(600)/2 - spelplanshöjd(400)/2
                sb.Draw(ContentHandler.redBlockTexture, new Vector2(585 + BlockLists.allaBlockLista[RedBlock.nextBlock][i + (RedBlock.rot * 4)].X * ContentHandler.blueBlockTexture.Width, 170 + BlockLists.allaBlockLista[RedBlock.nextBlock][i + (RedBlock.rot * 4)].Y * ContentHandler.blueBlockTexture.Height), Color.White);
            }
            //Rita ut vänster sidas förhandsgranskningsblocket
            for (int i = 0; i < 4; i++)
            {
                //260 = skärmbredd(800)/2 - spelplansbredd(280)/2
                //100 = skärmhöjd(600)/2 - spelplanshöjd(400)/2
                sb.Draw(ContentHandler.blueBlockTexture, new Vector2(180 + BlockLists.allaBlockLista[BlueBlock.nextBlock][i + (BlueBlock.rot * 4)].X * ContentHandler.blueBlockTexture.Width, 170 + BlockLists.allaBlockLista[BlueBlock.nextBlock][i + (BlueBlock.rot * 4)].Y * ContentHandler.blueBlockTexture.Height), Color.White);
            }

            //Skriver ut färdiga rader
            sb.DrawString(ContentHandler.fontSketchSmall, MenuBox.parseText("Lines Completed", 80), new Vector2(545, 255), Color.White);
            sb.DrawString(ContentHandler.fontDebug, Score.Lines.ToString(), new Vector2(605, 300), Color.White);

            //Skriver ut poäng
            sb.DrawString(ContentHandler.fontSketchSmall, "Score", new Vector2(565, 385), Color.White);
            sb.DrawString(ContentHandler.fontDebug, Score.score.ToString(), new Vector2(605, 410), Color.White);

            //Skriver ut Level
            sb.DrawString(ContentHandler.fontSketchSmall, "Level", new Vector2(165, 255), Color.White);
            sb.DrawString(ContentHandler.fontDebug, Score.Level.ToString(), new Vector2(180, 280), Color.White);


        }
        public static void CheckGameOver() // kollar gameover när block är fästa över en viss position
        {
            for (int row = 0; row < GameField.Width - 1; row++)
            {
                if (GameField.playfield[row, 2] >= 1)
                {
                    Game1.GameOver = true;
                }
            }
        }

        public static void Clear() // Tömmer hela spelfältet vid omstart av spel exempelvis
        {
            for (int x = 0; x < Width - 1; x++)
            {
                for (int y = 0; y < Height - 1; y++)
                {
                    playfield[x, y] = 0;
                }
            }
        }
    }
}
