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
        private LinkedList<Nodo> instru_else;

        private LinkedList<Objeto> datos_evaluados;
     
        
        public Sentencia_case(int linea, int columna, Nodo exp, LinkedList<Case> casos) : base(linea, columna)
        {
            this.expresion = exp;
            this.casos = casos;
            this.datos_evaluados = new LinkedList<Objeto>();
        }

        public Sentencia_case(int linea, int columna, Nodo exp, LinkedList<Case> casos, LinkedList<Nodo> intru_else):base(linea, columna)
        {
            this.expresion = exp;
            this.casos = casos;
            this.datos_evaluados = new LinkedList<Objeto>();
            this.instru_else = intru_else;
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
                
                try
                {
                    // TODO: validar si retorna algo
                    caso.execute_caso(entorno, condicion, this.datos_evaluados);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw new Exception(e.ToString());
                }
            }
            
            // si llega a este punto es porque no se cumplio ningun caso

            foreach(Nodo instruccion in instru_else)
            {
                try
                {
                    // TODO: validar si retorna algo
                    instruccion.execute(entorno);
                }catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return null;

        }
    }
}
