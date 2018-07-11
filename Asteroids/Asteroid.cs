using System;
using System.Drawing;

//1. Добавить космический корабль, как описано в уроке.
//2. Добработать игру «Астероиды».
//а) Добавить ведение журнала в консоль с помощью делегатов;
//б) * Добавить это и в файл.
//3. Разработать аптечки, которые добавляют энергию.
//4. Добавить подсчет очков за сбитые астероиды.
//5. *Добавить в пример Lesson3 обобщенный делегат.


// Александр КОновальцев

namespace Asteroids
{
    class Asteroid : BaseObject
    {
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Dir.X > 0 && Pos.X > Game.Width) Pos.X = -Size.Width;
            if (Dir.X < 0 && Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }

        public override void Reset()
        {
            Pos.X = Game.Width + Size.Width;
            Random rnd = new Random();
            Pos.Y = rnd.Next(0, Game.Height);
        }
    }
}