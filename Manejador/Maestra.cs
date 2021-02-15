using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.Manejador
{
    public sealed class Maestra
    {
        // instancia unica
        private static readonly Maestra instancia = new Maestra();

        private LinkedList<Error> errores = new LinkedList<Error>();

        private LinkedList<Nodo> instrucciones = new LinkedList<Nodo>();

        static Maestra() { }
        private Maestra() { }

        public static Maestra getInstancia
        {
            get
            {
                return instancia;
            }
        }

        public void addError(Error nuevo_error)
        {
            this.errores.AddLast(nuevo_error);
        }

        public void addInstruccion(Nodo nodo)
        {
            this.instrucciones.AddLast(nodo);
        }


        public void ejecutar()
        {
            foreach(Nodo nodo in instrucciones)
            {
                
                try
                {
                    Objeto retorno = nodo.execute();
                }
                catch(Exception e)
                {

                }
            }
        }

        public void clear()
        {
            this.errores = new LinkedList<Error>();
        }

    }
}
