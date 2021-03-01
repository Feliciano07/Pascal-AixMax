using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.Declaraciones
{
    public class CreacionFuncion:Nodo
    {
        private string nombre_funcion;
        private LinkedList<Nodo> instrucciones;
        private LinkedList<Nodo> parametros;
        private string tipo_retorno;
        private Objeto.TipoObjeto tipo;

        public CreacionFuncion(int linea, int columna, string nombre, LinkedList<Nodo> instru, LinkedList<Nodo> para,
            string tipo_retorno, Objeto.TipoObjeto tipo):base(linea, columna)
        {
            this.nombre_funcion = nombre;
            this.instrucciones = instru;
            this.parametros = para;
            this.tipo_retorno = tipo_retorno;
            this.tipo = tipo;
        }

        public override Objeto execute(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}
