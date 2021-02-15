using Irony.Parsing;
using Pascal_AirMax.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Expresion.Aritmetica;
using Pascal_AirMax.TipoDatos;
using Pascal_AirMax.Expresion.Logicas;
using Pascal_AirMax.Expresion.Relacionales;

namespace Pascal_AirMax.Expresion
{
    static class Expresion
    {

        public static Nodo evaluar(ParseTreeNode entrada)
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
                    case "*":
                        return new Multiplicacion(linea, columna, evaluar(entrada.ChildNodes[0]), evaluar(entrada.ChildNodes[2]));
                    case "/":
                        return new Division(linea, columna, evaluar(entrada.ChildNodes[0]), evaluar(entrada.ChildNodes[2]));
                    case "mod":
                        return new Modulo(linea, columna, evaluar(entrada.ChildNodes[0]), evaluar(entrada.ChildNodes[2]));
                    case "and":
                        return new And(linea, columna, evaluar(entrada.ChildNodes[0]), evaluar(entrada.ChildNodes[2]));
                    case ">":
                        return new MayorQ(linea, columna, evaluar(entrada.ChildNodes[0]), evaluar(entrada.ChildNodes[2]));
                    case "<":
                        return new MenorQ(linea, columna, evaluar(entrada.ChildNodes[0]), evaluar(entrada.ChildNodes[2]));
                    case ">=":
                        return new MayorIgual(linea, columna, evaluar(entrada.ChildNodes[0]), evaluar(entrada.ChildNodes[2]));
                    case "<=":
                        return new MenorIgual(linea, columna, evaluar(entrada.ChildNodes[0]), evaluar(entrada.ChildNodes[2]));
                    case "=":
                        return new Igual(linea, columna, evaluar(entrada.ChildNodes[0]), evaluar(entrada.ChildNodes[2]));
                    case "<>":
                        return new NoIgual(linea, columna, evaluar(entrada.ChildNodes[0]), evaluar(entrada.ChildNodes[2]));

                    default:
                        return evaluar(entrada.ChildNodes[1]);
                }
            }else if(entrada.ChildNodes.Count == 2)
            {
                String toke = entrada.ChildNodes[0].Term.Name;
                int linea = entrada.ChildNodes[0].Span.Location.Line;
                int columna = entrada.ChildNodes[0].Span.Location.Column;

                switch (toke)
                {
                    case "not":
                        return new Not(linea, columna, evaluar(entrada.ChildNodes[1]));
                    case "-":
                        return new Negativo(linea, columna, evaluar(entrada.ChildNodes[1]));
                }

            }
            else if(entrada.ChildNodes.Count == 1)
            {
                String type = entrada.ChildNodes[0].Term.Name;
                String valor = entrada.ChildNodes[0].Token.Text;
                int linea = entrada.ChildNodes[0].Span.Location.Line;
                int columna = entrada.ChildNodes[0].Span.Location.Column;

                switch (type)
                {
                    case "entero":
                        return new Constante(linea, columna, new Primitivo(Objeto.TipoObjeto.INTEGER, valor));

                    case "cadena":
                        return new Constante(linea, columna, new Primitivo(Objeto.TipoObjeto.STRING, valor));

                    case "decimal":
                        return new Constante(linea, columna, new Primitivo(Objeto.TipoObjeto.REAL, valor));
                    case "true":
                        return new Constante(linea, columna, new Primitivo(Objeto.TipoObjeto.BOOLEAN, valor));
                    case "false":
                        return new Constante(linea, columna, new Primitivo(Objeto.TipoObjeto.BOOLEAN, valor));

                }
            }
            return null;
        }
    }
}
