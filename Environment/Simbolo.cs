using System;
using System.Collections.Generic;
using System.Text;

using Pascal_AirMax.Abstract;

namespace Pascal_AirMax.Environment
{
    public class Simbolo
    {
        private string nombre;
        private Objeto sym;

        public Simbolo(string nombre, Objeto valor)
        {
            this.nombre = nombre;
            this.sym = valor;
        }


    }
}
