using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpLab3
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Person p1 = new Person("Vladimir", "Ahakov", new DateTime(2000, 11, 13));
            Copywriter c1 = new Copywriter(p1, "King", (Level)1, 4);
            Copywriter test = new Copywriter();
            Copywriter test1 = new Copywriter();
            c1.AddArticle(new Article("Staty_1", 150, new DateTime(2000, 07, 31)));
            c1.AddArticle(new Article("Staty_2", 267, new DateTime(2001,03,01)));
            c1.AddArticle(new Article("Staty_3", 73, new DateTime(1999,09,17)));

            Copywriter copyc1 = c1.DeepCopy();
            copyc1.AddArticle(new Article("ADDED", 175, new DateTime(2000, 08, 11)));
            Console.WriteLine("ORIGINAL: \n\n");
            Console.WriteLine(c1.ToString()+"\n\n");
            Console.WriteLine("CHANGED DEEP COPY: \n\n");
            Console.WriteLine(copyc1.ToString() + "\n\n");

            Console.WriteLine("Please, enter filename: ");
            string file = Console.ReadLine();
            string path = @"C:\Users\Varde\source\repos\sharpLab3\sharpLab3\bin\Debug\" +file+ ".dat";
            if (!File.Exists(path))
            {
                Console.WriteLine("No such file! Let me create one for example\nCreating...");
                copyc1.Save(file);
                test.Load(file);
            }
            else
            {
                Console.WriteLine("Found your file!\nLoading...");
                test.Load(file);
            }
            Console.WriteLine(test.ToString() + "\n\n");
            test.AddFromConsole();
            test.Save(path);
            Console.WriteLine(test.ToString() + "\n\n");
            Copywriter.Load(path,test);
            test.AddFromConsole();
            Copywriter.Save(path, test);
            Copywriter.Load(path, test1);
            Console.WriteLine(test1.ToString() + "\n\n");
            Console.ReadKey();
        }
    }
}
