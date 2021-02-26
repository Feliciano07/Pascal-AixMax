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

                case "acceso_objeto":
                    return Acceso_objeto(entrada);
                case "acceso_array":
                    return Acceso_arreglo(entrada);
                    
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

        public static Nodo Acceso_objeto(ParseTreeNode entrada)
        {
            Nodo expresion = Expresion.Expresion.evaluar(entrada.ChildNodes[2]);
            return primer_nivel(entrada.ChildNodes[0], expresion);
        }

        public static Nodo primer_nivel(ParseTreeNode entrada, Nodo expresion)
        {
            //TODO: validar si es id o array


            int linea = entrada.ChildNodes[0].Span.Location.Line;
            int columna = entrada.ChildNodes[0].Span.Location.Column;

            string nombre = entrada.ChildNodes[0].Token.Text;

            Acceso primero = new Acceso(linea, columna, nombre, null);

            Acceso retorno = Niveles_abajo(entrada.ChildNodes[2], primero);

            return new Asignacion1(linea, columna, retorno, expresion);

        }

        public static Acceso Niveles_abajo(ParseTreeNode entrada, Acceso primero)
        {
            Acceso auxiliar = null;

            for(int i = 0; i < entrada.ChildNodes.Count; i++)
            {
                //TODO: Validar si es solo id o un arreglo

                if( i == 0)
                {
                    Acceso acceso = new Acceso();
                    acceso = Llamada_id(entrada.ChildNodes[i]);
                    acceso.setAnterior(primero);
                    auxiliar = acceso;
                }
                else
                {
                    Acceso acceso = new Acceso();
                    acceso = Llamada_id(entrada.ChildNodes[i]);
                    acceso.setAnterior(auxiliar);
                    auxiliar = acceso;
                }
            }
            return auxiliar;
        }


        public static Acceso Llamada_id(ParseTreeNode entrada)
        {
            // prueba solo con id
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            string id_variable = entrada.Token.Text;

            return new Acceso(linea, columna, id_variable, null);
        }


        public static Nodo Acceso_arreglo(ParseTreeNode entrada)
        {
            int linea = entrada.ChildNodes[0].Span.Location.Line;
            int columna = entrada.ChildNodes[0].Span.Location.Column;
            Nodo expresion = Expresion.Expresion.evaluar(entrada.ChildNodes[2]);
            return new Asignacion1(linea, columna, Arreglo_Unico(entrada.ChildNodes[0]), expresion);
        }

        public static Acceso Arreglo_Unico(ParseTreeNode entrada)
        {
            int linea = entrada.ChildNodes[0].Span.Location.Line;
            int columna = entrada.ChildNodes[0].Span.Location.Column;

            string nombre = entrada.ChildNodes[0].Token.Text;

            LinkedList<Nodo> dimensiones = Main.lista_expresion(entrada.ChildNodes[2]);

            return new Acceso(linea, columna, nombre, null, dimensiones);
        }

    }
}
