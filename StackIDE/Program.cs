using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackIDE {
    class Program {
        static void Main(string[] args) {
            Channel ch = new Channel();
            ch.Register("test", () => Debug.Print("Testing"));            
            ch.Broadcast("test");

            Stack stack = new Stack();
            stack.Push("3");
            stack.Push("5");
            stack.Push("+");
            stack.Push("22");
            stack.Push("*");
            stack.Push("3.3");
            stack.Push("pow");
 
        }

        private static  void reflectorTest() {
            Reflector reflector = new Reflector();
            string code = @"
                using System.Collections.Generic;
                namespace StackIDE {
                class test {
                public string eval(List<string> i){
                    double sum = 0;
                    foreach (var a in i) {
                        sum += double.Parse(a);
                    }
                    return sum.ToString();
                    }
                   }
                }";
            string function =
                @"public string eval(List<string> i){
                    double sum = 0;
                    foreach (var a in i) {
                        sum += double.Parse(a);
                    }
                    return sum.ToString();
                    }";
            var parameters = new List<string>() { "44", "58" };
            var output = reflector.ExecuteCode(code, "StackIDE", "test", "eval", false, parameters);
            var output2 = reflector.ExecuteFunction(function, "eval", parameters);
        }
    }
}
