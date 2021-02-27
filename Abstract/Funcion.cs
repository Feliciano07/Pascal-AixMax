using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Abstract
{
    [Serializable]
    public abstract class Funcion:Nodo
    {

        private LinkedList<Object> parametros;//TODO: cambiar en un futuro a tipo parametro
        private LinkedList<Objeto> actuales;
        private string nombre_fun;

        public Funcion(int linea, int columna, LinkedList<Object> parametros, string nombre): base(linea, columna)
        {
            this.parametros = parametros;
            this.nombre_fun = nombre;
            this.actuales = new LinkedList<Objeto>();
        }


        //obtener parametros de la funcion
        public LinkedList<Object> getParametros()
        {
            return this.parametros;

        }

        public LinkedList<Objeto> getActuales()
        {
            return this.actuales;
        }

        public string getNombre()
        {
            return this.nombre_fun;
        }

        public abstract Objeto executeFuntion(LinkedList<Objeto> actuales);

    }
}
