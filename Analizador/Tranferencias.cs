using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Transferencia;

namespace Pascal_AirMax.Analizador
{
    public static class Tranferencias
    {

        public static Nodo Sentencia_continue(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            return new Continue(linea, columna);
        }

        public static Nodo Sentencia_break(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;
            return new Break(linea, columna);
        }


        public static Nodo Sentencia_Exit(ParseTreeNode entrada)
        {
            int linea = entrada.ChildNodes[0].Span.Location.Line;
            int columna = entrada.ChildNodes[0].Span.Location.Column;
            if(entrada.ChildNodes.Count == 4)
            {
                Nodo expresion = Expresion.Expresion.evaluar(entrada.ChildNodes[2]);

                return new Exit(linea, columna, expresion);
            }
            else
            {
                return new Exit(linea, columna, null);
            }
        }

    }
}
