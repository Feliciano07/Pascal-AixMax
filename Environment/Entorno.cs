using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Environment
{
    public class Entorno
    {

        private Dictionary<string, Simbolo> simbolos;


        public Entorno()
        {
            this.simbolos = new Dictionary<string, Simbolo>();
        }


        public void addSimbolo(Simbolo simbolo, string nombre)
        {
            this.simbolos.Add(nombre, simbolo);
        }


    }
}
