using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Asignacion
{
    public class Acceso : Nodo
    {
        private string nombre_variable;
        private Acceso llamada_anterior;

        public Acceso(int linea, int columna, string nombre, Acceso anterior):base(linea,columna)
        {
            this.nombre_variable = nombre;
            this.llamada_anterior = anterior;
        }

        public Acceso():base(0,0)
        {

        }

        public void setAnterior(Acceso anterior)
        {
            this.llamada_anterior = anterior;
        }


        public override Objeto execute(Entorno entorno)
        {
            
            return null;
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
                return simbolo_retorno;
            }
            else
            {
                Simbolo simbolo_retorno = llamada_anterior.retornar_simbolo(entorno);
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
    }
}
