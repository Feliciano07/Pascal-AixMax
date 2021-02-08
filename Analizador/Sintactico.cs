using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;
using Pascal_AirMax.Graficas;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Abstract;
namespace Pascal_AirMax.Analizador
{
    public class Sintactico
    {
        public bool Analizar (string texto)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData languageData = new LanguageData(gramatica);
            Parser parser = new Parser(languageData);
            ParseTree parseTree = parser.Parse(texto);
            ParseTreeNode raiz = parseTree.Root;
            ErroresLexicos(parseTree);
            if (raiz == null)
            {
                return false;
            }
            GenerarAST(raiz);

            //LinkedList<Instruction> ast = instrucciones(raiz.ChildNodes[0]);


            // Realizar la ejecucion de las instrucciones 
           /* foreach(Instruction inst in ast)
            {
                Object dato = inst.execute();

                Console.WriteLine(dato);
            }*/

            return true;
        }

        public LinkedList<Instruction> instrucciones (ParseTreeNode actual)
        {
            LinkedList<Instruction> lista = new LinkedList<Instruction>();
            
            foreach(ParseTreeNode node in actual.ChildNodes)
            {
                lista.AddLast(instruccion(node.ChildNodes[0]));
            }
            return lista;
        }


        public Instruction instruccion (ParseTreeNode actual)
        {
            String toke = actual.Term.Name;

            switch (toke)
            {
                case "exp":
                    return Expresion.Expresion.evaluar(actual);

            }
            return null;
        }



        public void GenerarAST (ParseTreeNode raiz)
        {
            Dot.GenerarAST(raiz);
        }

        public void ErroresLexicos(ParseTree arbol)
        {
            LinkedList<Error> lexicos = new LinkedList<Error>();

            foreach(var item in arbol.ParserMessages)
            {
                
                string type = item.Message;
                if (type.Contains("Invalid"))
                {
                    Error error = new Error(item.Location.Line, item.Location.Column, Error.Errores.Lexico, item.Message);
                    lexicos.AddLast(error);
                }
                else
                {
                    Error error = new Error(item.Location.Line, item.Location.Column, Error.Errores.Sintactico, item.Message);
                    lexicos.AddLast(error);
                }
            }
            Dot.GenerarErrores(lexicos);
        }
    }
}
