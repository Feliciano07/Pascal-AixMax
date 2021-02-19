using System;
using System.Collections.Generic;
using System.Text;

using Irony.Parsing;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Expresion;
using Pascal_AirMax.Instruccion;
using Pascal_AirMax.Sentencias;

namespace Pascal_AirMax.Analizador
{
    public static class Main
    {

        
        public static LinkedList<Nodo> lista_expresion(ParseTreeNode entrada)
        {
            LinkedList<Nodo> parametros = new LinkedList<Nodo>();

            foreach(ParseTreeNode node in entrada.ChildNodes)
            {
                parametros.AddLast(Expresion.Expresion.evaluar(node));
            }

            return parametros;
        }

        public static Nodo Inst_Writeln(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            if (entrada.ChildNodes.Count == 4)
            {
                LinkedList<Nodo> tem;
                tem = lista_expresion(entrada.ChildNodes[2]);
                Manejador.Maestra.getInstancia.addInstruccion(new Writeln(linea, columna));
                return new LlamadaFuncion(linea, columna, "writeln", tem);

            }else if(entrada.ChildNodes.Count == 3)
            {
                LinkedList<Nodo> tem = new LinkedList<Nodo>();
                Manejador.Maestra.getInstancia.addInstruccion(new Writeln(linea, columna));
                return new LlamadaFuncion(linea, columna, "writeln", tem);
            }
            return null;
        }

        public static Nodo Inst_Write(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            if (entrada.ChildNodes.Count == 4)
            {
                LinkedList<Nodo> tem;
                tem = lista_expresion(entrada.ChildNodes[2]);
                Manejador.Maestra.getInstancia.addInstruccion(new Write(linea, columna));
                return new LlamadaFuncion(linea, columna, "write", tem);

            }
            else if (entrada.ChildNodes.Count == 3)
            {
                LinkedList<Nodo> tem = new LinkedList<Nodo>();
                Manejador.Maestra.getInstancia.addInstruccion(new Write(linea, columna));
                return new LlamadaFuncion(linea, columna, "write", tem);
            }
            return null;
        }

        public static Nodo Inst_Ifthen(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;
            if(entrada.ChildNodes.Count == 4)
            {
                Nodo exp = Expresion.Expresion.evaluar(entrada.ChildNodes[1]);
                LinkedList<Nodo> temporal = new LinkedList<Nodo>();
                temporal.AddLast(Main_If(entrada.ChildNodes[3].ChildNodes[0]));
                return new IFthen(linea, columna,exp,temporal);

            }else if(entrada.ChildNodes.Count == 7)
            {
                Nodo exp = Expresion.Expresion.evaluar(entrada.ChildNodes[1]);
                LinkedList<Nodo> tem = ListaMain_If(entrada.ChildNodes[4]);
                return new IFthen(linea, columna, exp, tem);
            }else if(entrada.ChildNodes.Count == 6)
            {
                //pos no hacer nada
            }
            return null;
        }

