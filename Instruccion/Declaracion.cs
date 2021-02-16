﻿using Pascal_AirMax.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Instruccion
{
    public class Declaracion : Nodo
    {
        private string[] ids;
        private Nodo expresion;
        private Objeto.TipoObjeto tipo;

        public Declaracion(int linea, int columna, string[] ids, Nodo exp, Objeto.TipoObjeto tipo) : base(linea, columna)
        {
            this.ids = ids;
            this.expresion = exp;
            this.tipo = tipo;
        }

        public override Objeto execute(Entorno entorno)
        {

            foreach (string str in ids)
            {
                if (entorno.ExisteSimbolo(str))
                {
                    Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                   "ya existe la variable:  " + str + "declarada dentro del programa");
                    Maestra.getInstancia.addError(error);

                    throw new Exception("ya existe el simbolo " + "dentro del programa");
                }
                Objeto retorno = null;
                try
                {
                    retorno = expresion.execute(entorno);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Verificar_Tipo_Valor(retorno, str);
                retorno.setTipo(tipo);
                Simbolo simbolo = new Simbolo(str, retorno);
                entorno.addSimbolo(simbolo, str);

            }
            return null;
        }

        public object Verificar_Tipo_Valor(Objeto resultado, string nombre)
        {
            if (resultado.getTipo() == tipo )
            {
                return true;
            }else if(resultado.getTipo() == Objeto.TipoObjeto.INTEGER && tipo == Objeto.TipoObjeto.REAL)
            {
                return true;
            }
            Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                  "La variable: " + nombre + "No es compatible con el dato asignado "+ resultado.getValor().ToString());
            Maestra.getInstancia.addError(error);

            throw new Exception("La variable: " + nombre + "No es compatible con el dato asignado " + resultado.getValor().ToString());
        }

    }
}
