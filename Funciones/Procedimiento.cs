using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Funciones
{
    public class Procedimiento : Funcion
    {

        private LinkedList<Nodo> instruciones; // instruciones validas
        
        public Procedimiento(LinkedList<Parametro> para, LinkedList<Nodo> instru, string nombre):base(0,0,para, nombre)
        {
            this.instruciones = instru;
        }


        public override Objeto execute(Entorno entorno)
        {
            // TODO:
            throw new NotImplementedException();
        }

        public override Objeto executeFuntion(LinkedList<Objeto> actuales)
        {
            throw new NotImplementedException();
        }


    }
}
