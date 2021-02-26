using System;
using System.Collections.Generic;
using System.Text;

using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Asignacion
{
    /*
     * Esta clase implementara la asignacion de variables que sean sencillas por ejemplos:
     * var1:= 45, var1:= true, var1:= 'hola'
     * objetovariable := otro_objeto
     */

    public class Asignacion1 : Nodo
    {
        private Acceso asignar;
        private Nodo expresion;

        public Asignacion1(int linea, int columna, Acceso buscador , Nodo exp) : base(linea, columna)
        {
            this.asignar = buscador;
            this.expresion = exp;
        }

        public override Objeto execute(Entorno entorno)
        {
            Simbolo simbolo = retornar_asignacion(entorno);

            No_constante(simbolo);

            Objeto valor = retornar_valor_nuevo(entorno);

            Validar_tipos(simbolo.getValor(), valor);

            simbolo.setValor(valor);

            return null;
        }


        public Simbolo retornar_asignacion(Entorno entorno)
        {
            Simbolo simbolo = null;

            try
            {
                simbolo = asignar.retornar_simbolo(entorno);
            }catch(Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.ToString());
            }

            return simbolo;
        }

        public void No_constante(Simbolo simbolo)
        {
            if(simbolo.GetTipo_Variable() == Simbolo.Tipo_variable.CONST)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "No se puede asignar valor a una constante: "+ simbolo.getNombre());
                Maestra.getInstancia.addError(error);
                throw new Exception("No se puede asignar valor a una constante id= " + simbolo.getNombre());
            }
        }

        public Objeto retornar_valor_nuevo(Entorno entorno)
        {
            Objeto valor = null;
            try
            {
                valor = expresion.execute(entorno);
            }catch( Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new Exception(e.ToString());
            }
            return valor;
        }

        public bool Validar_tipos(Objeto variable, Objeto valor_nuevo)
        {
            Objeto.TipoObjeto tipo_dominante = TablaTipo.tabla[variable.getTipo().GetHashCode(), valor_nuevo.getTipo().GetHashCode()];

            if(tipo_dominante == Objeto.TipoObjeto.NULO)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "Tipos de datos incompatibles: " + variable.getTipo().ToString() + ", " + valor_nuevo.getTipo().ToString());
                Maestra.getInstancia.addError(error);
                throw new Exception("tipos de datos incompatibles");
            }else if(tipo_dominante == Objeto.TipoObjeto.REAL && variable.getTipo() == Objeto.TipoObjeto.INTEGER)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "Tipos de datos incompatibles: " + variable.getTipo().ToString() + ", " + valor_nuevo.getTipo().ToString());
                Maestra.getInstancia.addError(error);
                throw new Exception("tipos de datos incompatibles");
            }

            return true;
        }

    }
}
