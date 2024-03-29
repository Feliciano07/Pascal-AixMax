﻿using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Sentencias
{
    [Serializable]
    public class Repeat : Nodo
    {
        private Nodo expresion;
        private LinkedList<Nodo> instrucciones;

        public Repeat(int linea, int columna, Nodo exp, LinkedList<Nodo> instrucciones):base(linea, columna)
        {
            this.expresion = exp;
            this.instrucciones = instrucciones;
        }

        public override Objeto execute(Entorno entorno)
        {
            Objeto condicion = Obtener_condicion(entorno);

            bool salida = Obtener_valor(condicion);

            do
            {
                foreach(Nodo instruccion in this.instrucciones)
                {
                    if (instruccion != null)
                    {
                        try
                        {
                            Objeto retorno = instruccion.execute(entorno);

                            if (retorno != null)
                            {
                                if (retorno.getTipo() == Objeto.TipoObjeto.CONTINUE)
                                {
                                    break;
                                }
                                else if (retorno.getTipo() == Objeto.TipoObjeto.BREAK)
                                {
                                    return null;
                                }
                                else if (retorno.getTipo() == Objeto.TipoObjeto.NULO)
                                {
                                    return retorno;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
                condicion = Obtener_condicion(entorno);
                salida = Obtener_valor(condicion);

            } while (!salida);

            return null;
        }

        public Objeto Obtener_condicion(Entorno entorno)
        {
            Objeto retorno = null;

            try
            {
                retorno = this.expresion.execute(entorno);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.ToString());
            }
            Verificar_boleano(retorno);
            return retorno;
        }

        public void Verificar_boleano(Objeto condicion)
        {
            if (condicion.getTipo() != Objeto.TipoObjeto.BOOLEAN)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "La sentencia while do en su condicion solo acepta valores booleanos o relacionales " +
                    "el tipo de dato actual es: " + condicion.getTipo().ToString());
                Maestra.getInstancia.addError(error);
                throw new Exception("condicion no booleana");
            }
        }

        public bool Obtener_valor(Objeto condicion)
        {
            return bool.Parse(condicion.getValor().ToString());
        }
    }
}
