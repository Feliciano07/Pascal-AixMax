using Pascal_AirMax.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Environment
{
    public class Entorno
    {

        private Dictionary<string, Simbolo> simbolos;
        private Dictionary<string,Funcion> nativas;


        public Entorno()
        {
            this.simbolos = new Dictionary<string, Simbolo>();
            this.nativas = new Dictionary<string, Funcion>();
        }


        //TODO: case sensitive

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
        //TODO: case sensitive

        public bool addFuncion(Funcion ft)
        {
            string name = ft.getNombre().ToLower();

            if (this.nativas.ContainsKey(name) == false)
            {
                this.nativas.Add(name, ft);
                return true;
            }
            return false;
        }

        public Funcion GetFuncion(string id)
        {
            id = id.ToLower();

            if (this.nativas.ContainsKey(id))
            {
                Funcion ft;
                this.nativas.TryGetValue(id, out ft);
                return ft;
            }
            return null;

        }

    }
}
