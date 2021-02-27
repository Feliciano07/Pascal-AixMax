using Pascal_AirMax.Environment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Abstract
{
    [Serializable]
    public abstract class Objeto
    {
        public enum TipoObjeto:int
        {
            STRING = 0,
            INTEGER = 1,
            REAL = 2,
            BOOLEAN = 3,
            VOID = 4,
            ARRAY = 5,
            OBJECTS = 6,
            NULO = 10,
            CONST = 11,
            FUNCION = 12,
            TYPES = 13,
            CONTINUE = 14,
            BREAK = 15
        }

        private TipoObjeto tipo;

        public Objeto(TipoObjeto tipo)
        {
            this.tipo = tipo;
        }

        public TipoObjeto getTipo()
        {
            return this.tipo;
        }

        public void setTipo(TipoObjeto tipo)
        {
            this.tipo = tipo;
        }

        public abstract object getValor();

        public abstract object toString();

        public abstract Simbolo get_atributo(string nombre);

        public abstract Objeto Clonar_Objeto();

        public abstract Simbolo get_posicion(int posicion);

       //implementar funcion para clonar
       

    }
}
