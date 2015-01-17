using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraageCounterTest.Controllers
{
    internal class Class1
    {
        public class Foo
        {
            public String Bar { get; set; }
        }

        public void test()
        {
            Method1();
            Method2();
        }

        public static void Method1()
        {
            Foo foofoo = null;
            try
            {
                foofoo = new Foo();
                foofoo.Bar = Throw();
            }
            catch
            {
                Console.WriteLine(foofoo == null ? "nullです" : "nullじゃないです");
            }

        }

        public static void Method2()
        {
            Foo foofoo = null;
            try
            {
                foofoo = new Foo()
                {
                    Bar = Throw()
                };
            }
            catch
            {
                Console.WriteLine(foofoo == null ? "nullです" : "nullじゃないです");
            }
        }

        public static string Throw()
        {
            throw new Exception();
        }
    }

}

