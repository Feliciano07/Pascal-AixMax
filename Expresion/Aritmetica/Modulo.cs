using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.TipoDatos;

namespace Pascal_AirMax.Expresion.Aritmetica
{
    public class Modulo : Nodo
    {
        private Nodo left;
        private Nodo right;

        public Modulo(int linea, int columna, Nodo left, Nodo right):base(linea, columna)
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
                return new Primitivo(tipo_dominante, Int16.Parse(res_left.getValor().ToString()) % Int16.Parse(res_right.getValor().ToString()));
            }
            else
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                   "No se puede obtener el modulo de tipos de datos" + res_left.getTipo().ToString() + "con" + res_right.getTipo().ToString());
                Maestra.getInstancia.addError(error);
            }


            throw new Exception("No se puede obtener el modulo de tipos de datos" + res_left.getTipo().ToString() + "con" + res_right.getTipo().ToString());

        }
    }
}
