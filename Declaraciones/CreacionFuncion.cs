using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Funciones;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.TipoDatos;

namespace Pascal_AirMax.Declaraciones
{
    public class CreacionFuncion:Nodo
    {
        private string nombre_funcion;
        private LinkedList<Nodo> instrucciones;
        private LinkedList<Nodo> parametros;
        private string tipo_retorno;
        private Objeto.TipoObjeto tipo;
        private Nodo expresion;

        public CreacionFuncion(int linea, int columna, string nombre, LinkedList<Nodo> instru, LinkedList<Nodo> para,
            string tipo_retorno, Objeto.TipoObjeto tipo, Nodo exp):base(linea, columna)
        {
            this.nombre_funcion = nombre;
            this.instrucciones = instru;
            this.parametros = para;
            this.tipo_retorno = tipo_retorno;
            this.tipo = tipo;
            this.expresion = exp;
        }

        public override Objeto execute(Entorno entorno)
        {
            if (entorno.ExisteSimbolo(this.nombre_funcion))
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "el nombre de la funcion: " + this.nombre_funcion + " es un nombre duplicado");
                Maestra.getInstancia.addError(error);
                throw new Exception("el nombre de la funcion: " + this.nombre_funcion + " es un nombre duplicado");
            }

            Dictionary<string, Parametro> parametro_procedimiento = new Dictionary<string, Parametro>();

            foreach (Nodo nuevo_parametro in this.parametros)
            {
                try
                {
                    Objeto salida = nuevo_parametro.execute(entorno);

                    Parametro aux = (Parametro)salida;

                    // si existe un parametro repite lo reporta
                    Verificar_nombre_parametros(parametro_procedimiento, aux.getNombreParametro(), nuevo_parametro);

                    parametro_procedimiento.Add(aux.getNombreParametro().ToLower(), aux);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw new Exception(e.ToString());
                }
            }
            if(tipo != Objeto.TipoObjeto.TYPES)
            {
                Objeto salida = retornar_valor(entorno);

                Function nueva = new Function(retornar_lista_parametos(parametro_procedimiento), this.instrucciones, salida, this.nombre_funcion);
                entorno.addFuncion(nueva);
            }
            else
            {
                Objeto salida = Buscar_types(entorno);
                Function nueva = new Function(retornar_lista_parametos(parametro_procedimiento), this.instrucciones, salida, this.nombre_funcion);
                entorno.addFuncion(nueva);
            }
            return null;
        }
        public void Verificar_nombre_parametros(Dictionary<string, Parametro> parametro_procedimiento, string nombre, Nodo instru)
        {
            if (parametro_procedimiento.ContainsKey(nombre))
            {
                Error error = new Error(instru.getLinea(), instru.getColumna(), Error.Errores.Semantico,
                    "nombre de variable duplicada: " + nombre + " en los parametros de la funcion: " + this.nombre_funcion);
                Maestra.getInstancia.addError(error);
                throw new Exception("nombre de variable duplicada: " + nombre + " en los parametros de la funcion: " + this.nombre_funcion);
            }
        }

        public LinkedList<Parametro> retornar_lista_parametos(Dictionary<string, Parametro> entrada)
        {
            LinkedList<Parametro> salida = new LinkedList<Parametro>();
            foreach (Parametro aux in entrada.Values)
            {
                salida.AddLast(aux);
            }

            return salida;
        }

        public Objeto Buscar_types(Entorno entorno)
        {
            Objeto salida = entorno.search_types_entornos(this.tipo_retorno);
            if (salida == null)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "Error identificador no encontrado, no existe el type: " + this.tipo_retorno);
                Maestra.getInstancia.addError(error);
                throw new Exception("Identificador no encontrado");
            }
            return Copia(salida);
        }

        public Objeto Copia(Objeto entrada)
        {
            if (entrada.getTipo() == Objeto.TipoObjeto.OBJECTS)
            {
                Type_obj salida = new Type_obj();
                salida = (Type_obj)entrada.Clonar_Objeto();
                return salida;
            }
            else if (entrada.getTipo() == Objeto.TipoObjeto.ARRAY)
            {
                Arreglo salida = new Arreglo();
                salida = (Arreglo)entrada.Clonar_Objeto();
                return salida;
            }
            return null;
        }

        public Objeto retornar_valor(Entorno entorno)
        {
            Objeto valor = null;
            try
            {
                valor = expresion.execute(entorno);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.ToString());
            }
            return valor;
        }
    }
}
