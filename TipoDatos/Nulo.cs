using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.TipoDatos
{
    [Serializable]
    public class Nulo : Objeto
    {

        public Nulo() : base(TipoObjeto.NULO)
        {

        }

        public override Objeto Clonar_Objeto()
        {
            throw new NotImplementedException();
        }

        public override string getNombre()
        {
            throw new NotImplementedException();
        }

        public override object getValor()
        {
            throw new NotImplementedException();
        }

        public override Simbolo get_atributo(string nombre)
        {
            throw new NotImplementedException();
        }

        public override Simbolo get_posicion(int posicion)
        {
            throw new NotImplementedException();
        }

        public override object toString()
        {
            throw new NotImplementedException();
        }
    }
}
