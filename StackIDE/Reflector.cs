﻿using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StackIDE {
    class Reflector {
        public Assembly BuildAssembly(string code) {
            Microsoft.CSharp.CSharpCodeProvider provider = new CSharpCodeProvider();
            ICodeCompiler compiler = provider.CreateCompiler();
            CompilerParameters compilerparams = new CompilerParameters();
            compilerparams.GenerateExecutable = false;
            compilerparams.GenerateInMemory = true;
            CompilerResults results = compiler.CompileAssemblyFromSource(compilerparams, code);
            if (results.Errors.HasErrors) {
                StringBuilder errors = new StringBuilder("Compiler Errors :\r\n");
                foreach (CompilerError error in results.Errors) {
                    errors.AppendFormat("Line {0},{1}\t: {2}\n", error.Line, error.Column, error.ErrorText);
                }
                throw new Exception(errors.ToString());
            } else {
                return results.CompiledAssembly;
            }
        }

        public object ExecuteCode(string code, string namespacename, string classname, string functionname, bool isstatic, params object[] args) {
            object returnval = null;
            Assembly asm = BuildAssembly(code);
            object instance = null;
            Type type = null;
            if (isstatic) {
                type = asm.GetType(namespacename + "." + classname);
            } else {
                instance = asm.CreateInstance(namespacename + "." + classname);
                type = instance.GetType();
            }
            MethodInfo method = type.GetMethod(functionname);
            returnval = method.Invoke(instance, args);
            return returnval;
        }

        private const string thisNamespace = "_reflectorNamespace";
        private const string thisClass = "_reflectorClass";
        private const string headers = "using System.Collections.Generic; ";

        public object ExecuteFunction(string function, string functionName, params object[] args) {
            function = headers + "namespace " + thisNamespace + "{" + "class " + thisClass + "{" + function + "}}";
            return ExecuteCode(function, thisNamespace, thisClass, functionName, false, args);
        }

    }
}
