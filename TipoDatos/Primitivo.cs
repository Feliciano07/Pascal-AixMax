using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

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

        public override Simbolo get_atributo(string nombre)
        {
            throw new NotImplementedException();
        }

        public override object toString()
        {
            return this.valor.ToString();
        }

        public override Objeto Clonar_Objeto()
        {
            return (Objeto)this.MemberwiseClone();
        }

        public override Simbolo get_posicion(int posicion)
        {
            throw new NotImplementedException();
        }
    }
}
