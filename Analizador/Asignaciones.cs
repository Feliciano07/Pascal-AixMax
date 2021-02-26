using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Asignacion;

namespace Pascal_AirMax.Analizador
{
    public static class Asignaciones
    {

        public static Nodo Tipo_asignacion(ParseTreeNode entrada)
        {
            string toke = entrada.ChildNodes[0].Term.Name;

            switch (toke)
            {
                case "Id":
                    return Variable_unica(entrada);
            }
            return null;
        }

        public static Nodo Variable_unica(ParseTreeNode entrada)
        {
            int linea = entrada.ChildNodes[0].Span.Location.Line;
            int columna = entrada.ChildNodes[0].Span.Location.Column;

            string nombre_variable = entrada.ChildNodes[0].Token.Text;

            Nodo expresion = Expresion.Expresion.evaluar(entrada.ChildNodes[2]);

            return new Asignacion1(linea, columna, new Acceso(linea, columna, nombre_variable, null), expresion);

        }

    }
}
