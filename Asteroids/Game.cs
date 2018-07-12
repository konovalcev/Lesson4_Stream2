using System;
using System.Drawing;
using System.Windows.Forms;

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
    static class Game
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics Buffer;
        private static BaseObject[] _objs;
        private static Asteroid[] _asteroids;
        private static Bullet _bullet;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int ShipPosition { get; set; }
        public static int Delta { get; set; }
        private static readonly Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));        private static Timer _timer = new Timer { Interval = 100 };
        public static Random Rnd = new Random();

        static Game()
        {
        }

        public static void Init(Form form)
        {
            try
            {
                Graphics g;
                context = BufferedGraphicsManager.Current;
                g = form.CreateGraphics();
                Width = (form.ClientSize.Width > 0 && form.ClientSize.Width < 1000) ? form.ClientSize.Width : throw new ArgumentOutOfRangeException("Incorrect size of form");
                Height = (form.ClientSize.Height > 0 && form.ClientSize.Height < 1000) ? form.ClientSize.Height : throw new ArgumentOutOfRangeException("Incorrect size of form");
                Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
                _timer.Start();
                ShipPosition = Control.MousePosition.Y;
                form.KeyDown += Form_KeyDown;
                _timer.Tick += Timer_Tick;
                Ship.MessageDie += Finish;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Delta = ShipPosition - Control.MousePosition.Y;
            ShipPosition = Control.MousePosition.Y;
            Draw();
            Update();
        }

        public static void Load()
        {
            _objs = new BaseObject[25];
            Random rnd = new Random();
            _bullet = new Bullet(new Point(0, Height / 2), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[3];
            for (int i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(15, 25);
                _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Height)),
                               new Point(-r, r), new Size(r, r));
            }
            Image star = Image.FromFile("star.jpg");
            Image comet = Image.FromFile("comet.jpg");
            for (int i = 0; i < _objs.Length / 2; i++)
            {
                int r = rnd.Next(4, 8);
                _objs[i] = new Star(star, new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(rnd.Next(r, r), rnd.Next(-r, r)), new Size(r, r));
            }
            for (int i = _objs.Length / 2; i < _objs.Length; i++)
            {
                int r = rnd.Next(3, 6);
                _objs[i] = new Comet(comet, new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(rnd.Next(r, r), rnd.Next(-r, r)), new Size(r, r));
            }
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid a in _asteroids)
                a?.Draw();
            _bullet?.Draw();
            _ship?.Draw();
            if (_ship != null)
            {
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            }
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            _bullet?.Update();
            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _asteroids[i] = null;
                    _bullet = null;
                    continue;
                }
                if (!_ship.Collision(_asteroids[i])) continue;
                var rnd = new Random();
                _ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship?.Die();
            }
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _bullet = new Bullet(new
            Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
    }
}