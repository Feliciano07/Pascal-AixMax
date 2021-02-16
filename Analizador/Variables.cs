using Irony.Parsing;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Instruccion;
using Pascal_AirMax.TipoDatos;
using Pascal_AirMax.Expresion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Analizador
{
    static class Variables
    {

        public static Nodo Lista_variable(ParseTreeNode entrada)
        {
            foreach (ParseTreeNode node in entrada.ChildNodes)
            {
                Manejador.Maestra.getInstancia.addInstruccion(evaluar(node));
            }
            return null;
        }

        public static Nodo Lista_constante(ParseTreeNode entrada)
        {
            foreach (ParseTreeNode node in entrada.ChildNodes)
            {
                Manejador.Maestra.getInstancia.addInstruccion(EvaluarConstante(node));
            }
            return null;
        }


        public static Nodo evaluar(ParseTreeNode entrada)
        {
            if (entrada.ChildNodes.Count == 4) //Declaraciones de variables sin iniciar
            {

                String type = entrada.ChildNodes[0].Term.Name;
                int linea = entrada.ChildNodes[0].Span.Location.Line;
                int columna = entrada.ChildNodes[0].Span.Location.Column;

                switch (type)
                {
                    case "Id":
                        {
                            string [] nombre = new string[] { entrada.ChildNodes[0].Token.Text };
                            Objeto.TipoObjeto tipo = getTipo(entrada.ChildNodes[2]);
                            return new Declaracion(linea,columna, nombre, getObjeto(tipo), tipo);
                        }
                    case "lista_id":
                        {
                            //TODO: indicar mejor la linea y columna
                            string[] nombre = getId(entrada.ChildNodes[0]);
                            Objeto.TipoObjeto tipo = getTipo(entrada.ChildNodes[2]);
                            return new Declaracion(linea, columna, nombre, getObjeto(tipo), tipo);
                        }
                    case "id_constante":
                        {
                            string nombre = entrada.ChildNodes[0].Token.Text;
                            return new DeclaracionConstante(linea, columna, nombre, Expresion.Expresion.evaluar(entrada.ChildNodes[2]), Objeto.TipoObjeto.CONST);
                        }
                }
            }else if(entrada.ChildNodes.Count == 6) // declaracion de variable iniciada
            {
                String type = entrada.ChildNodes[0].Term.Name;
                int linea = entrada.ChildNodes[0].Span.Location.Line;
                int columna = entrada.ChildNodes[0].Span.Location.Column;

                switch (type)
                {
                    case "Id":
                        {
                            string[] nombre = new string[] { entrada.ChildNodes[0].Token.Text };
                            Objeto.TipoObjeto tipo = getTipo(entrada.ChildNodes[2]);
                            return new Declaracion(linea, columna, nombre, Expresion.Expresion.evaluar(entrada.ChildNodes[4]), tipo);
                        }
                    case "id_constante":
                        {
                            string nombre = entrada.ChildNodes[0].Token.Text;
                            Objeto.TipoObjeto tipo = getTipo(entrada.ChildNodes[2]);
                            return new DeclaracionConstante(linea, columna, nombre, Expresion.Expresion.evaluar(entrada.ChildNodes[4]), tipo);
                        }
                }

            }

            return null;
        }


        public static Nodo EvaluarConstante(ParseTreeNode entrada)
        {
            if (entrada.ChildNodes.Count == 4) //Declaraciones de variables sin iniciar
            {


                int linea = entrada.ChildNodes[0].Span.Location.Line;
                int columna = entrada.ChildNodes[0].Span.Location.Column;

                string nombre = entrada.ChildNodes[0].Token.Text;
                return new DeclaracionConstante(linea, columna, nombre, Expresion.Expresion.evaluar(entrada.ChildNodes[2]), Objeto.TipoObjeto.CONST);
            }
            else if (entrada.ChildNodes.Count == 6) // declaracion de variable iniciada
            {
                int linea = entrada.ChildNodes[0].Span.Location.Line;
                int columna = entrada.ChildNodes[0].Span.Location.Column;

                string nombre = entrada.ChildNodes[0].Token.Text;
                Objeto.TipoObjeto tipo = getTipo(entrada.ChildNodes[2]);
                return new DeclaracionConstante(linea, columna, nombre, Expresion.Expresion.evaluar(entrada.ChildNodes[4]), tipo);

            }

            return null;
        }

        public static Nodo getObjeto(Objeto.TipoObjeto tipo)
        {
            switch (tipo)
            {
                case Objeto.TipoObjeto.INTEGER:
                    return new Constante(0,0, new Primitivo(tipo, ValoresDefecto(tipo)));
                case Objeto.TipoObjeto.REAL:
                    return new Constante(0, 0, new Primitivo(tipo, ValoresDefecto(tipo)));
                case Objeto.TipoObjeto.STRING:
                    return new Constante(0, 0, new Primitivo(tipo, ValoresDefecto(tipo)));
                case Objeto.TipoObjeto.BOOLEAN:
                    return new Constante(0, 0, new Primitivo(tipo, ValoresDefecto(tipo)));

            }
            return null;
        }

        public static Objeto.TipoObjeto getTipo(ParseTreeNode entrada)
        {
            String tipo = entrada.ChildNodes[0].Term.Name;

            switch (tipo)
            {
                case "integer":
                    return Objeto.TipoObjeto.INTEGER;
                case "string":
                    return Objeto.TipoObjeto.STRING;
                case "real":
                    return Objeto.TipoObjeto.REAL;
                case "boolean":
                    return Objeto.TipoObjeto.BOOLEAN;
            }
            return Objeto.TipoObjeto.NULO;
        }

        public static object ValoresDefecto(Objeto.TipoObjeto tipo)
        {
            switch (tipo)
            {
                case Objeto.TipoObjeto.INTEGER:
                    return 0;
                case Objeto.TipoObjeto.REAL:
                    return 0.0;
                case Objeto.TipoObjeto.BOOLEAN:
                    return false;
                case Objeto.TipoObjeto.STRING:
                    return "\'\'";

            }
            return null;
        }

        public static string[] getId(ParseTreeNode entrada)
        {
            string[] ids = new string[entrada.ChildNodes.Count];

            for (int i = 0; i < entrada.ChildNodes.Count; i++)
            {
                ids[i] = entrada.ChildNodes[i].Token.Text;
            }
            return ids;
        }



    }
}
