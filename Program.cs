using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataBase dataBase = new DataBase("dados.xml");
            Grapichs menu = new Grapichs(dataBase);
            menu.IniciaInterface();
            Console.ReadKey();
        }
    }
}
