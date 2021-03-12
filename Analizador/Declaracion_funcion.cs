using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Declaraciones;
using Pascal_AirMax.Funciones;

namespace Pascal_AirMax.Analizador
{
    public static class Declaracion_funcion
    {

        public static Nodo Procedimiento(ParseTreeNode entrada)
        {
            string id_procedimiento = entrada.ChildNodes[1].Token.Text;
            int linea = entrada.ChildNodes[1].Span.Location.Line;
            int columna = entrada.ChildNodes[1].Span.Location.Column;

            LinkedList<Nodo> parametros =  Obtener_parametros(entrada.ChildNodes[3]);

            if(entrada.ChildNodes.Count == 8)
            {
                LinkedList<Nodo> variables = new LinkedList<Nodo>();
                Variables.Variables_Objeto(entrada.ChildNodes[6],variables);
                Instrucciones_Funcion(entrada.ChildNodes[7], variables);

                return new CreacionProcedimiento(linea, columna, id_procedimiento, variables, parametros);

            }else if(entrada.ChildNodes.Count == 7)
            {
                LinkedList<Nodo> variables = new LinkedList<Nodo>();
                // chilnode[6] instrucciones
                Instrucciones_Funcion(entrada.ChildNodes[6],variables);
                return new CreacionProcedimiento(linea, columna, id_procedimiento, variables, parametros);
            }
            return null;
        }


        public static LinkedList<Nodo> Obtener_parametros(ParseTreeNode entrada)
        {
            LinkedList<Nodo> parametros = new LinkedList<Nodo>();

            foreach(ParseTreeNode node in entrada.ChildNodes)
            {
                string term = node.Term.Name;
                switch (term)
                {
                    case "parametros_valor":
                        Parametros_valor(node, parametros);
                        break;
                    case "parametros_referencia":
                        Parametros_referencia(node, parametros);
                        break;
                }
            }
            return parametros;
        }

        public static void Parametros_valor(ParseTreeNode entrada, LinkedList<Nodo> salida)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            Objeto.TipoObjeto tipo = Variables.getTipo(entrada.ChildNodes[2]);
            string nombre_type = Variables.Nombre_del_tipo(entrada.ChildNodes[2]);

            getId(entrada.ChildNodes[0], tipo, nombre_type, Parametro.Tipo_Parametro.VALOR, salida); 
        }

        public static void Parametros_referencia(ParseTreeNode entrada, LinkedList<Nodo> salida)
        {
            ParseTreeNode referencia = entrada.ChildNodes[1];

            int linea = referencia.Span.Location.Line;
            int columna = referencia.Span.Location.Column;

            Objeto.TipoObjeto tipo = Variables.getTipo(referencia.ChildNodes[2]);
            string nombre_type = Variables.Nombre_del_tipo(referencia.ChildNodes[2]);

            getId(referencia.ChildNodes[0], tipo, nombre_type, Parametro.Tipo_Parametro.REFERENCIA,salida);
        }


        public static  void getId(ParseTreeNode entrada, Objeto.TipoObjeto tipo, string nombre_tipo, 
            Parametro.Tipo_Parametro tipo_Parametro, LinkedList<Nodo> salida)
        {

            for (int i = 0; i < entrada.ChildNodes.Count; i++)
            {
                int linea = entrada.ChildNodes[i].Span.Location.Line;
                int columna = entrada.ChildNodes[i].Span.Location.Column;
                string nombre = entrada.ChildNodes[i].Token.Text;

                salida.AddLast(new Creacion_Parametro(linea, columna, nombre, tipo, nombre_tipo, tipo_Parametro, Variables.getObjeto(tipo)));

            }
        }


        public static void Instrucciones_Funcion(ParseTreeNode entrada, LinkedList<Nodo> lista)
        {
            
            foreach(ParseTreeNode node in entrada.ChildNodes[1].ChildNodes)
            {
                Instruccion(node.ChildNodes[0],lista);
            }
        }

        public static void Instruccion(ParseTreeNode actual, LinkedList<Nodo> lista)
        {
            String toke = actual.Term.Name;

            switch (toke)
            {
                case "writeln":
                    lista.AddLast(Main.Inst_Writeln(actual));
                    break;
                case "write":
                    lista.AddLast(Main.Inst_Write(actual));
                    break;
                case "ifthen":
                    lista.AddLast(Main.Inst_Ifthen(actual));
                    break;
                case "ifelse":
                    lista.AddLast(Main.Instru_IfElse(actual));
                    break;
                case "caseof":
                    lista.AddLast(Main.Case_OF(actual.ChildNodes[0]));
                    break;
                case "whiledo":
                    lista.AddLast(Main.While(actual));
                    break;
                case "repeat":
                    lista.AddLast(Main.Repeat(actual));
                    break;
                case "no_for":
                    lista.AddLast(Main.For(actual));
                    break;
                case "asignacion":
                    lista.AddLast(Asignaciones.Tipo_asignacion(actual));
                    break;
                case "continue":
                    lista.AddLast(Tranferencias.Sentencia_continue(actual));
                    break;
                case "break":
                    lista.AddLast(Tranferencias.Sentencia_break(actual));
                    break;
                case "llamada_funciones":
                    lista.AddLast( Main.LLamada_funcion(actual));
                    break;
                case "exit":
                    lista.AddLast(Tranferencias.Sentencia_Exit(actual));
                    break;
                case "tabla_sym":
                    lista.AddLast( Main.Instruccion_Graficar_ts(actual));
                    break;
                case "sentencia_case":
                     lista.AddLast(Main.Case_OF(actual));
                    break;
                case "sentencia_while":
                    lista.AddLast(Main.While_If(actual));
                    break;
                case "sentencia_repeat":
                   lista.AddLast(Main.Repeat(actual));
                    break;
                case "sentencia_for":
                    lista.AddLast(Main.For_if(actual));
                    break;



            }
        }


        // ******************** CREACION DE FUNCIONES

        public static Nodo Funciones(ParseTreeNode entrada)
        {
            int linea = entrada.ChildNodes[1].Span.Location.Line;
            int columna = entrada.ChildNodes[1].Span.Location.Column;

            string nombre_funcion = entrada.ChildNodes[1].Token.Text;

            LinkedList<Nodo> parametros = Obtener_parametros(entrada.ChildNodes[3]);

            Objeto.TipoObjeto tipo_retorno = Variables.getTipo(entrada.ChildNodes[6]);
            string nombre_retorno = Variables.Nombre_del_tipo(entrada.ChildNodes[6]);


            if (entrada.ChildNodes.Count == 10)
            {
                LinkedList<Nodo> variables = new LinkedList<Nodo>();
                Variables.Variables_Objeto(entrada.ChildNodes[8], variables);
                Instrucciones_Funcion(entrada.ChildNodes[9], variables);

                return new CreacionFuncion(linea, columna, nombre_funcion, variables, parametros, nombre_retorno, tipo_retorno, Variables.getObjeto(tipo_retorno));

            }
            else if(entrada.ChildNodes.Count == 9)
            {
                LinkedList<Nodo> variables = new LinkedList<Nodo>();
                // chilnode[6] instrucciones
                Instrucciones_Funcion(entrada.ChildNodes[8], variables);
                return new CreacionFuncion(linea, columna, nombre_funcion, variables, parametros, nombre_retorno, tipo_retorno, Variables.getObjeto(tipo_retorno));
            }
            return null;
        }

    }
}
