using System;
using System.Collections.Generic;
using System.Text;

using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.TipoDatos;

namespace Pascal_AirMax.Declaraciones
{
    public class Declaracion_type : Nodo
    {
        private int linea;
        private int columna;
        private LinkedList<Nodo> declaraciones;
        private string nombre;

        public Declaracion_type(int linea, int columna, LinkedList<Nodo> dec, string nombre):base(linea, columna)
        {
            this.declaraciones = dec;
            this.nombre = nombre;
        }

        public override Objeto execute(Entorno entorno)
        {
            if (entorno.ExisteSimbolo(this.nombre))
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                  "Error nombre de simbolo duplicado: " + this.nombre);
                Maestra.getInstancia.addError(error);

                throw new Exception("Error nombre de simbolo duplicado");
            }
            else
            {
                Type_obj nuevo_objeto = new Type_obj(this.nombre);
                nuevo_objeto.entorno_type.Anterior = entorno;
                foreach(Nodo instruccion in declaraciones)
                {
                    try
                    {
                        instruccion.execute(nuevo_objeto.GetEntorno());
                    }catch(Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        throw new Exception(e.ToString());
                    }
                }
                entorno.Anterior = null;
                entorno.addObjeto(nuevo_objeto, this.nombre);
            }

            return null;
        }
    }
}
