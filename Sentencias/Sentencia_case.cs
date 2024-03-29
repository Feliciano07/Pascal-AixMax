﻿using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.Sentencias
{
    [Serializable]
    public class Sentencia_case : Nodo
    {
        private Nodo expresion;
        private LinkedList<Case> casos;
        private LinkedList<Nodo> instru_else;

        private LinkedList<Objeto> datos_evaluados;
     
        
        public Sentencia_case(int linea, int columna, Nodo exp, LinkedList<Case> casos) : base(linea, columna)
        {
            this.expresion = exp;
            this.casos = casos;
            this.datos_evaluados = new LinkedList<Objeto>();
        }

        public Sentencia_case(int linea, int columna, Nodo exp, LinkedList<Case> casos, LinkedList<Nodo> intru_else):base(linea, columna)
        {
            this.expresion = exp;
            this.casos = casos;
            this.datos_evaluados = new LinkedList<Objeto>();
            this.instru_else = intru_else;
        }

        public override Objeto execute(Entorno entorno)
        {
            Objeto condicion = null;
            this.datos_evaluados.Clear();   
            try
            {
                condicion = expresion.execute(entorno);
            }catch(Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.ToString());
            }

            foreach(Case caso in casos)
            {
                
                if(caso != null)
                {
                    try
                    {
                        // TODO: validar si retorna algo
                        Objeto retorno = caso.execute_caso(entorno, condicion, this.datos_evaluados);
                        if (retorno != null)
                        {
                            if (retorno.getTipo() == Objeto.TipoObjeto.CONTINUE)
                            {
                                return retorno;
                            }
                            else if (retorno.getTipo() == Objeto.TipoObjeto.BREAK)
                            {
                                return retorno;
                            }
                            else if (retorno.getTipo() == Objeto.TipoObjeto.NULO)
                            {
                                return retorno;
                            }
                            else if (retorno.getTipo() == Objeto.TipoObjeto.BOOLEAN)
                            {
                                if (Parser(retorno))
                                {
                                    return null;
                                }
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        //throw new Exception(e.ToString());
                    }
                }
            }
            
            // si llega a este punto es porque no se cumplio ningun caso

            foreach(Nodo instruccion in instru_else)
            {
                if(instruccion != null)
                {
                    try
                    {
                        // TODO: validar si retorna algo
                        Objeto retorno = instruccion.execute(entorno);
                        if (retorno != null)
                        {
                            if (retorno.getTipo() == Objeto.TipoObjeto.CONTINUE)
                            {
                                return retorno;
                            }
                            else if (retorno.getTipo() == Objeto.TipoObjeto.BREAK)
                            {
                                return retorno;
                            }
                            else if (retorno.getTipo() == Objeto.TipoObjeto.NULO)
                            {
                                return retorno;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }

            return null;

        }

        public bool Parser(Objeto valor)
        {
            if(bool.Parse(valor.getValor().ToString()) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
