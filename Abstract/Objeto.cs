﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Abstract
{
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
            NULO = 10
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

        public abstract object getValor();

        public abstract object toString();

       

    }
}
