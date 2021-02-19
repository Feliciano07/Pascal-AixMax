using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.Sentencias
{
    public class Sentencia_case : Nodo
    {
        private Nodo expresion;
        private LinkedList<Case> casos;

        private LinkedList<Objeto> datos_evaluados;
        
        public Sentencia_case(int linea, int columna, Nodo exp, LinkedList<Case> casos) : base(linea, columna)
        {
            this.expresion = exp;
            this.casos = casos;
            this.datos_evaluados = new LinkedList<Objeto>();
        }

        public override Objeto execute(Entorno entorno)
        {
            Objeto condicion = null;

            try
            {
                condicion = expresion.execute(entorno);
            }catch(Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.ToString());
            }

            foreach(Case caso in casos)
            {
                // TODO: validar si retorna algo o si la condicion fue true
                try
                {
                    caso.execute_caso(entorno, condicion, datos_evaluados);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw new Exception(e.ToString());
                }
            }
            return null;

        }
    }
}
