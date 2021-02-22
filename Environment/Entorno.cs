using Pascal_AirMax.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Environment
{
    public class Entorno
    {

        private Dictionary<string, Simbolo> simbolos;// primitivo, array, objetos
        private Dictionary<string,Funcion> funciones;

        /*
         * Al asignar un valor tomar en cuenta que el id a la izquierda puede ser funciones o simbolos
         */


        public Entorno()
        {
            this.simbolos = new Dictionary<string, Simbolo>();
            this.funciones = new Dictionary<string, Funcion>();
        }


        
        // agregar a la tabla lo que se declara como var, const
        public void addSimbolo(Simbolo simbolo, string nombre)
        {
            nombre = nombre.ToLower();
            this.simbolos.Add(nombre, simbolo);
        }

        

        public bool ExisteSimbolo(string nombre)
        {
            nombre = nombre.ToLower();
            //verifica primero aquello que se declara como var o const
            foreach (string nombre_simbolo in simbolos.Keys)
            {
                if(String.Compare(nombre_simbolo,nombre) == 0)
                {
                    return true;
                }

            }
            //verifica primero aquello que se declara como funcion
            foreach(string nombre_simbolo in funciones.Keys)
            {
                if (String.Compare(nombre_simbolo, nombre) == 0)
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

            if (this.funciones.ContainsKey(name) == false)
            {
                this.funciones.Add(name, ft);
                return true;
            }
            return false;
        }

        public Funcion GetFuncion(string id)
        {
            id = id.ToLower();

            if (this.funciones.ContainsKey(id))
            {
                Funcion ft;
                this.funciones.TryGetValue(id, out ft);
                return ft;
            }
            return null;

        }

    }
}
