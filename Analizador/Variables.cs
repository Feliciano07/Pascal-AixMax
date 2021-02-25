using Irony.Parsing;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Instruccion;
using Pascal_AirMax.TipoDatos;
using Pascal_AirMax.Expresion;
using Pascal_AirMax.Declaraciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Analizador
{
    static class Variables
    {
        //Varialbes de conveniencia para manejar las declaracions de objetos
        


        public static Nodo Lista_variable(ParseTreeNode entrada)
        {
            foreach (ParseTreeNode node in entrada.ChildNodes)
            {
                Manejador.Maestra.getInstancia.addInstruccion(evaluar_variable(node));
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




        public static Nodo evaluar_variable(ParseTreeNode entrada)
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
                            string nombre_type = Nombre_del_tipo(entrada.ChildNodes[2]);
                            return new Declaracion(linea,columna, nombre, getObjeto(tipo), tipo, nombre_type);
                        }
                    case "lista_id":
                        {
                            //TODO: indicar mejor la linea y columna
                            string[] nombre = getId(entrada.ChildNodes[0]);
                            Objeto.TipoObjeto tipo = getTipo(entrada.ChildNodes[2]);
                            string nombre_type = Nombre_del_tipo(entrada.ChildNodes[2]);
                            return new Declaracion(linea, columna, nombre, getObjeto(tipo), tipo, nombre_type);
                        }
                    case "id_constante":
                        {
                            //hace algo?
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
                            string nombre_type = Nombre_del_tipo(entrada.ChildNodes[2]);
                            return new Declaracion(linea, columna, nombre, Expresion.Expresion.evaluar(entrada.ChildNodes[4]), tipo,nombre_type);
                        }
                    case "id_constante":
                        {
                            //hace algo?
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
                default:
                    return Objeto.TipoObjeto.TYPES;
            }
            
        }

        public static string Nombre_del_tipo(ParseTreeNode entrada)
        {
            string tipo = entrada.ChildNodes[0].Token.Text;
            return tipo;
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


        // LO QUE ESTA AQUI PARA ABAJO ES SOLO PARA DECLARAR OBJETOS
        public static Nodo Declaracion_Objeto(ParseTreeNode entrada)
        {
            string nombre = entrada.ChildNodes[1].Token.Text;
            int linea = entrada.ChildNodes[1].Span.Location.Line;
            int columna = entrada.ChildNodes[1].Span.Location.Column;

            if (entrada.ChildNodes.Count == 7)
            {
                LinkedList<Nodo> var_objeto = new LinkedList<Nodo>();
                Variables_Objeto(entrada.ChildNodes[4], var_objeto);

                return new Declaracion_type(linea, columna, var_objeto, nombre);

            }
            else if (entrada.ChildNodes.Count == 6)
            {
                LinkedList<Nodo> var = new LinkedList<Nodo>();
                return new Declaracion_type(linea, columna, var, nombre);
            }
            return null;
        }


        //retorna una lista de nodos 
        public static void Variables_Objeto(ParseTreeNode entrada, LinkedList<Nodo> var_objeto)
        {
           
            foreach (ParseTreeNode node in entrada.ChildNodes)
            {
                declaraciones_objeto(node.ChildNodes[0], var_objeto);
            }

        }

        public static void declaraciones_objeto(ParseTreeNode entrada, LinkedList<Nodo> var_objeto)
        {
            String toke = entrada.Term.Name;

            switch (toke)
            {
              
                case "variable":
                    Variables.Lista_variable_obje(entrada.ChildNodes[1], var_objeto);
                    break;
                    
                case "constante":
                    Lista_constante_obje(entrada.ChildNodes[1], var_objeto);
                    break;
            }
        }


        public static void Lista_variable_obje(ParseTreeNode entrada, LinkedList<Nodo> var_objeto)
        {
            
            foreach (ParseTreeNode node in entrada.ChildNodes)
            {
                var_objeto.AddLast(evaluar_variable(node));
            }
            
        }

        public static void Lista_constante_obje(ParseTreeNode entrada, LinkedList<Nodo> var_objeto)
        {
            
            foreach (ParseTreeNode node in entrada.ChildNodes)
            {
                var_objeto.AddLast(EvaluarConstante(node));
            }
            
        }

        // lo que esta aqui para abajo es solo para manejar los arreglos, que se declaran en el entorno global

        public static void Declaracion_arreglo(ParseTreeNode entrada, int enviroment, LinkedList<Nodo> lista)
        {
            foreach(ParseTreeNode node in entrada.ChildNodes)
            {
                if(enviroment == 0)// global
                {
                    Manejador.Maestra.getInstancia.addInstruccion(definicion_arreglo(node));
                }
                else if(enviroment == 1)
                {
                    lista.AddLast(definicion_arreglo(node));
                }
            }
        }

        public static Nodo definicion_arreglo(ParseTreeNode entrada)
        {
            int linea = entrada.ChildNodes[0].Span.Location.Line;
            int columna = entrada.ChildNodes[0].Span.Location.Column;

            string nombre = entrada.ChildNodes[0].Token.Text;

            Objeto.TipoObjeto tipo = getTipo(entrada.ChildNodes[7]);

            Nodo[] arreglo_dimensio;

            arreglo_dimensio = get_dimensiones(entrada.ChildNodes[4]);

            return new Declaracion_Arreglo(linea, columna, arreglo_dimensio, nombre, tipo);

        }

        public static Nodo[] get_dimensiones(ParseTreeNode entrada)
        {
            
            Nodo[] dimensiones = new Nodo[entrada.ChildNodes.Count*2];
            int contador = 0;
            foreach(ParseTreeNode node in entrada.ChildNodes)
            {
                dimensiones[contador]  = get_dimension(node.ChildNodes[0]);
                contador++;
                dimensiones[contador] = get_dimension(node.ChildNodes[2]);
                contador++;
            }
            return dimensiones;
        }

        public static Nodo get_dimension(ParseTreeNode entrada)
        {
            return Expresion.Expresion.evaluar(entrada);
        }

    }
}
