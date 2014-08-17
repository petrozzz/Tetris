﻿/* Klass BlueBlock är skriven av Yvonne Schudeck (ysk12001@student.mdh.se)
 * Kurs: Programmeringsteknik i c# (DVA105)
 * Klassen hanterar dem blåa blocken. 
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    class BlueBlock
    {
        public static bool apressed = false; //Bool för om A är nedtryckt eller inte.
        public static bool dpressed = false; //Bool för om D är nedtryckt eller inte.
        public static bool wpressed = false; //Bool för om W är nedtryckt eller inte.

        public static int rot = 0; //Int för blockens rotation
        public static int blockPosX = 20; //Blockens x position
        public static int blockPosY = 0; // Blockens y position
        public static int oldBlockPosX = 0; // Lagring för blockets före detta X-position
        public static int oldBlockPosY = 0; // Lagring för blockets före detta Y-position

        public static bool newBlock = false; // Bool om det kommit ett nytt block eller inte.

        public static Random randomnummer = new Random((int)DateTime.Now.Ticks); // Slumpgenerator för blocken
        public static int blockholder; // Int för vilket block som ska ritas ut.
        public static int nextBlock = 0; // Håller värdet för nästa block.
        public static Vector2[] oldBlockPos = new Vector2[4]; // Håller blockets gammla position

        public static void RandomBlock()
        {
            blockholder = nextBlock; // Blockholder får nextBlocks värde.
            nextBlock = randomnummer.Next(0, 7); //nextBlock slumpas
        }

        public static void UpdateBlockRotation() //Funktion för blockrotation
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !wpressed) //Om man trycker ner W-tangenten och bool wpressed är false.
            {
                wpressed = true; // Då blir wpressed true och rot ökar med 1.
                rot++;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.W)) //Om man släpper W-tangenten blir wpressed false igen.
            {
                wpressed = false;
            }

            if (rot > 3) //Om rot blir mer än 3.
            {
                rot = 0; //Blir rot 0 igen.
            }
                        
        }

        public static void UpdateBlockMovement(GameTime gameTime) //Funktion för blockets rörelse
        { 
            if (Keyboard.GetState().IsKeyDown(Keys.S)) //Om S är nedtryckt faller blocket ett miliTimer milisekunder / 3.
            {
                Score.BlueBlockSpeed = (700 - (100 * Score.Level)) / 3; 
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) //Om space (Dunk knappen) är nedtryckt...
            {
                if (!newBlock) //Om det inte har kommit ett nytt blått block.
                {
                    blockPosY += ContentHandler.blueBlockTexture.Height; // Ökas Y positionen efter varje frame.
                    Score.score += 5; // Spelaren får poäng.
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space)) //Om space inte är nedtryckt...
            {
                newBlock = false; // Blir newBlock falskt
            }
                                   
        }

        public static void DrawBlock(SpriteBatch spriteBatch) // Funktion för att rita ut blocken.
        {
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(ContentHandler.blueBlockTexture,
                                 new Vector2(260 + BlockLists.allaBlockLista[blockholder][i + (rot * 4)].X * ContentHandler.blueBlockTexture.Width + blockPosX, 
                                             100 + BlockLists.allaBlockLista[blockholder][i + (rot * 4)].Y * ContentHandler.blueBlockTexture.Height + blockPosY),
                                 Color.White);
                //allaBlockLista[blockholder] hör till RandomBlock funktionen, blockholder bestämmer vilken blocklista det blir av alla blocklistor.
                //i står för varje enskilt block i ett sammansatt block, rot * 4 står för rotationen på ett sammansatt block.
                //260 = skärmbredd(800)/2 - spelplansbredd(280)/2
                //100 = skärmhöjd(600)/2 - spelplanshöjd(400)/2
            }
        }

    }
}
