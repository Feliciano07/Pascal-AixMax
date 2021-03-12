using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.TipoDatos;

namespace Pascal_AirMax.Expresion.Aritmetica
{
    [Serializable]
    public class Negativo : Nodo
    {
        private Nodo right;

        public Negativo(int linea, int columna, Nodo right) : base(linea, columna)
        {
            this.right = right;
        }

        public override Objeto execute( Entorno entorno)
        {
            Objeto res_right = right.execute(entorno);

            if(res_right.getTipo() == Objeto.TipoObjeto.INTEGER)
            {
                return new Primitivo(Objeto.TipoObjeto.INTEGER, int.Parse(res_right.getValor().ToString()) * -1);
            }else if(res_right.getTipo() == Objeto.TipoObjeto.REAL)
            {
                return new Primitivo(Objeto.TipoObjeto.REAL, Double.Parse(res_right.getValor().ToString()) * -1);
            }
            else
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "No se pueden asignar valor negativo a tipo de datos: " + res_right.getTipo().ToString());
                Maestra.getInstancia.addError(error);
            }
            throw new Exception("No se pueden asignar valor negativo a tipo de datos: " + res_right.getTipo().ToString());
            
        }
    }
}
