using Pascal_AirMax.Abstract;
using Pascal_AirMax.TipoDatos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Environment
{
    public class Entorno
    {

        private Dictionary<string, Simbolo> simbolos;// primitivo, array, objetos
        private Dictionary<string,Funcion> funciones;
        private Dictionary<string, Arreglo> arreglos;
        private Dictionary<string, Type_obj> objetos;
       
        /*
         * Al asignar un valor tomar en cuenta que el id a la izquierda puede ser funciones o simbolos
         */


        public Entorno()
        {
            this.simbolos = new Dictionary<string, Simbolo>();
            this.funciones = new Dictionary<string, Funcion>();
            this.arreglos = new Dictionary<string, Arreglo>();
            this.objetos = new Dictionary<string, Type_obj>();
        }


        
        // agregar a la tabla lo que se declara como var, const
        public void addSimbolo(Simbolo simbolo, string nombre)
        {
            nombre = nombre.ToLower();
            this.simbolos.Add(nombre, simbolo);
        }

        public void addArreglo(Arreglo arreglo, string nombre)
        {
            nombre = nombre.ToLower();
            this.arreglos.Add(nombre, arreglo);
        }

        public void addObjeto(Type_obj objeto, string nombre)
        {
            nombre = nombre.ToLower();
            this.objetos.Add(nombre, objeto);
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
            //verifica aquello que se declara como funcion
            foreach(string nombre_simbolo in funciones.Keys)
            {
                if (String.Compare(nombre_simbolo, nombre) == 0)
                {
                    return true;
                }
            }

            foreach (string nombre_simbolo in arreglos.Keys)
            {
                if (String.Compare(nombre_simbolo, nombre) == 0)
                {
                    return true;
                }
            }

            foreach (string nombre_simbolo in objetos.Keys)
            {
                if (String.Compare(nombre_simbolo, nombre) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public Simbolo GetSimbolo(string id)
        {
            id = id.ToLower();
            if (this.simbolos.ContainsKey(id))
            {
                Simbolo obj;
                this.simbolos.TryGetValue(id, out obj);
                return obj;
            }
            return null;
        }

        public Objeto GetObjeto(string id)
        {
            id = id.ToLower();
            if (this.arreglos.ContainsKey(id))
            {
                Arreglo arr;
                this.arreglos.TryGetValue(id, out arr);
                return arr;
            }

            if (this.objetos.ContainsKey(id))
            {
                Type_obj arr;
                this.objetos.TryGetValue(id, out arr);
                return arr;
            }
            return null;
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
