using System;
using System.Collections.Generic;
using System.Text;

using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.Transferencia
{
    [Serializable]
    public class Continue : Nodo
    {

        public Continue(int linea, int columna):base(linea, columna)
        {

        }

        public override Objeto execute(Entorno entorno)
        {
            return new Sentencia_transferencia(Objeto.TipoObjeto.CONTINUE, base.getLinea(), base.getColumna());
        }
    }
}
