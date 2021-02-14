using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;

namespace Pascal_AirMax.TipoDatos
{
    public class Primitivo: Objeto
    {
        public object valor;

        public Primitivo(TipoObjeto tipo, object valor) : base(tipo)
        {
            this.valor = valor;
        }

 
        public override object getValor()
        {
            return this.valor;
        }

        public override object toString()
        {
            return this.valor.ToString();
        }
    }
}
