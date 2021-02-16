using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;
using Pascal_AirMax.Graficas;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Manejador;


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


            instrucciones(raiz.ChildNodes[0]);

            Maestra.getInstancia.ejecutar();

            return true;
        }

        public void instrucciones (ParseTreeNode actual)
        {
            
            foreach(ParseTreeNode node in actual.ChildNodes)
            {
          
                Maestra.getInstancia.addInstruccion(instruccion(node.ChildNodes[0]));

            }
        }


        public Nodo instruccion (ParseTreeNode actual)
        {
            String toke = actual.Term.Name;

            switch (toke)
            {
                case "exp":
                    return Expresion.Expresion.evaluar(actual);

                case "variable":
                    return Variables.Lista_variable(actual.ChildNodes[1]);
                case "constante":
                    return Variables.Lista_constante(actual.ChildNodes[1]);

            }
            return null;
        }



        public void GenerarAST (ParseTreeNode raiz)
        {
            Dot.GenerarAST(raiz);
        }

        public void ErroresLexicos(ParseTree arbol)
        {
            //LinkedList<Error> lexicos = new LinkedList<Error>();


            foreach(var item in arbol.ParserMessages)
            {
                
                string type = item.Message;
                if (type.Contains("Invalid"))
                {
                    Error error = new Error(item.Location.Line, item.Location.Column, Error.Errores.Lexico, item.Message);
                    Maestra.getInstancia.addError(error);
                }
                else
                {
                    Error error = new Error(item.Location.Line, item.Location.Column, Error.Errores.Sintactico, item.Message);
                    Maestra.getInstancia.addError(error);
                }
            }
            
        }
    }
}
