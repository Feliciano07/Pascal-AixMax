using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Instruccion
{
    public class LlamadaFuncion : Nodo
    {
        private LinkedList<Nodo> parametros;
        private string nombre;


        public LlamadaFuncion(int linea, int columna, string nombre, LinkedList<Nodo> para) : base(linea, columna)
        {
            this.nombre = nombre;
            this.parametros = para;
        }

        public override Objeto execute(Entorno entorno)
        {
            //TODO: validar si es null, retornar excepcion
            Funcion retorno = entorno.GetFuncion(this.nombre); 

            LinkedList<Objeto> actuales = new LinkedList<Objeto>();

            foreach(Nodo node in parametros)
            {
                // esto puede retornar una excepcion

                actuales.AddLast(node.execute(entorno));
            }

            return retorno.executeFuntion(actuales);

        }
    }
}
