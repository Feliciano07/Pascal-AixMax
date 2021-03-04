using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Asignacion;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.TipoDatos;

namespace Pascal_AirMax.Sentencias
{
    [Serializable]
    public class For : Nodo
    {
        private Asignacion1 condicion;
        private Nodo expresion;
        private LinkedList<Nodo> instrucciones;
        private bool comportamiento;

        public For(int linea, int columna, Asignacion1 con, Nodo exp, LinkedList<Nodo> ins, bool compor) : base(linea, columna)
        {
            this.condicion = con;
            this.expresion = exp;
            this.instrucciones = ins;
            this.comportamiento = compor;
        }

        public override Objeto execute(Entorno entorno)
        {
            //TODO: implementar asignacion, validar que sea integer

            Simbolo inicio = this.condicion.execute_for(entorno);
            validar_integer(inicio.getValor());

            Objeto final = obtener_expresion(entorno);
            validar_integer(final);

            int b = Int16.Parse(final.getValor().ToString());

            // Asignacion se debe agregar aca
            int a = Int16.Parse(inicio.getValor().getValor().ToString());

            if (comportamiento)
            {
                for(int i =a; i >= b; i--)
                {
                    Primitivo pr = new Primitivo(Objeto.TipoObjeto.INTEGER, i);
                    inicio.setValor(pr);
                    foreach (Nodo instruccion in this.instrucciones)
                    {
                        try
                        {
                            Objeto retorno = instruccion.execute(entorno);

                            if (retorno != null)
                            {
                                if (retorno.getTipo() == Objeto.TipoObjeto.CONTINUE)
                                {
                                    break;
                                }else if (retorno.getTipo() == Objeto.TipoObjeto.BREAK)
                                {
                                    return null;
                                }
                            }


                        }catch(Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }

                }
            }
            else
            {
                for (int i = a; i <= b; i++)
                {
                    Primitivo pr = new Primitivo(Objeto.TipoObjeto.INTEGER, i);
                    inicio.setValor(pr);
                    foreach (Nodo instruccion in this.instrucciones)
                    {
                        try
                        {
                            //validar los retornos
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
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }

                }
            }
            


            return null;
        }

        public Objeto obtener_expresion(Entorno entorno)
        {
            Objeto valor = null;

            try
            {
                valor = expresion.execute(entorno);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new Exception(e.ToString());
            }
            return valor;
        }

        public void validar_integer(Objeto valor)
        {
            if(valor.getTipo() != Objeto.TipoObjeto.INTEGER)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "Los valores dentro del for deben de ser integer");
                Maestra.getInstancia.addError(error);
                throw new Exception("El valor del for debe ser integer");
            }
        }


        public void Analizar_Retornos(Nodo instruccion, Entorno entorno)
        {
            
        }
    }
}
