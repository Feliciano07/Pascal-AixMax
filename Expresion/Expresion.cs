using Irony.Parsing;
using Pascal_AirMax.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Expresion.Aritmetica;
using Pascal_AirMax.TipoDatos;
using Pascal_AirMax.Expresion.Logicas;
using Pascal_AirMax.Expresion.Relacionales;
using Pascal_AirMax.Asignacion;
using Pascal_AirMax.Analizador;

namespace Pascal_AirMax.Expresion
{
    public static class Expresion
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
              
                int linea = entrada.ChildNodes[0].Span.Location.Line;
                int columna = entrada.ChildNodes[0].Span.Location.Column;

                switch (type)
                {
                    case "entero":
                        {
                            String valor = entrada.ChildNodes[0].Token.Text;
                            return new Constante(linea, columna, new Primitivo(Objeto.TipoObjeto.INTEGER, valor));
                        }

                    case "cadena":
                        {
                            String valor = entrada.ChildNodes[0].Token.Text;
                            return new Constante(linea, columna, new Primitivo(Objeto.TipoObjeto.STRING, valor));
                        }

                    case "decimal":
                        {
                            String valor = entrada.ChildNodes[0].Token.Text;
                            return new Constante(linea, columna, new Primitivo(Objeto.TipoObjeto.REAL, valor));
                        }
                    case "true":
                        {
                            String valor = entrada.ChildNodes[0].Token.Text;
                            return new Constante(linea, columna, new Primitivo(Objeto.TipoObjeto.BOOLEAN, valor));
                        }
                    case "false":
                        {
                            String valor = entrada.ChildNodes[0].Token.Text;
                            return new Constante(linea, columna, new Primitivo(Objeto.TipoObjeto.BOOLEAN, valor));
                        }
                    case "Id":
                        {
                            String valor = entrada.ChildNodes[0].Token.Text;
                            return new Acceso(linea, columna, valor, null);
                        }
                    case "acceso_array":
                        return Asignaciones.Arreglo_Unico(entrada.ChildNodes[0]);
                    case "acceso_objeto":
                        return Primer_Nivel(entrada.ChildNodes[0]);
                }
            }
            return null;
        }

        public static Nodo Primer_Nivel(ParseTreeNode entrada)
        {
            //TODO: validar si es id o array
            int linea = entrada.ChildNodes[0].Span.Location.Line;
            int columna = entrada.ChildNodes[0].Span.Location.Column;
            if (entrada.ChildNodes.Count == 3)
            {

                string nombre = entrada.ChildNodes[0].Token.Text;

                Acceso primero = new Acceso(linea, columna, nombre, null);

                Acceso retorno = Asignaciones.Niveles_abajo(entrada.ChildNodes[2], primero);

                return retorno;
            }
            else if (entrada.ChildNodes.Count == 6)
            {
                string nombre = entrada.ChildNodes[0].Token.Text;
                LinkedList<Nodo> dimensiones = Main.lista_expresion(entrada.ChildNodes[2]);
                Acceso primero = new Acceso(linea, columna, nombre, null, dimensiones);
                Acceso retorno = Asignaciones.Niveles_abajo(entrada.ChildNodes[5], primero);

                return retorno;
            }
            return null;
        }

        
    }
}
