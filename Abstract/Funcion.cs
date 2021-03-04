using Pascal_AirMax.Environment;
using Pascal_AirMax.Funciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Abstract
{
    [Serializable]
    public abstract class Funcion:Nodo
    {

        private LinkedList<Parametro> parametros;//TODO: cambiar en un futuro a tipo parametro
        private LinkedList<Objeto> actuales;
        private string nombre_fun;

        public Funcion(int linea, int columna, LinkedList<Parametro> parametros, string nombre): base(linea, columna)
        {
            this.parametros = parametros;
            this.nombre_fun = nombre;
            this.actuales = new LinkedList<Objeto>();
        }


        //obtener parametros de la funcion
        public LinkedList<Parametro> getParametros()
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



        public abstract Objeto executar_funcion_usuario(Entorno entorno);

    }
}