        public static Nodo Main_If(ParseTreeNode actual)
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
                case "opcion_else":
                    return Main.Opcion_else(actual);
            }
            return null;
        }

        public static LinkedList<Nodo> ListaMain_If(ParseTreeNode actual)
        {
            LinkedList<Nodo> salida = new LinkedList<Nodo>();

            foreach(ParseTreeNode node in actual.ChildNodes)
            {
                salida.AddLast(Main_If(node.ChildNodes[0]));
            }

            return salida;
        }


        public static Nodo Instru_IfElse(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            if(entrada.ChildNodes.Count == 6)
            {
                Nodo exp = Expresion.Expresion.evaluar(entrada.ChildNodes[1]); // condicion

                if(entrada.ChildNodes[3].ChildNodes.Count == 1)
                {
                    LinkedList<Nodo> temporal = new LinkedList<Nodo>(); // if
                    temporal.AddLast(Main_If(entrada.ChildNodes[3].ChildNodes[0]));

                    LinkedList<Nodo> tem_else = new LinkedList<Nodo>();
                    tem_else.AddLast(Main_If(entrada.ChildNodes[5].ChildNodes[0]));//else

                    return new IfElse(linea, columna, exp, temporal, tem_else);

                }
                else if(entrada.ChildNodes[3].ChildNodes.Count == 3)
                {
                    LinkedList<Nodo> tem_if = ListaMain_If(entrada.ChildNodes[3].ChildNodes[1]); //if
                    LinkedList<Nodo> tem_else = new LinkedList<Nodo>();
                    tem_else.AddLast(Main_If(entrada.ChildNodes[5].ChildNodes[0])); //else

                    return new IfElse(linea, columna, exp, tem_if, tem_else);
                }

            }
            else if(entrada.ChildNodes.Count == 9)
            {
                Nodo exp = Expresion.Expresion.evaluar(entrada.ChildNodes[1]); // condicion
                if (entrada.ChildNodes[3].ChildNodes.Count == 1)
                {
                    LinkedList<Nodo> temporal = new LinkedList<Nodo>(); // if
                    temporal.AddLast(Main_If(entrada.ChildNodes[3].ChildNodes[0]));

                    LinkedList<Nodo> tem_else = ListaMain_If(entrada.ChildNodes[6]);



                    return new IfElse(linea, columna, exp, temporal, tem_else);

                }
                else if (entrada.ChildNodes[3].ChildNodes.Count == 3)
                {
                    LinkedList<Nodo> tem_if = ListaMain_If(entrada.ChildNodes[3].ChildNodes[1]); //if
                    LinkedList<Nodo> tem_else = ListaMain_If(entrada.ChildNodes[6]);
                    return new IfElse(linea, columna, exp, tem_if, tem_else);
                }
            }
            return null;
        }

        public static Nodo Opcion_else(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            Nodo exp = Expresion.Expresion.evaluar(entrada.ChildNodes[1]);
            LinkedList<Nodo> tem_if = new LinkedList<Nodo>();

            // if
            if (entrada.ChildNodes[3].ChildNodes.Count == 1)
            {
                
                tem_if.AddLast(Main_If(entrada.ChildNodes[3].ChildNodes[0]));

            }
            else if (entrada.ChildNodes[3].ChildNodes.Count == 3)
            {
                tem_if = ListaMain_If(entrada.ChildNodes[3].ChildNodes[1]);
   
            }
            LinkedList<Nodo> tem_else = new LinkedList<Nodo>();

            //else
            if (entrada.ChildNodes[5].ChildNodes.Count == 1)
            {

                tem_else.AddLast(Main_If(entrada.ChildNodes[5].ChildNodes[0]));

            }
            else if (entrada.ChildNodes[5].ChildNodes.Count == 3)
            {
                tem_else = ListaMain_If(entrada.ChildNodes[5].ChildNodes[1]);

            }

            return new IfElse(linea, columna, exp, tem_if, tem_else);
        }

        public static Nodo Case_OF(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;
            if(entrada.ChildNodes.Count == 5)
            {
                Nodo expresion = Expresion.Expresion.evaluar(entrada.ChildNodes[1]);

                LinkedList<Case> casos = Lista_Casos(entrada.ChildNodes[3]);

                return new Sentencia_case(linea, columna, expresion, casos);

            }
            return null;
        }

        public static LinkedList<Case> Lista_Casos(ParseTreeNode entrada)
        {
            LinkedList<Case> lista = new LinkedList<Case>();

            foreach(ParseTreeNode actual in entrada.ChildNodes)
            {
                lista.AddLast(Casos(actual));

            }
            return lista;
        }

        public static Case Casos(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            if(entrada.ChildNodes.Count == 3)
            {
                LinkedList<Nodo> tem;
                tem = lista_expresion(entrada.ChildNodes[0]);

                LinkedList<Nodo> temporal = new LinkedList<Nodo>();

                temporal.AddLast(Main_If(entrada.ChildNodes[2].ChildNodes[0]));

                return new Case(linea, columna, tem, temporal);

            }else if(entrada.ChildNodes.Count == 6)
            {
                LinkedList<Nodo> tem_exp;
                tem_exp = lista_expresion(entrada.ChildNodes[0]);

                LinkedList<Nodo> tem = ListaMain_If(entrada.ChildNodes[3]);

                return new Case(linea, columna, tem_exp, tem);

            }
            return null;
        }

    }
}
