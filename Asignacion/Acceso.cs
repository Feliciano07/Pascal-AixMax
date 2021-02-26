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
            return null;
        }
    }
}
