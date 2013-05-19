using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StackIDE {
    public class Stack {
        public Stack() {
            this.TokenStack = new Stack<string>();
            this.OperationStack = new Stack<string>();
            foreach (var f in FunctionLib.GetFunctions()) {
                foreach (var n in f.Names) {
                    this.functions.Add(n, f);
                }
            }
        }

        public Stack<string> TokenStack { get; set; }

        public void AddFunction(int paramCount, evaluator eval, string description, params string[] names) {
            var func = new StackFunction() { Eval = eval, ParamCount = paramCount, Description = description, Names = names };
            foreach (var n in names) {
                this.functions.Add(n, 
                    func);
            }
        }

        public Stack<string> OperationStack { get; set; }
        Dictionary<string, StackFunction> functions = new Dictionary<string, StackFunction>();

        public Dictionary<string, StackFunction> GetFunctions() {
            return functions;
        }

        public void Push(string val, bool functional = true) {
            if (functions.ContainsKey(val) && functional) {
                var function = functions[val];
                List<string> parameters = new List<string>();
                for (int i = 0; i < function.ParamCount; i++) {
                    parameters.Add(this.TokenStack.Pop());
                }
                var result = function.Eval(this, parameters);
                if (result != "") {
                    this.TokenStack.Push(result);
                }
                this.OperationStack.Push(val);
            } else {
                this.TokenStack.Push(val);
            }
        }
    }

    public delegate string evaluator(Stack stack, List<string> vals);
    public class StackFunction {
        public int ParamCount { get; set; }
        public evaluator Eval;
        public string Description { get; set; }
        public string[] Names { get; set; }
    }
}
