using Pascal_AirMax.Abstract;
using Pascal_AirMax.TipoDatos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pascal_AirMax.Environment
{
    [Serializable]
    public class Entorno
    {

        public Dictionary<string, Simbolo> simbolos;// primitivo, array, objetos
        public Dictionary<string,Funcion> funciones;
        public Dictionary<string, Arreglo> arreglos;
        public Dictionary<string, Type_obj> objetos;
        public Entorno anterior;

        public string nombre_entorno;

        /*
         * Al asignar un valor tomar en cuenta que el id a la izquierda puede ser funciones o simbolos
         */
        public Entorno Anterior { get => anterior; set => anterior = value; }


        public Entorno(string nombre)
        {
            this.simbolos = new Dictionary<string, Simbolo>();
            this.funciones = new Dictionary<string, Funcion>();
            this.arreglos = new Dictionary<string, Arreglo>();
            this.objetos = new Dictionary<string, Type_obj>();
            this.anterior = null;
            this.nombre_entorno = nombre;
        }

        public Entorno()
        {
            this.simbolos = new Dictionary<string, Simbolo>();
            this.funciones = new Dictionary<string, Funcion>();
            this.arreglos = new Dictionary<string, Arreglo>();
            this.objetos = new Dictionary<string, Type_obj>();
            this.anterior = null;
        }

        public Entorno(Entorno padre, string nombre)
        {
            this.simbolos = new Dictionary<string, Simbolo>();
            this.arreglos = new Dictionary<string, Arreglo>();
            this.objetos = new Dictionary<string, Type_obj>();
            this.funciones = new Dictionary<string, Funcion>();
            this.anterior = padre;
            this.nombre_entorno = nombre;
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
        
        // TODO: Buscar por entorno esto
        public bool ExisteSimbolo(string nombre)
        {
            nombre = nombre.ToLower();
            for (Entorno e = this; e != null; e = e.anterior)
            {
                //verifica primero aquello que se declara como var o const
                if (e.simbolos.ContainsKey(nombre))
                {
                    return true;
                }
                //verifica aquello que se declara como funcion
                if (e.funciones.ContainsKey(nombre))
                {
                    return true;
                }

                if (e.arreglos.ContainsKey(nombre))
                {
                    return true;
                }

                if (e.objetos.ContainsKey(nombre))
                {
                    return true;
                }
            }

            return false;
        }

        public Simbolo GetSimbolo(string id)
        {
            id = id.ToLower();
            for (Entorno e = this; e!= null; e= e.anterior)
            {
                if (e.simbolos.ContainsKey(id))
                {
                    Simbolo obj;
                    e.simbolos.TryGetValue(id, out obj);
                    return obj;
                }
               
            }
            return null;

        }

        // Al declarar un objeto, como puede declarar una variable objeto dentro
        // buscar en su anterior

        public Objeto search_types_entornos(string id)
        {
            id = id.ToLower();
            for (Entorno e = this; e != null; e = e.anterior)
            {
                Objeto salida = e.GetObjeto(id);
                if (salida != null) { return salida; }
            }
            return null;
        }
        public Objeto GetObjeto(string id)
        {
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

            for (Entorno e = this; e != null; e = e.anterior)
            {
                if (e.funciones.ContainsKey(id))
                {
                    Funcion ft;
                    e.funciones.TryGetValue(id, out ft);
                    return ft;
                }

            }
            return null;

        }

        public Entorno getGlobal()
        {
            Entorno aux = this;
            while(aux.anterior != null)
            {
                aux = aux.anterior;
            }
            return aux;
        }


        public string Retornar_simbolos()
        {
            string salida = "";
            for (Entorno e = this; e != null; e = e.anterior)
            {
                foreach(KeyValuePair<string, Simbolo> kvp in e.simbolos)
                {
                    
                    salida += "<tr>";
                    salida += "<td>" + kvp.Key+ "</td>\n";
                    salida += "<td>" + kvp.Value.getValor().getTipo().ToString() + "</td>\n";
                    salida += "<td>" + e.nombre_entorno + "</td>\n";
                    salida += "<td>" + kvp.Value.getLinea() + "</td>\n";
                    salida += "<td>" + kvp.Value.getColumna() + "</td>\n";
                    salida += "</tr>";
                }
                foreach(Funcion funcion in e.funciones.Values)
                {
                    if(string.Compare(funcion.getNombre(),"write") !=0 & string.Compare(funcion.getNombre(), "writeln") !=0
                        & string.Compare(funcion.getNombre(), "graficar_ts") != 0)
                    {
                        salida += "<tr>";
                        salida += "<td>" + funcion.getNombre() + "</td>\n";
                        salida += "<td>" + funcion.valor_retorno().getTipo().ToString() + "</td>\n";
                        salida += "<td>" + e.nombre_entorno + "</td>\n";
                        salida += "<td>" + funcion.getLinea() + "</td>\n";
                        salida += "<td>" + funcion.getColumna() + "</td>\n";
                        salida += "</tr>";
                    }


                }

            }
            return salida;
        }

        public void Tabla_general()
        {
            string ruta = @"C:\compiladores2";

            StreamWriter fichero = new StreamWriter(ruta + "\\" + "Tabla_general"+ ".html");
            fichero.WriteLine("<html>");
            fichero.WriteLine("<head><title>Simbolos</title></head>");
            fichero.WriteLine("<body>");
            fichero.WriteLine("<h2>"+"Simbolos"+ "</h2>");
            fichero.WriteLine("<br></br>");
            fichero.WriteLine("<center>" +
            "<table border=3 width=60% height=7%>");
            fichero.WriteLine("<tr>");
            fichero.WriteLine("<th>Nombre</th>");
            fichero.WriteLine("<th>Tipo</th>");
            fichero.WriteLine("<th>Ambito</th>");
            fichero.WriteLine("<th>Fila</th>");
            fichero.WriteLine("<th>Columna</th>");
            fichero.WriteLine("</tr>");
            fichero.Write(this.Retornar_simbolos());
            fichero.Write("</table>");
            fichero.WriteLine("</center>" + "</body>" + "</html>");
            fichero.Close();
        }

    }
}
