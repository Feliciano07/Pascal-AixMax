using Irony.Parsing;
using Pascal_AirMax.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Expresion.Aritmetica;
namespace Pascal_AirMax.Expresion
{
    static class Expresion
    {

        public static Instruction evaluar(ParseTreeNode entrada)
        {
            //operacion binaria
            if (entrada.ChildNodes.Count == 3)
            {
                String toke = entrada.ChildNodes[1].Term.Name;
                int linea = entrada.ChildNodes[1].Span.Location.Line;
                int columna = entrada.ChildNodes[1].Span.Location.Column;

                switch (toke)
                {
                    case "+":
                        return new Suma(linea, columna, evaluar(entrada.ChildNodes[0]), evaluar(entrada.ChildNodes[2]));

                    case "-":
                        return new Resta(linea, columna, evaluar(entrada.ChildNodes[0]), evaluar(entrada.ChildNodes[2]));
                }
            }else if(entrada.ChildNodes.Count == 1)
            {
                String type = entrada.ChildNodes[0].Term.Name;
                String valor = entrada.ChildNodes[0].Token.Text;
                int linea = entrada.ChildNodes[0].Span.Location.Line;
                int columna = entrada.ChildNodes[0].Span.Location.Column;

                switch (type)
                {
                    case "entero":
                        return new Tipo(linea, columna, valor, Tipo.TIPOS.Entero);
                }
            }
            return null;
        }
    }
}
