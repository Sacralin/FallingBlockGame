using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingBlockGame
{
    internal class GameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Sprite { get; private set; }

        public GameObject(int x, int y, string sprite)
        {
            X = x;
            Y = y;
            Sprite = sprite;
        }

        public virtual void Move(int dx, int dy, int maxX, int maxY)
        {
            int newX = X + dx;
            int newY = Y + dy;

            if (newX >= 0 && newX < maxX)
            {
                X = newX;
            }

            if (newY >= 0 && newY < maxY)
            {
                Y = newY;
            }
            //else if (newY > maxX) newY = maxX;
        }

        public virtual void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Sprite);
        }


        
    }

    class Player : GameObject
    {
        public int HP { get; private set; }
        public Player(int x, int y, string sprite, int hp) : base(x, y, sprite)
        {
            HP = hp;
        }

        public void CheckCollisionWith(GameObject other)
        {
            if (X == other.X && Y == other.Y)
            {

                HP -= 1;
            }
        }

        public override void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Sprite);
            Console.ResetColor();
        }

    }

    class Block : GameObject
    {
        public int Speed { get; private set; }
        private int frameCounter;

        public Block(int x, int y, string sprite, int speed) : base(x, y, sprite)
        {
            Speed = speed;
            frameCounter = 0;
        }


        public void Update(Block block, int maxX, int maxY)
        {
            frameCounter++;
            if (frameCounter % Speed == 0) // % looks at the decimal number.
            {
                Move(0, 1, maxX, maxY);
                frameCounter = 0;
            }
        }
        public override void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Sprite);
            Console.ResetColor();
        }

    }
}
