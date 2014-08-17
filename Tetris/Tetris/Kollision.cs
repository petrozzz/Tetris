/* Klass HighScoreClass är skriven av Niklas Sjöqvist (nst12002@student.mdh.se)
 * Kurs: Programmeringsteknik i c# (DVA105)
 * Klassen hanterar Kollision mellan blocken och spelplanen. 
 */
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    class Kollision
    {
        public static void RotationsKollision() //Kollar kollison med väggar när block roterar.
        {
            int i = 0;//räknare
            bool collision = false;

            while (i < 4) //kollision med sidan till vänster för rött och blått block
            {
                collision = true; //start värde på Collision
                while (collision == true)//loop för kollision för blått block
                {
                    collision = false; //Collision blir false så att den inte kommer loopa ett varv till
                    if (BlockLists.allaBlockLista[BlueBlock.blockholder][i + (BlueBlock.rot * 4)].X * ContentHandler.blueBlockTexture.Width + BlueBlock.blockPosX < 0)//om blocket finns på en position mindre än 0 (ute till vänster om spelplanen)
                    {
                        BlueBlock.blockPosX += ContentHandler.blueBlockTexture.Width; //flyttas blocket ett steg till höger (+1)
                        collision = true; //om blocket är utanför testa igen om den är utanför ifall den var mer än en ett steg utanför
                    }
                }

                collision = true; //start värde på Collision
                while (collision == true)//loop för kollision för rött block
                {
                    collision = false; //Collision blir false så att den inte kommer loopa ett varv till
                    if (BlockLists.allaBlockLista[RedBlock.blockholder][i + (RedBlock.rot * 4)].X * ContentHandler.blueBlockTexture.Width + RedBlock.blockPosX < 0)//om blocket finns på en position mindre än 0 (ute till vänster om spelplanen)
                    {
                        RedBlock.blockPosX += ContentHandler.blueBlockTexture.Width;//flyttas blocket ett steg till höger (+1)
                        collision = true; //om blocket är utanför testa igen om den är utanför ifall den var mer än en ett steg utanför
                    }
                }
                i++; //ökar värdet på i så att alla 4 del block i de båda blocken testas
            }

            i = 0;//i blir 0 igen för att återanvändas i nästa loop

            while (i < 4) //kollision med sidan till höger för rött och blått block
            {
                collision = true; //start värde på Collision
                while (collision == true)//loop för kollision för blått block
                {
                    collision = false;//Collision blir false så att den inte kommer loopa ett varv till
                    if (BlockLists.allaBlockLista[BlueBlock.blockholder][i + (BlueBlock.rot * 4)].X * ContentHandler.blueBlockTexture.Width + BlueBlock.blockPosX > 13 * ContentHandler.blueBlockTexture.Width)//om blocket finns på en position större än 13 gånger block bredden (ute till höger om spelplanen)
                    {
                        BlueBlock.blockPosX -= ContentHandler.blueBlockTexture.Width;//flyttas blocket ett steg till vänster (-1)
                        collision = true;//om blocket är utanför testa igen om den är utanför ifall den var mer än en ett steg utanför
                    }

                }
                collision = true;//start värde på Collision
                while (collision == true)//loop för kollision för rött block
                {
                    collision = false;//Collision blir false så att den inte kommer loopa ett varv till
                    if (BlockLists.allaBlockLista[RedBlock.blockholder][i + (RedBlock.rot * 4)].X * ContentHandler.blueBlockTexture.Width + RedBlock.blockPosX > 13 * ContentHandler.blueBlockTexture.Width)//om blocket finns på en position större än 13 gånger block bredden (ute till höger om spelplanen)
                    {
                        RedBlock.blockPosX -= ContentHandler.blueBlockTexture.Width;//flyttas blocket ett steg till vänster (-1)
                        collision = true;//om blocket är utanför testa igen om den är utanför ifall den var mer än en ett steg utanför
                    }
                }
                i++;//ökar värdet på i så att alla 4 del block i de båda blocken testas
            }
        }

        public static void Collision()
        {
            int redNextBlock = 0; //int istället för bool (gav error när det var bool) där 0 är false och 1 är true. När den är true så slumpas ett nytt rött block
            int blueNextBlock = 0; //int istället för bool (gav error när det var bool) där 0 är false och 1 är true. När den är true så slumpas ett nytt blått block

            for (int i = 0; i < 4; i++)//alla fyra delblock testas ett åt gången
            {
                if (BlockLists.allaBlockLista[BlueBlock.blockholder][i + (BlueBlock.rot * 4)].Y + BlueBlock.blockPosY / ContentHandler.blueBlockTexture.Height > 19)//om det blå blocket gått i botten (position 20)
                {
                    BlueBlock.blockPosY = BlueBlock.blockPosY - 20; //Backa till den gamla positionen
                    blueNextBlock = 1;//när blocket fastnat i botten ska det komma ett nytt block 
                    ljudklass.Block(); //spelar upp ljud när block sätts fast i botten på spelplanen

                }
                if (BlockLists.allaBlockLista[RedBlock.blockholder][i + (RedBlock.rot * 4)].Y + RedBlock.blockPosY / ContentHandler.redBlockTexture.Height > 19)//om det röda blocket gått i botten (position 20)
                {
                    RedBlock.blockPosY = RedBlock.blockPosY - 20; //Backa till den gamla positionen
                    redNextBlock = 1;//när blocket fastnat i botten ska det komma ett nytt block 
                    ljudklass.Block(); //spelar upp ljud när block sätts fast i botten på spelplanen

                }
            }


            for (int counter = 0; counter < 4; counter++)//Loopa igenom blocken för att kolla kollision mellan block
            {
                if (GameField.playfield[BlockLists.allaBlockLista[BlueBlock.blockholder][counter + (BlueBlock.rot * 4)].X + BlueBlock.blockPosX / ContentHandler.blueBlockTexture.Width, BlockLists.allaBlockLista[BlueBlock.blockholder][counter + (BlueBlock.rot * 4)].Y + 1 + BlueBlock.blockPosY / ContentHandler.blueBlockTexture.Height] >= 1)//kollar om det finns ett block på positionen under det blå blocket
                {
                    blueNextBlock = 1; //kommer nytt blått block
                }
                if (GameField.playfield[BlockLists.allaBlockLista[RedBlock.blockholder][counter + (RedBlock.rot * 4)].X + RedBlock.blockPosX / ContentHandler.blueBlockTexture.Width, BlockLists.allaBlockLista[RedBlock.blockholder][counter + (RedBlock.rot * 4)].Y + 1 + RedBlock.blockPosY / ContentHandler.blueBlockTexture.Height] >= 1)//kollar om det finns ett block på positionen under det röda blocket
                {
                    redNextBlock = 1; //kommer nytt rött block
                }
            }

            if (blueNextBlock == 1) //om det ska komma ett nytt blått block
            {
                BlueBlock.newBlock = true;//förhindrar dunkfunktionen från att fortsätta

                for (int counter2 = 0; counter2 < 4; counter2++)//loop för att testa kollision med alla fyra del-block (punkter som blocken består av)
                {
                    GameField.playfield[BlockLists.allaBlockLista[BlueBlock.blockholder][counter2 + (BlueBlock.rot * 4)].X + BlueBlock.blockPosX / ContentHandler.blueBlockTexture.Width, BlockLists.allaBlockLista[BlueBlock.blockholder][counter2 + (BlueBlock.rot * 4)].Y + BlueBlock.blockPosY / ContentHandler.blueBlockTexture.Height] = 1;//fäster blocket i playfield på dess nuvarande position, 1 säger att det är blått
                }
                blueNextBlock = 0; //inte nytt block nästa gång
                BlueBlock.blockPosY = 0; //ny y-pos (högst upp)
                BlueBlock.blockPosX = 20; //nytt block på x-position 20 (1 i playfield)
                BlueBlock.RandomBlock(); //nytt block slumpas
                ljudklass.Block(); //ljud när block blir låsta mot varandra
            }
            if (redNextBlock == 1) //om det ska komma ett nytt rött block
            {
                RedBlock.newBlock = true;//förhindrar dunkfunktionen från att fortsätta

                for (int counter2 = 0; counter2 < 4; counter2++)//loop för att testa kollision med alla fyra del-block (punkter som blocken består av)
                {
                    GameField.playfield[BlockLists.allaBlockLista[RedBlock.blockholder][counter2 + (RedBlock.rot * 4)].X + RedBlock.blockPosX / ContentHandler.blueBlockTexture.Width, BlockLists.allaBlockLista[RedBlock.blockholder][counter2 + (RedBlock.rot * 4)].Y + RedBlock.blockPosY / ContentHandler.blueBlockTexture.Height] = 2;//fäster blocket i playfield på dess nuvarande position, 2 säger att det är rött
                }
                redNextBlock = 0; //inte nytt block nästa gång
                RedBlock.blockPosY = 0;//ny y-pos (högst upp)
                RedBlock.blockPosX = 220; //nytt block på x-position 220 (11 i playfield)
                RedBlock.RandomBlock(); //nytt block slumpas
                ljudklass.Block(); //ljud när block blir låsta mot varandra
            }


        }

        public static void BlueLeftRightCollision() //kollision i sidled mellan blåa block och blåa block mot sida
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A) && !BlueBlock.apressed)//om knapp A ned tryckt (gå till vänster blått block) 
            {
                BlueBlock.apressed = true;
                int i = 0;//räknare

                //Denna try catch kollar om man får flytta alla fyra block eller om ett krockar med kanten av spelplanen
                try //Prova köra loopen
                {
                    //Loopen körs så länge det inte är kollision, när den kolliderar krashar loopen och vi fångar det i catch
                    for (; (BlockLists.allaBlockLista[BlueBlock.blockholder][i + (BlueBlock.rot * 4)].X * ContentHandler.blueBlockTexture.Width + BlueBlock.blockPosX > 0 * ContentHandler.blueBlockTexture.Width) && i <= 3; i++) ;
                }
                catch //Loopen krashar när det kolliderar, vi fångar det med catch
                {

                    if (i == 4) //är i = 4 är det inte kollision och man får flytta
                    {
                        BlueBlock.blockPosX = BlueBlock.blockPosX - ContentHandler.blueBlockTexture.Width;//blocket går ett steg till vänster
                        i++;
                    }
                }


                for (int counter = 0; counter < 4; counter++)//Loopa igenom blocken för att kolla kollision med annat block åt vänster
                {
                    try
                    {
                        if (GameField.playfield[BlockLists.allaBlockLista[BlueBlock.blockholder][counter + (BlueBlock.rot * 4)].X - 1 + BlueBlock.blockPosX / ContentHandler.blueBlockTexture.Width,
                                                BlockLists.allaBlockLista[BlueBlock.blockholder][counter + (BlueBlock.rot * 4)].Y + BlueBlock.blockPosY / ContentHandler.blueBlockTexture.Height] >= 1)
                        {
                            i = 0;
                        }
                    }
                    catch { }
                }

                if (i == 4) //är i = 4 är det inte kollision och man får flytta rött block till vänster
                {
                    BlueBlock.oldBlockPosX = BlueBlock.blockPosX;
                    BlueBlock.blockPosX = BlueBlock.blockPosX - ContentHandler.blueBlockTexture.Width;
                    i++;
                }

            }

            if (Keyboard.GetState().IsKeyUp(Keys.A) && BlueBlock.apressed)//Knapp tryckningen blir false så att den bara går ett steg om man håller in knappen
            {
                BlueBlock.apressed = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && !BlueBlock.dpressed)// gå till höger blått block
            {
                int i = 0;//räknare
                BlueBlock.dpressed = true;

                try //försöker köra loopen
                {
                    for (; (BlockLists.allaBlockLista[BlueBlock.blockholder][i + (BlueBlock.rot * 4)].X * ContentHandler.blueBlockTexture.Width + BlueBlock.blockPosX < 13 * ContentHandler.blueBlockTexture.Width) && i <= 3; i++) ; //kollar kollision med sida för block 0 till 3 i listan ggr 4(för rotation) bryter när den blir 4 eller kollision uppstår
                }
                catch //loopen kraschar vid 4 då 3 är den sissta variabeln i listan vid rotation 3, 4 finns alltså inte, därför catch
                {
                    if (i == 4) //är i = 4 är det inte kollision och man får flytta
                    {
                        BlueBlock.blockPosX = BlueBlock.blockPosX + ContentHandler.blueBlockTexture.Width;//ett steg till höger
                        i++;
                    }
                }

                for (int counter = 0; counter < 4; counter++)//Loopa igenom blocken för att kolla kollision till höger med annat block
                {
                    try
                    {
                        if (GameField.playfield[BlockLists.allaBlockLista[BlueBlock.blockholder][counter + (BlueBlock.rot * 4)].X + 1 + BlueBlock.blockPosX / ContentHandler.blueBlockTexture.Width,
                                                BlockLists.allaBlockLista[BlueBlock.blockholder][counter + (BlueBlock.rot * 4)].Y + BlueBlock.blockPosY / ContentHandler.blueBlockTexture.Height] >= 1)
                        {
                            i = 0;
                        }
                    }
                    catch { }
                }
                if (i == 4) //är i = 4 är det inte kollision och man får flytta blått block till höger
                {

                    BlueBlock.oldBlockPosX = BlueBlock.blockPosX;
                    BlueBlock.blockPosX = BlueBlock.blockPosX + ContentHandler.blueBlockTexture.Width;
                }

            }

            if (Keyboard.GetState().IsKeyUp(Keys.D) && BlueBlock.dpressed)//Knapp tryckningen blir false så att den bara går ett steg om man håller in knappen
            {
                BlueBlock.dpressed = false;
            }
        }

        public static void RedLeftRightCollision()//kollision i sidled mellan röda block och röda block mot sida
        {

            if (Keyboard.GetState().IsKeyDown(Keys.J) && !RedBlock.jpressed)//om j (rött block åt vänster)
            {
                RedBlock.jpressed = true;
                int i = 0;//räknare

                //Denna try catch kollar om man får flytta alla fyra block eller om ett krockar 
                try //försöker köra loopen
                {
                    //kollar kollision med sida för block 0 till 3 i listan ggr 4(för rotation) bryter när den blir 4 eller kollision uppstår
                    for (; (BlockLists.allaBlockLista[RedBlock.blockholder][i + (RedBlock.rot * 4)].X * ContentHandler.blueBlockTexture.Width + RedBlock.blockPosX > 0 * ContentHandler.blueBlockTexture.Width) && i <= 3; i++) ;
                }
                catch //Loopen kraschar vid 4 då 3 är den sista variabeln i listan vid rotation 3, 4 finns alltså inte, därför catch
                {

                    if (i == 4) //är i = 4 är det inte kollision och man får flytta
                    {
                        RedBlock.blockPosX = RedBlock.blockPosX - ContentHandler.redBlockTexture.Width;
                        i++;
                    }
                }

                for (int counter = 0; counter < 4; counter++)//Loopa igenom blocken för att kolla kollision
                {
                    try
                    {
                        if (GameField.playfield[BlockLists.allaBlockLista[RedBlock.blockholder][counter + (RedBlock.rot * 4)].X - 1 + RedBlock.blockPosX / ContentHandler.blueBlockTexture.Width,
                                                BlockLists.allaBlockLista[RedBlock.blockholder][counter + (RedBlock.rot * 4)].Y + RedBlock.blockPosY / ContentHandler.blueBlockTexture.Height] >= 1)
                        {
                            i = 0;
                        }
                    }
                    catch { }
                }

                if (i == 4) //är i = 4 är det inte kollision och man får flytta rött block till vänster
                {
                    RedBlock.oldBlockPosX = RedBlock.blockPosX;
                    RedBlock.blockPosX = RedBlock.blockPosX - ContentHandler.redBlockTexture.Width;
                }


            }

            if (Keyboard.GetState().IsKeyUp(Keys.J) && RedBlock.jpressed)//Knapp tryckningen blir false så att den bara går ett steg om man håller in knappen
            {
                RedBlock.jpressed = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L) && !RedBlock.lpressed)
            {
                int i = 0;//räknare
                RedBlock.lpressed = true;

                try //försöker köra loopen
                {
                    for (; (BlockLists.allaBlockLista[RedBlock.blockholder][i + (RedBlock.rot * 4)].X * ContentHandler.blueBlockTexture.Width + RedBlock.blockPosX < 13 * ContentHandler.blueBlockTexture.Width) && i <= 3; i++) ; //kollar kollision med sida för block 0 till 3 i listan ggr 4(för rotation) bryter när den blir 4 eller kollision uppstår
                }
                catch //loopen kraschar vid 4 då 3 är den sissta variabeln i listan vid rotation 3, 4 finns alltså inte, därför catch
                {
                    if (i == 4) //är i = 4 är det inte kollision och man får flytta
                    {
                        RedBlock.blockPosX = RedBlock.blockPosX + ContentHandler.redBlockTexture.Width;
                        i++;
                    }
                }

                for (int counter = 0; counter < 4; counter++)//Loopa igenom blocken för att kolla kollision
                {
                    try
                    {
                        if (GameField.playfield[BlockLists.allaBlockLista[RedBlock.blockholder][counter + (RedBlock.rot * 4)].X + 1 + RedBlock.blockPosX / ContentHandler.blueBlockTexture.Width,
                                                BlockLists.allaBlockLista[RedBlock.blockholder][counter + (RedBlock.rot * 4)].Y + RedBlock.blockPosY / ContentHandler.blueBlockTexture.Height] >= 1)
                        {
                            i = 0;
                        }
                    }
                    catch { }
                }
                if (i == 4) //är i = 4 är det inte kollision och man får flytta rött block till höger
                {
                    RedBlock.oldBlockPosX = RedBlock.blockPosX;
                    RedBlock.blockPosX = RedBlock.blockPosX + ContentHandler.redBlockTexture.Width;
                }

            }

            if (Keyboard.GetState().IsKeyUp(Keys.L) && RedBlock.lpressed)//Knapp tryckningen blir false så att den bara går ett steg om man håller in knappen
            {
                RedBlock.lpressed = false;
            }
        }


    }
}
