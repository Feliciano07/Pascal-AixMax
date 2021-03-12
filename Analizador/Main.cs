using System;
using System.Collections.Generic;
using System.Text;

using Irony.Parsing;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Asignacion;
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


        public static Nodo LLamada_funcion(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            if(entrada.ChildNodes.Count == 4)
            {
                LinkedList<Nodo> expresiones;
                expresiones = lista_expresion(entrada.ChildNodes[2]);

                string nombre_funcion = entrada.ChildNodes[0].Token.Text;

                return new LlamadaFuncion(linea, columna, nombre_funcion, expresiones);

            }else if(entrada.ChildNodes.Count == 3)
            {
                LinkedList<Nodo> expresion = new LinkedList<Nodo>();
                string nombre_funcion = entrada.ChildNodes[0].Token.Text;

                return new LlamadaFuncion(linea, columna, nombre_funcion, expresion);
            }
            return null;
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
            else
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
            else
            {
                LinkedList<Nodo> tem = new LinkedList<Nodo>();
                Manejador.Maestra.getInstancia.addInstruccion(new Write(linea, columna));
                return new LlamadaFuncion(linea, columna, "write", tem);
            }
            return null;
        }

        public static Nodo Instruccion_Graficar_ts(ParseTreeNode entrada)
        {
            int linea = entrada.ChildNodes[0].Span.Location.Line;
            int columna = entrada.ChildNodes[0].Span.Location.Column;

            Manejador.Maestra.getInstancia.addInstruccion(new GraficarTs(linea, columna));
            return new LlamadaFuncion(linea, columna, "graficar_ts", null);
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
                case "caseof":
                    return Main.Case_OF(actual.ChildNodes[0]);
                case "sentencia_case":
                    return Main.Case_OF(actual);
                case "sentencia_while":
                    return Main.While_If(actual);
                case "sentencia_repeat":
                    return Main.Repeat(actual);
                case "sentencia_for":
                    return Main.For_if(actual);
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
                case "no_for":
                    return Main.For(actual);
                case "whiledo":
                    return Main.While(actual);
                case "repeat":
                    return Main.Repeat(actual);
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

            }else if(entrada.ChildNodes.Count == 7)
            {
                Nodo exp = Expresion.Expresion.evaluar(entrada.ChildNodes[1]);
                LinkedList<Case> casos = Lista_Casos(entrada.ChildNodes[3]);

                LinkedList<Nodo> temporal = new LinkedList<Nodo>();
                temporal.AddLast(Main_If(entrada.ChildNodes[5].ChildNodes[0]));

                return new Sentencia_case(linea, columna, exp, casos, temporal);

            }else if(entrada.ChildNodes.Count == 10)
            {
                Nodo exp = Expresion.Expresion.evaluar(entrada.ChildNodes[1]);
                LinkedList<Case> casos = Lista_Casos(entrada.ChildNodes[3]);
                LinkedList<Nodo> tem = ListaMain_If(entrada.ChildNodes[6]);

                return new Sentencia_case(linea, columna, exp, casos, tem);


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

        public static Nodo While(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;
            if(entrada.ChildNodes.Count == 4)
            {
                Nodo exp = Expresion.Expresion.evaluar(entrada.ChildNodes[1]);
                LinkedList<Nodo> temporal = new LinkedList<Nodo>();
                temporal.AddLast(Main_If(entrada.ChildNodes[3].ChildNodes[0]));

                return new WhileDO(linea, columna, exp, temporal);

            }
            else if(entrada.ChildNodes.Count == 7)
            {
                Nodo exp = Expresion.Expresion.evaluar(entrada.ChildNodes[1]);
                LinkedList<Nodo> tem = ListaMain_If(entrada.ChildNodes[4]);
                return new WhileDO(linea, columna, exp, tem);

            }
            return null;
        }

        public static Nodo While_If(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;
            Nodo exp = Expresion.Expresion.evaluar(entrada.ChildNodes[1]);

            if(entrada.ChildNodes[3].ChildNodes.Count == 1)
            {
                LinkedList<Nodo> tem_else = new LinkedList<Nodo>();
                tem_else.AddLast(Main_If(entrada.ChildNodes[3].ChildNodes[0]));//else
                return new WhileDO(linea, columna, exp, tem_else);
            }
            else if (entrada.ChildNodes[3].ChildNodes.Count  == 3)
            {
                LinkedList<Nodo> tem_if = ListaMain_If(entrada.ChildNodes[3].ChildNodes[1]); //if
                return new WhileDO(linea, columna, exp, tem_if);
            }
            return null;

        }


        public static Nodo Repeat(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            Nodo expresion = Expresion.Expresion.evaluar(entrada.ChildNodes[3]);

            LinkedList<Nodo> tem = Lista_Repeat(entrada.ChildNodes[1]);

            return new Repeat(linea, columna, expresion, tem);

        }

        public static LinkedList<Nodo> Lista_Repeat(ParseTreeNode entrada)
        {
            LinkedList<Nodo> nueva_instru = new LinkedList<Nodo>();

            foreach(ParseTreeNode node in entrada.ChildNodes)
            {
                
                String tipo = node.Term.Name;

                switch (tipo)
                {
                    case "main":
                        nueva_instru.AddLast(Instrucion_Repeat(node.ChildNodes[0]));
                        break;
                    case "sentencias_main":
                        {
                            foreach (ParseTreeNode node2 in node.ChildNodes[1].ChildNodes)
                            {
                                nueva_instru.AddLast(Instrucion_Repeat(node2.ChildNodes[0]));
                            }
                            break;
                        }
                }
            }
            return nueva_instru;
        }

        public static Nodo Instrucion_Repeat(ParseTreeNode actual)
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
                case "opcion_else":
                    return Main.Opcion_else(actual);
                case "sentencia_case":
                    return Main.Case_OF(actual);
                case "sentencia_while":
                    return Main.While_If(actual);
                case "sentencia_repeat":
                    return Main.Repeat(actual);
                case "sentencia_for":
                    return Main.For_if(actual);



            }
            return null;
        }


        public static Nodo For(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            //captura de la expresion
            Asignacion1 asignacion = (Asignacion1)Asignaciones.Tipo_asignacion(entrada.ChildNodes[1]); //entrada.childNodes[1];

            string token = entrada.ChildNodes[2].Term.Name.ToLower();
            bool comportamiento = false;

            switch (token)
            {
                case "to":
                    comportamiento = false;
                    break;
                case "downto":
                    comportamiento = true;
                    break;
            }

            Nodo expresion = Expresion.Expresion.evaluar(entrada.ChildNodes[3]);

            token = entrada.ChildNodes[5].Term.Name;
             
            if(entrada.ChildNodes.Count == 6)
            {
                LinkedList<Nodo> temporal = new LinkedList<Nodo>();
                temporal.AddLast(Main_If(entrada.ChildNodes[5].ChildNodes[0]));
                return new For(linea, columna, asignacion, expresion, temporal, comportamiento);

            }
            else if(entrada.ChildNodes.Count == 9)
            {
                LinkedList<Nodo> temporal;
                temporal = ListaMain_If(entrada.ChildNodes[6]);
                return new For(linea, columna, asignacion, expresion, temporal, comportamiento);

            }

            return null;
        }

        public static Nodo For_if(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            //captura de la expresion
            Asignacion1 asignacion = (Asignacion1)Asignaciones.Tipo_asignacion(entrada.ChildNodes[1]);

            string token = entrada.ChildNodes[2].Term.Name.ToLower();
            bool comportamiento = false;

            switch (token)
            {
                case "to":
                    comportamiento = false;
                    break;
                case "downto":
                    comportamiento = true;
                    break;
            }

            Nodo expresion = Expresion.Expresion.evaluar(entrada.ChildNodes[3]);

            if (entrada.ChildNodes[5].ChildNodes.Count == 1)
            {
                LinkedList<Nodo> tem_else = new LinkedList<Nodo>();
                tem_else.AddLast(Main_If(entrada.ChildNodes[5].ChildNodes[0]));
                return new For(linea, columna, asignacion, expresion, tem_else, comportamiento);
            }
            else if (entrada.ChildNodes[5].ChildNodes.Count == 3)
            {
                LinkedList<Nodo> tem_if = ListaMain_If(entrada.ChildNodes[5].ChildNodes[1]);
                return new For(linea, columna, asignacion, expresion, tem_if, comportamiento);

            }
            return null;
        }

    }
}
