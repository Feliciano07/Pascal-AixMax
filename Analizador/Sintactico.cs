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
            encabezado(raiz.ChildNodes[0]);
            Maestra.getInstancia.ejecutar();

            if(Maestra.getInstancia.total_errores_encontrados() > 0)
            {
                return false;
            }

            return true;
        }

        public void encabezado (ParseTreeNode actual)
        {
            if(actual.ChildNodes.Count == 5)
            {
                // declaracion de variables, objetos, arrays, funciones
                declaraciones(actual.ChildNodes[3]);

                Instruciones(actual.ChildNodes[4].ChildNodes[1]);
               
            }else if(actual.ChildNodes.Count == 4)
            {
                Instruciones(actual.ChildNodes[3].ChildNodes[1]);
            }
        }

        public void declaraciones (ParseTreeNode actual)
        {
            
            foreach(ParseTreeNode node in actual.ChildNodes)
            {
          
                Maestra.getInstancia.addInstruccion(declaracion(node.ChildNodes[0]));

            }
        }


        public Nodo declaracion (ParseTreeNode actual)
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
                case "objectos":
                    return Variables.Declaracion_Objeto(actual);
                case "arrays":
                    Variables.Declaracion_arreglo(actual.ChildNodes[1],0, null);
                    break;
                case "dec_procedimiento":
                    return Declaracion_funcion.Procedimiento(actual);
                case "dec_funcion":
                    return Declaracion_funcion.Funciones(actual);
            }
            return null;
        }


        public void Instruciones(ParseTreeNode entrada)
        {
            foreach( ParseTreeNode node in entrada.ChildNodes)
            {
                
                String tipo = node.Term.Name;

                switch (tipo)
                {
                    case "main":
                        Maestra.getInstancia.addInstruccion(Instruccion(node.ChildNodes[0]));
                        break;
                    case "sentencias_main":
                        {
                            foreach(ParseTreeNode node2 in node.ChildNodes[1].ChildNodes)
                            {
                                Maestra.getInstancia.addInstruccion(Instruccion(node2.ChildNodes[0]));
                            }
                            break;
                        }
                }

                
            }
        }

        public Nodo Instruccion(ParseTreeNode actual)
        {
            String toke = actual.Term.Name;

            switch (toke)
            {
                case "writeln":
                    return Main.Inst_Writeln(actual);
                case "write":
                    return Main.Inst_Write(actual);
                case "ifthen":
                    return Main.Inst_Ifthen(actual);
                case "ifelse":
                    return Main.Instru_IfElse(actual);
                case "caseof":
                    return Main.Case_OF(actual.ChildNodes[0]);
                case "whiledo":
                    return Main.While(actual);
                case "repeat":
                    return Main.Repeat(actual);
                case "no_for":
                    return Main.For(actual);
                case "asignacion":
                    return Asignaciones.Tipo_asignacion(actual);
                case "continue":
                    return Tranferencias.Sentencia_continue(actual);
                case "break":
                    return Tranferencias.Sentencia_break(actual);
                case "llamada_funciones":
                    return Main.LLamada_funcion(actual);
                case "exit":
                    return Tranferencias.Sentencia_Exit(actual);
                case "tabla_sym":
                    return Main.Instruccion_Graficar_ts(actual);

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
