using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackIDE {
    class FunctionLib {
        private static StackFunction AddFunction(int paramCount, evaluator eval, string description, params string[] names) {
            return new StackFunction() { Eval = eval, ParamCount = paramCount, Description = description, Names = names };
            
        }

        public static IEnumerable<StackFunction> GetFunctions() {
            yield return AddFunction(2,
                (i, j) => {
                    double sum = 0;
                    try {
                        double a = double.Parse(j.First());
                        double b = double.Parse(j.Last());
                        sum = a + b;
                    } catch {
                        return j.First() + j.Last();
                    }
                    return sum.ToString();
                }, "returns a + b", "+"
                );
            yield return AddFunction(2,
                (i, j) => {
                    double sum = 0;
                    foreach (var a in j) {
                        sum += double.Parse(a);
                    }
                    return sum.ToString();
                }, "returns a - b", "-"
                );

            yield return AddFunction(2,
                (i, j) => {
                    double a = double.Parse(j.First());
                    double b = double.Parse(j.Last());
                    return (a * b).ToString();
                }, "returns a * b", "*"
                );
            yield return AddFunction(2,
                (i, j) => {
                    double a = double.Parse(j.First());
                    double b = double.Parse(j.Last());
                    return (a / b).ToString();
                }, "returns a / b", "/"
                );
            yield return AddFunction(2,
                (i, j) => {
                    double a = double.Parse(j.First());
                    double b = double.Parse(j.Last());
                    return Math.Pow(a, b).ToString();
                }, "returns a^b", "pow"
                );
            yield return AddFunction(0,
                (i, j) => {
                    return i.TokenStack.First();
                }, "Duplicates the top element on the stack", "dup"
                );
            yield return AddFunction(0,
                (i, j) => {
                    i.Push("2");
                    i.Push("*");
                    return "";
                }, "Doubles the top element", "*2"
                );
            yield return AddFunction(0,
                (i, j) => {
                    i.TokenStack.Pop();
                    return "";
                }, "Pops the top element off the stack", "pop"
                );
            yield return AddFunction(0,
                (i, j) => {
                    string output = "";
                    while (i.TokenStack.Count() > 0) {
                        output += i.TokenStack.Pop();
                    }
                    return output;
                }, "Concatinates all elements in the stack", "concat"
                );
            yield return AddFunction(1,
                (i, j) => {
                    var delim = j.First();
                    string output = "";
                    while (i.TokenStack.Count() > 0) {
                        output += i.TokenStack.Pop() + delim;
                    }
                    return output;
                }, "Concatinates all elements in the stack using a specified delimiter", "concat_delim"
                );
            yield return AddFunction(2,
                (i, j) => {
                    var delimiters = j.First().ToCharArray();
                    var content = j.Last();
                    var items = content.Split(delimiters);
                    foreach (var item in items) {
                        i.Push(item, false);
                    }
                    return "";
                }, "Splits the string using a specified delimiter", "split"
                );
            yield return AddFunction(0,
                (i, j) => {
                    return rand.NextDouble().ToString();
                }, "Pushes a random double to the stack", "randDouble"
                );
            yield return AddFunction(2,
                (i, j) => {
                    try {
                        var filename = j.First();
                        var content = j.Last();

                        System.IO.File.WriteAllText(filename, content);
                        ///will return sucess or failure
                        return "";
                    } catch {
                        return "failure";
                    }
                }, "Saves element b to disk with filename a", "save"
                );
            yield return AddFunction(0,
                (i, j) => {
                    i.TokenStack.Clear();
                    return "";
                }, "Clears the stack", "clear"
                );
            yield return AddFunction(1,
                (i, j) => {
                    try {
                        var filename = j.First();
                        return System.IO.File.ReadAllText(filename);
                    } catch {
                        return j.First();
                    }
                }, "Opens a specified text file", "open"
                );
            yield return AddFunction(2,
                (i, j) => {
                    try {
                        var times = int.Parse(j.First());
                        var functionName = j.Last();
                        for (int k = 0; k < times; k++) {
                            i.Push(functionName);
                        }
                        ///will return sucess or failure
                        return "";
                    } catch {
                        return "failure";
                    }
                }, "Exceutes the function b the specified number of times", "execute"
                );
        }
        private static Random rand = new Random();

    }
}
