using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Asignacion
{
    [Serializable]
    public class Acceso : Nodo
    {
        private string nombre_variable;
        private Acceso llamada_anterior;

        private LinkedList<Nodo> dimensiones;

        public Acceso(int linea, int columna, string nombre, Acceso anterior):base(linea,columna)
        {
            this.nombre_variable = nombre;
            this.llamada_anterior = anterior;
        }

        public Acceso():base(0,0)
        {

        }

        public Acceso(int linea, int columna, string nombre, Acceso anterior, LinkedList<Nodo> dim) : base(linea, columna)
        {
            this.nombre_variable = nombre;
            this.llamada_anterior = anterior;
            this.dimensiones = dim;
        }

        public void setAnterior(Acceso anterior)
        {
            this.llamada_anterior = anterior;
        }


        public override Objeto execute(Entorno entorno)
        {
       
            Simbolo valor = this.retornar_simbolo(entorno);
            return valor.getValor().Clonar_Objeto();

        }

        public Simbolo execute_no_clonador(Entorno entorno)
        {
            if (this.llamada_anterior == null)
            {
                Simbolo simbolo_retorno = entorno.GetSimbolo(this.nombre_variable);
                if (simbolo_retorno == null)
                {
                    Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                        "se esperaba un identificador de variable");
                    Maestra.getInstancia.addError(error);
                    throw new Exception("se esperaba un identificador de variable");
                }

                if (simbolo_retorno.getValor().getTipo() == Objeto.TipoObjeto.ARRAY)
                {
                    return Recorrer_array(entorno, simbolo_retorno);
                }

                return simbolo_retorno;
            }
            else
            {
                Simbolo simbolo_retorno = llamada_anterior.retornar_simbolo(entorno);

                if (simbolo_retorno.getValor().getTipo() == Objeto.TipoObjeto.ARRAY)
                {
                    simbolo_retorno = Recorrer_array(entorno, simbolo_retorno);
                }

                Validar_Nivel(simbolo_retorno);

                Simbolo atributo_objeto = simbolo_retorno.getValor().get_atributo(this.nombre_variable);
                if (atributo_objeto == null)
                {
                    Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                       "Se esperaba un identificador de variable");
                    Maestra.getInstancia.addError(error);
                    throw new Exception("se esperaba un identificador de variable");
                }

                return atributo_objeto;

            }
        }

        public Simbolo retornar_simbolo(Entorno entorno)
        {
            if (this.llamada_anterior == null)
            {
                Simbolo simbolo_retorno = entorno.GetSimbolo(this.nombre_variable);
                if (simbolo_retorno == null)
                {
                    Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                        "Error el simbolo: " + this.nombre_variable + " no se encontro");
                    Maestra.getInstancia.addError(error);
                    throw new Exception("Error el simbolo: " + this.nombre_variable + " no se encontro");
                }

                if(simbolo_retorno.getValor().getTipo() == Objeto.TipoObjeto.ARRAY)
                {
                    return Recorrer_array(entorno, simbolo_retorno);
                }

                return simbolo_retorno;
            }
            else
            {
                Simbolo simbolo_retorno = llamada_anterior.retornar_simbolo(entorno);

                if(simbolo_retorno.getValor().getTipo() == Objeto.TipoObjeto.ARRAY)
                {
                    simbolo_retorno = Recorrer_array(entorno, simbolo_retorno);
                }

                Validar_Nivel(simbolo_retorno);

                Simbolo atributo_objeto = simbolo_retorno.getValor().get_atributo(this.nombre_variable);
                if(atributo_objeto == null)
                {
                    Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                       "Error el simbolo: " + this.nombre_variable + " no se encontro");
                    Maestra.getInstancia.addError(error);
                    throw new Exception("Error el simbolo: " + this.nombre_variable + " no se encontro");
                }

                return atributo_objeto;

            }
        }

        public void Validar_Nivel(Simbolo simbolo)
        {
            if(simbolo.getValor().getTipo() != Objeto.TipoObjeto.OBJECTS)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "El simbolo: " + this.nombre_variable + "no es de tipo objeto");
                Maestra.getInstancia.addError(error);
                throw new Exception("El simbolo: " + this.nombre_variable + "no es de tipo objeto");
            }
        }


        public Simbolo Recorrer_array(Entorno entorno, Simbolo array)
        {
            foreach(Nodo exp in this.dimensiones)
            {
                if(array.getValor().getTipo() == Objeto.TipoObjeto.ARRAY)
                {
                    Objeto valor = exp.execute(entorno);
                    Validar_Entero(valor);

                    int valor_intero = int.Parse(valor.getValor().ToString());

                    try
                    {
                        array = array.getValor().get_posicion(valor_intero);

                    }
                    catch (Exception e)
                    {
                        Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                            e.ToString() + this.nombre_variable);
                        Maestra.getInstancia.addError(error);
                        throw new Exception(e.ToString());
                    }
                }
                else
                {
                    Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                        "El simbolo definido: " + this.nombre_variable + " no es un arreglo o las dimensiones no son las correctas");
                    Maestra.getInstancia.addError(error);
                    throw new Exception("Dimensiones incorrectas");
                }

            }
            return array;
        }

        public void Validar_Entero(Objeto valor)
        {
            if(valor .getTipo() != Objeto.TipoObjeto.INTEGER)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "La dimension del arreglo: " + this.nombre_variable + " tiene que sen integer");
                Maestra.getInstancia.addError(error);
                throw new Exception("La dimension del arreglo: " + this.nombre_variable + " tiene que sen integer");
            }
        }

    }
}
