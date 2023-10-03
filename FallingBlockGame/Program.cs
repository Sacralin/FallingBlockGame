using System;
using System.Drawing;

namespace FallingBlockGame
{
    internal class Program
    {
        static Player player;
        static List<Block> blocks;


        static Random random = new Random();
        static bool isRunning = true;
        static DateTime lastUpdateTime = DateTime.Now;
        static double deltaTime;
        static double gameTimeElapsed;
        static double blockTimeElapsed;
        static double blockSpawnTimer = 5000;

        static int windowWidth = 40;
        static int windowHeight = 20;

        static void Main(string[] args)
        {
            Console.BufferHeight = Console.WindowHeight * 2;
            Console.CursorVisible = false;
            player = new Player (windowWidth / 2, windowHeight -1, "@", 100);
            blocks = new List<Block>();

            while (isRunning)
            {
                DateTime currentTime = DateTime.Now;
                deltaTime = (currentTime - lastUpdateTime).TotalMilliseconds;
                lastUpdateTime = currentTime;
                gameTimeElapsed += deltaTime;
                blockTimeElapsed += deltaTime;
                Update();
                Draw();
                Thread.Sleep(100);
            }
        }

        private static void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {

                    case ConsoleKey.Q:
                        isRunning = false;
                        break;
                    case ConsoleKey.LeftArrow:
                        player.Move(-1, 0, windowWidth, windowHeight);
                        break;
                    case ConsoleKey.RightArrow:
                        player.Move(1, 0, windowWidth, windowHeight);
                        break;
                    default:
                        break;
                }
            }

            foreach (Block block in blocks)
            {
                block.Update(block, windowWidth, windowHeight);
                player.CheckCollisionWith(block);
                //if(block.Y == windowHeight)
                //{
                //    blocks.Remove(block);
                //}

            }
            blocks.RemoveAll(block => player.X == block.X && player.Y == block.Y); // not sure how this works so look into it 

            if (blockTimeElapsed >= blockSpawnTimer)
            {
                int speed = random.Next(1, 3);
                int x = random.Next(0, windowWidth);
                blocks.Add(new Block(x, 0, "X", speed));
                blockTimeElapsed = 0;
            }
        }

        private static void Draw()
        {
            Console.Clear();

            player.Draw();

            foreach (Block block in blocks)
            {
                block.Draw();
            }
            Console.SetCursorPosition(0, 21);
            Console.WriteLine($"Time elapsed (ms): {Math.Round(gameTimeElapsed / 1000)}");
            Console.WriteLine($"Player HP: {player.HP}");
        }

        
    }
}