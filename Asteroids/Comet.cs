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
    class Comet : BaseObject
    {
        public Comet(Image img, Point pos, Point dir, Size size) : base(img, pos, dir, size)
        {
        }

        public Comet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            if (Img != null) Game.Buffer.Graphics.DrawImage(Img, Pos.X, Pos.Y, Size.Width, Size.Height);
            else Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Dir.X > 0 && Pos.X > Game.Width) Pos.X = -Size.Width;
            if (Dir.X < 0 && Pos.X < 0) Pos.X = Game.Width + Size.Width;
            Pos.Y = Pos.Y + Dir.Y;
            if (Dir.Y > 0 && Pos.Y > Game.Height) Pos.Y = -Size.Height;
            if (Dir.Y < 0 && Pos.Y < 0) Pos.Y = Game.Height + Size.Height;
        }
    }
}