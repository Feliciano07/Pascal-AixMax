using System;
using System.Collections.Generic;
using System.Text;

using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Instruccion
{
    public class DeclaracionConstante : Nodo
    {
        private string nombre;
        private Nodo expresion;
        private Objeto.TipoObjeto tipo;

        public DeclaracionConstante(int linea, int columna, string nombre, Nodo exp, Objeto.TipoObjeto tipo) :base(linea, columna)
        {
            this.nombre = nombre;
            this.expresion = exp;
            this.tipo = tipo;
        }

        public override Objeto execute(Entorno entorno)
        {
            Objeto retorno = retornar_valor(entorno);

            if (entorno.ExisteSimbolo(nombre))
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                  "Error nombre de simbolo duplicado: " + nombre);
                Maestra.getInstancia.addError(error);
            }
            else
            {
                Verificar_Tipo_Valor(retorno, nombre);
                retorno.setTipo(this.tipo);
                Simbolo simbolo = new Simbolo(nombre, retorno, Simbolo.Tipo_variable.CONST, base.getLinea(), base.getColumna());
                entorno.addSimbolo(simbolo, nombre);
            }

            return null;
        }

        public object Verificar_Tipo_Valor(Objeto resultado, string nombre)
        {
            if (resultado.getTipo() == tipo | tipo == Objeto.TipoObjeto.CONST | tipo == Objeto.TipoObjeto.REAL
                | resultado.getTipo() == Objeto.TipoObjeto.INTEGER)
            {
                return true;
            }
            Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                  "La constante: " + nombre + "No es compatible con el dato asignado " + resultado.getValor().ToString());
            Maestra.getInstancia.addError(error);

            throw new Exception("La constante: " + nombre + "No es compatible con el dato asignado " + resultado.getValor().ToString());
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
