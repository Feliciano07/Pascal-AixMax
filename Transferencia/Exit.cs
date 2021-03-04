using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.Transferencia
{
    [Serializable]
    public class Exit : Nodo
    {
        private Nodo expresion;

        public Exit(int linea, int columna, Nodo expresion) : base(linea, columna)
        {
            this.expresion = expresion;
        }

        public override Objeto execute(Entorno entorno)
        {
            if(expresion != null)
            {
                try
                {

                    Objeto retorno = expresion.execute(entorno);
                    return new Sentencia_transferencia(Objeto.TipoObjeto.NULO, base.getLinea(), base.getColumna(), retorno);

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw new Exception(e.ToString());
                }
            }
            else
            {
                return new Sentencia_transferencia(Objeto.TipoObjeto.NULO, base.getLinea(), base.getColumna(),null);
            }
        }
    }
}
