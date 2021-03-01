using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Funciones
{
    public class Parametro:Objeto
    {
        private string nombre;
        private Objeto valor;
        
        public enum Tipo_Parametro
        {
            VALOR,
            REFERENCIA
        }

        public Parametro(Objeto.TipoObjeto tipo, string nombre): base(tipo)
        {

        }


        public override Objeto Clonar_Objeto()
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
