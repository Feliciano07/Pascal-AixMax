using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.TipoDatos;

namespace Pascal_AirMax.Expresion.Aritmetica
{
    public class Div : Nodo
    {
        private Nodo left;
        private Nodo right;

        public Div(int linea, int columna, Nodo left, Nodo right) : base(linea, columna)
        {
            this.left = left;
            this.right = right;
        }

        public override Objeto execute(Entorno entorno)
        {
            Objeto res_left = left.execute(entorno);
            Objeto res_right = right.execute(entorno);
            Objeto.TipoObjeto tipo_dominante = TablaTipo.tabla[res_left.getTipo().GetHashCode(), res_right.getTipo().GetHashCode()];

            if(tipo_dominante == Objeto.TipoObjeto.INTEGER)
            {
                if(int.Parse(res_right.getValor().ToString()) != 0)
                {
                    return new Primitivo(Objeto.TipoObjeto.INTEGER, int.Parse(res_left.getValor().ToString()) / int.Parse(res_right.getValor().ToString()));
                }
                else
                {
                    Error error1 = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                        "No se puede realizar la division entre 0");
                    Maestra.getInstancia.addError(error1);
                    throw new Exception("No se puede realizar la division entre 0");
                }
            }
            Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                  "No se pueden dividir tipos de datos" + res_left.getTipo().ToString() + "con" + res_right.getTipo().ToString());
            Maestra.getInstancia.addError(error);
            throw new Exception("No se pueden dividir tipos de datos" + res_left.getTipo().ToString() + "con" + res_right.getTipo().ToString());
        }
    }
}
