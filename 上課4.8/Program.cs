using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 上課4._8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("plz input UR name  ");
            string name = Console.ReadLine();
            MessageBox.Show("hellow  " + name);
            //full name
            System.Windows.Forms.MessageBox.Show("hellow,fullname");
        }
    }
}
