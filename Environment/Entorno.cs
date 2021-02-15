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

        public bool ExisteSimbolo(string nombre)
        {
            foreach(string sym in simbolos.Keys)
            {
                if(String.Compare(sym,nombre,true) == 0)
                {
                    return true;
                }

            }
            return false;
        }


    }
}
