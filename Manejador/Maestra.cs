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

        private String Output = "";


        static Maestra() { }
        private Maestra() { }

        public static Maestra getInstancia
        {
            get
            {
                return instancia;
            }
        }

        public void add_output_writeln(string mensaje)
        {
            this.Output += mensaje;
        }

        public void add_output_write(string mensaje)
        {
            this.Output += mensaje;
        }

        public String getOutput()
        {
            return this.Output;
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
            Entorno entorno = new Entorno();
            foreach (Nodo nodo in instrucciones)
            {
                if(nodo != null)
                {
                    try
                    {
                        Objeto retorno = nodo.execute(entorno);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }    

            }
        }

        public void clear()
        {
            this.errores = new LinkedList<Error>();
        }

    }
}
