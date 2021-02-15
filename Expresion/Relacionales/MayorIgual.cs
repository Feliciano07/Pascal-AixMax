using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Environment;
using Pascal_AirMax.TipoDatos;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.Abstract;

namespace Pascal_AirMax.Expresion.Relacionales
{
    public class MayorIgual : Nodo
    {
        private Nodo left;
        private Nodo right;


        public MayorIgual(int linea, int columna, Nodo left, Nodo right):base(linea, columna)
        {
            this.left = left;
            this.right = right;
        }

        public override Objeto execute(Entorno entorno)
        {
            Objeto res_left = left.execute(entorno);
            Objeto res_right = right.execute(entorno);

            Objeto.TipoObjeto tipo_dominante = TablaTipo.tabla[res_left.getTipo().GetHashCode(), res_right.getTipo().GetHashCode()];

            if (tipo_dominante == Objeto.TipoObjeto.INTEGER)
            {

                if (Int16.Parse(res_left.getValor().ToString()) >= Int16.Parse(res_right.getValor().ToString()))
                {
                    return new Primitivo(Objeto.TipoObjeto.BOOLEAN, true);
                }
                return new Primitivo(Objeto.TipoObjeto.BOOLEAN, false);
            }
            else if (tipo_dominante == Objeto.TipoObjeto.REAL)
            {
                if (Double.Parse(res_left.getValor().ToString()) >= Double.Parse(res_right.getValor().ToString()))
                {
                    return new Primitivo(Objeto.TipoObjeto.BOOLEAN, true);
                }
                return new Primitivo(Objeto.TipoObjeto.BOOLEAN, false);

            }
            else if (tipo_dominante == Objeto.TipoObjeto.STRING)
            {
                if (String.Compare(res_left.getValor().ToString(), res_right.getValor().ToString()) >= 0)
                {
                    return new Primitivo(Objeto.TipoObjeto.BOOLEAN, true);
                }
                return new Primitivo(Objeto.TipoObjeto.BOOLEAN, false);

            }
            else if (tipo_dominante == Objeto.TipoObjeto.BOOLEAN)
            {
                if (bool.Parse(res_left.getValor().ToString()) == true)
                {
                    return new Primitivo(Objeto.TipoObjeto.BOOLEAN, true);
                }
                return new Primitivo(Objeto.TipoObjeto.BOOLEAN, false);
            }
            else
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                   "No se puede aplicar mayor igual que a tipos de datos " + res_left.getTipo().ToString() + " con " + res_right.getTipo().ToString());
                Maestra.getInstancia.addError(error);
            }
            throw new Exception("No se puede aplicar mayor igual que a tipos de datos " + res_left.getTipo().ToString() + " con " + res_right.getTipo().ToString());

        }
    }
}
