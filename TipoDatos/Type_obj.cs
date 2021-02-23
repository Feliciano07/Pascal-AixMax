using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.TipoDatos
{
    public class Type_obj : Objeto
    {
        public string nombre;
        public Entorno entorno_type;

        public Type_obj(string nombre) : base(Objeto.TipoObjeto.OBJECTS)
        {
            this.nombre = nombre;
            this.entorno_type = new Entorno();
        }


        public Entorno GetEntorno()
        {
            return this.entorno_type;
        }




        public override object getValor()
        {
            throw new NotImplementedException();
        }

        public override object toString()
        {
            throw new NotImplementedException();
        }
    }
}
