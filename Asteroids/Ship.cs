using System.Drawing;

namespace Asteroids
{
    class Ship : BaseObject
    {
        public static event Message MessageDie;
        public int Energy { get; private set; } = 100;

        public void EnergyLow(int n)
        {
            Energy -= n;
        }
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Die()
        {
            MessageDie?.Invoke();
        }
    }
}