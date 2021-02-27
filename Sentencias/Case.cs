using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Expresion;
using Pascal_AirMax.Expresion.Relacionales;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.TipoDatos;

namespace Pascal_AirMax.Sentencias
{
    public class Case : Nodo
    {
        private LinkedList<Nodo> expresiones;
        private LinkedList<Nodo> instrucciones;

        public Case(int linea, int columna, LinkedList<Nodo> exp, LinkedList<Nodo> instru) : base(linea, columna)
        {
            this.expresiones = exp;
            this.instrucciones = instru;
        }

        public override Objeto execute(Entorno entorno)
        {
            throw new NotImplementedException();
        }

        public  Objeto execute_caso(Entorno entorno, Objeto condicion, LinkedList<Objeto> casos_evaluados)
        {
            foreach(Nodo exp in this.expresiones)
            {
                Objeto actual = null;
                try
                {
                    actual = exp.execute(entorno);


                }catch(Exception e)
                {
                    Console.WriteLine(e);
                    throw new Exception(e.ToString());
                }
                
                Evaluar_Tipo(condicion, actual);
                Evaluar_Caso_anterior(actual, casos_evaluados);
                casos_evaluados.AddLast(actual);

                Igual igual = new Igual(base.getLinea(), base.getColumna(),
                    new Constante(base.getLinea(), base.getColumna(), condicion), new Constante(base.getLinea(), base.getColumna(), actual));

                Objeto salida = null;

                try
                {
                    salida = igual.execute(entorno);
                    
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    throw new Exception(e.ToString());
                }

                if (bool.Parse(salida.getValor().ToString()))
                {
                    foreach (Nodo instruccion in this.instrucciones)
                    {
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
                        }
                    }
                    throw new Exception("se cumplio la condicion");
                }

                

            }
            return null;
        }

        public void Evaluar_Tipo(Objeto condicion, Objeto actual)
        {
            if(condicion.getTipo() != actual.getTipo())
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "La constante y el caso evaluado no son del mismo tipo " + actual.getValor().ToString() + " " + condicion.getValor().ToString());
                Maestra.getInstancia.addError(error);
                throw new Exception("la constante y el caso evaluado no son del mismo tipo");
            }

        }

        public void Evaluar_Caso_anterior(Objeto actual, LinkedList<Objeto> casos_evaluados)
        {
            //TODO: validar otros tipo
            if(actual.getTipo() == Objeto.TipoObjeto.BOOLEAN)
            {
                foreach(Objeto caso in casos_evaluados)
                {
                    if(String.Compare(actual.getValor().ToString().ToLower(), caso.getValor().ToString().ToLower()) == 0)
                    {
                        Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                            "Casos duplicados " + actual.getValor().ToString() + " " + caso.getValor().ToString());
                        Maestra.getInstancia.addError(error);

                        throw new Exception("Casos duplicados " + actual.getValor().ToString() + " " + caso.getValor().ToString());

                    }
                }
            }
            else
            {
                foreach (Objeto caso in casos_evaluados)
                {
                    if (String.Compare(actual.getValor().ToString(), caso.getValor().ToString()) == 0)
                    {
                        Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                            "Casos duplicados " + actual.getValor().ToString() + " " + caso.getValor().ToString());
                        Maestra.getInstancia.addError(error);
                        throw new Exception("Casos duplicados " + actual.getValor().ToString() + " " + caso.getValor().ToString());

                    }
                }
            }
        }
    }
}
