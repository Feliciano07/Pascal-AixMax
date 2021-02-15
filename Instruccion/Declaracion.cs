using Pascal_AirMax.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Instruccion
{
    public class Declaracion: Nodo
    {
        private string[] ids;
        private Objeto valor;

        public Declaracion(int linea, int columna, string[] ids, Objeto valor) : base(linea, columna)
        {
            this.ids = ids;
            this.valor = valor;
        }

        public override Objeto execute(Entorno entorno)
        {
            
            foreach(string str in ids)
            {
                if (entorno.ExisteSimbolo(str))
                {
                    Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                   "ya existe el simbolo "+str + "dentro del programa" );
                    Maestra.getInstancia.addError(error);

                    throw new Exception("ya existe el simbolo " + "dentro del programa");
                }
                Simbolo sym = new Simbolo(str, valor);
                entorno.addSimbolo(sym, str);
            }

            return null;
        }
    }
}
