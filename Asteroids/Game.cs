using System;
using System.Drawing;
using System.IO;
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
        private static BaseObject[] objs;
        private static Bullet bullet;
        private static Asteroid[] asteroids;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Ypos { get; set; }
        public static int Delta { get; set; }

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
                Load();
                Timer timer = new Timer { Interval = 100 };
                timer.Start();
                Ypos = Control.MousePosition.Y;
                timer.Tick += Timer_Tick;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Delta = Ypos - Control.MousePosition.Y;
            Ypos = Control.MousePosition.Y;
            Draw();
            Update();
        }

        public static void Load()
        {
            objs = new BaseObject[120];
            Random rnd = new Random();
            bullet = new Bullet(new Point(0, Height / 2), new Point(5, 0), new Size(4, 1));
            asteroids = new Asteroid[3];
            for (int i = 0; i < asteroids.Length; i++)
            {
                int r = rnd.Next(15, 25);
                asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Height)),
                               new Point(-r, r), new Size(r, r));
            }
            Image star = Image.FromFile("star.jpg");
            Image comet = Image.FromFile("comet.jpg");
            for (int i = 0; i < objs.Length / 2; i++)
            {
                int r = rnd.Next(4, 8);
                objs[i] = new Star(star, new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(rnd.Next(r, r), rnd.Next(-r, r)), new Size(r, r));
            }
            for (int i = objs.Length / 2; i < objs.Length; i++)
            {
                int r = rnd.Next(3, 6);
                objs[i] = new Comet(comet, new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(rnd.Next(r, r), rnd.Next(-r, r)), new Size(r, r));
            }
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            foreach (Asteroid a in asteroids)
                a.Draw();
            bullet.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
            foreach (Asteroid a in asteroids)
            {
                a.Update();
                if (a.Collision(bullet))
                {
                    Console.Beep();
                    a.Reset();
                    bullet.Reset();
                }
            }
            bullet.Move(Delta);
            bullet.Update();
        }
    }
}