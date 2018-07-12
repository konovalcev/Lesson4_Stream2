using System;
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
    static class Progam
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please, enter game window width");
                int.TryParse(Console.ReadLine(), out int width);              
                Console.WriteLine("Please, enter game window height");
                int.TryParse(Console.ReadLine(), out int height);
                Form form = new Form() { Width = width, Height = height };
                Game.Init(form);
                form.Show();
                Game.Load();
                Game.Draw();
                Application.Run(form);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}