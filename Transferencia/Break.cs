using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.Transferencia
{
    [Serializable]
    public class Break : Nodo
    {

        public Break(int linea, int columna) : base(linea, columna)
        {

        }
        public override Objeto execute(Entorno entorno)
        {
            return new Sentencia_transferencia(Objeto.TipoObjeto.BREAK,base.getLinea(), base.getColumna());
        }
    }
}
