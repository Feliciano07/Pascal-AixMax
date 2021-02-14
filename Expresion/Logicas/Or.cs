using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.TipoDatos;

namespace Pascal_AirMax.Expresion.Logicas
{
    public class Or : Nodo
    {
        private Nodo left;
        private Nodo right;

        public Or(int linea, int columna, Nodo left, Nodo right):base(linea, columna)
        {
            this.left = left;
            this.right = right;
        }

        public override Objeto execute()
        {
            Objeto res_left = left.execute();
            Objeto res_right = right.execute();
            Objeto.TipoObjeto tipo_dominante = TablaTipo.tabla[res_left.getTipo().GetHashCode(), res_right.getTipo().GetHashCode()];

            if (tipo_dominante == Objeto.TipoObjeto.BOOLEAN)
            {
                if (bool.Parse(res_left.getValor().ToString()) == false && bool.Parse(res_right.getValor().ToString()) == false)
                {
                    return new Primitivo(Objeto.TipoObjeto.BOOLEAN, false);
                }
                else
                {
                    return new Primitivo(Objeto.TipoObjeto.BOOLEAN, true);
                }
            }
            else
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                   "No se puede operar OR en tipos" + res_left.getTipo().ToString() + "con" + res_right.getTipo().ToString());
                Maestra.getInstancia.addError(error);
            }

            throw new Exception("La operacion logica OR solo puede hacerse con valores booleanos");
        }
    }
}
