using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT154_Libs
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = Data.getUserByEmail("paalvaa@gmail.com");
            Console.WriteLine(user.email);

            Console.ReadKey();
        }
    }
}