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
    abstract class BaseObject : ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        protected Image Img;

        protected BaseObject(Image img, Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
            Img = img;
        }

        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public abstract void Draw();
        public abstract void Update();
        public virtual void Reset()
        {
            Pos.X = -Size.Width;
        }
        public virtual void Move(int delta)
        {
            Pos.Y = Pos.Y - delta;
        }
        public Rectangle Rect => new Rectangle(Pos, Size);
        public bool Collision(ICollision obj) => Rect.IntersectsWith(obj.Rect);
    }
}