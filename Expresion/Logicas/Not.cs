using System;
using System.Collections.Generic;
using System.Text;

using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.TipoDatos;

namespace Pascal_AirMax.Expresion.Logicas
{
    public class Not : Nodo
    {


        private Nodo right;

        public Not(int linea, int columna, Nodo right):base(linea, columna)
        {
   
            this.right = right;
        }

        public override Objeto execute(Entorno entorno)
        {
            Objeto res_right = right.execute(entorno);

            if(res_right.getTipo() == Objeto.TipoObjeto.BOOLEAN)
            {
                return new Primitivo(Objeto.TipoObjeto.BOOLEAN, !bool.Parse(res_right.getValor().ToString()));
            }
            else
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                   "No se puede operar NOT a un tipos de datos: " + res_right.getTipo().ToString());
                Maestra.getInstancia.addError(error);
            }

            throw new Exception("La operacion logica NOT solo puede hacerse con valores booleanos");
        }
    }
}
