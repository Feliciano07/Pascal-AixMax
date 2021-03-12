using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Asignacion;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Funciones;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Instruccion
{
    [Serializable]
    public class LlamadaFuncion : Nodo
    {
        private LinkedList<Nodo> parametros;
        private string nombre;


        public LlamadaFuncion(int linea, int columna, string nombre, LinkedList<Nodo> para) : base(linea, columna)
        {
            this.nombre = nombre;
            this.parametros = para;
        }

        public override Objeto execute(Entorno entorno)
        {
            //TODO: validar si es null, retornar excepcion
            Funcion retorno = entorno.GetFuncion(this.nombre); 

            if(retorno == null)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "la llamada de la funcion o procedimiento: " + this.nombre + " no fue encontrado");
                Maestra.getInstancia.addError(error);
                throw new Exception("la llamada de la funcion o procedimiento: " + this.nombre + " no fue encontrado");
            }


            if (Funciones_no_nativas(retorno, entorno))
            {
                return Ejecutar_Writeln_Write(entorno, retorno);
            }else if (Funcion_graficar(retorno))
            {
                return Ejecutar_graficar(entorno, retorno);
            }
            else
            {
                return Ejecutar_Funcion_usuario(entorno, retorno);
            }

        }


        public bool Funciones_no_nativas(Funcion llamada, Entorno entorno)
        {
            if(string.Compare(llamada.getNombre(), "write") ==0 || string.Compare(llamada.getNombre(), "writeln")==0)
            {
                return true;
            }
            return false;
        }

        public bool Funcion_graficar(Funcion llamada)
        {
            if (string.Compare(llamada.getNombre(), "graficar_ts") == 0)
            {
                return true;
            }
            return false;
        }

        //Ejecuta
        public Objeto Ejecutar_Writeln_Write(Entorno entorno, Funcion llamada)
        {
            LinkedList<Objeto> actuales = new LinkedList<Objeto>();
            foreach (Nodo node in this.parametros)
            {
                if(node != null)
                {
                    try
                    {
                        Objeto salida_objeto = node.execute(entorno);
                        actuales.AddLast(salida_objeto);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        throw new Exception(e.ToString());
                    }
                }

            }

            return llamada.executeFuntion(actuales);
        }


        //ejecuta la funcion para graficar tabla de simbolos

        public Objeto Ejecutar_graficar(Entorno entorno, Funcion llamada)
        {
            llamada.setLinea(base.getLinea());
            llamada.setColumna(base.getColumna());
            return llamada.executar_funcion_usuario(entorno);
        }


        //ejecuta
        public Objeto Ejecutar_Funcion_usuario(Entorno entorno, Funcion llamada)
        {
            // Se procede a copiar la lista de parametros a un arreglo
            Parametro[] auxiliar = new Parametro[llamada.getParametros().Count];
            llamada.getParametros().CopyTo(auxiliar, 0);
            //Se procede a validar el total de parametros con el total enviados
            Validar_total_parametros(llamada);

            // enlaza los entornos, con el entorno global del programa
            Entorno nuevo_entorno = new Entorno(entorno.getGlobal(), llamada.getNombre());
            
            int contador = 0;
            foreach (Nodo node in this.parametros)
            {
               if(node != null)
                {
                    try
                    {
                        if (auxiliar[contador].GetTipo_Parametro() == Parametro.Tipo_Parametro.VALOR)
                        {
                            // obtiene lo que viene por valor
                            Objeto salida = node.execute(entorno);
                            Match_Parametro(auxiliar[contador], salida);

                            Simbolo simbolo = new Simbolo(auxiliar[contador].getNombreParametro(), salida, Simbolo.Tipo_variable.VAR, llamada.getLinea(), llamada.getColumna());
                            nuevo_entorno.addSimbolo(simbolo, auxiliar[contador].getNombreParametro());
                        }
                        else
                        {
                            //obtiene lo que se manda por referencia y se guarda ese mismo simbolo
                            if (node is Acceso aux)
                            {
                                Simbolo salida = aux.execute_no_clonador(entorno);
                                Match_Parametro(auxiliar[contador], salida.getValor());
                                nuevo_entorno.addSimbolo(salida, auxiliar[contador].getNombreParametro());
                            }
                            else
                            {
                                Error error = new Error(node.getLinea(), node.getColumna(), Error.Errores.Semantico,
                                "se esperaba un identificador de variable");
                                Maestra.getInstancia.addError(error);
                                throw new Exception("se esperaba un identificador de variable");
                            }

                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        throw new Exception(e.ToString());
                    }
                }
                contador++;
            }


            // TODO: verificar el cambio de esto
            //llamada.setLinea(base.getLinea());
            //llamada.setColumna(base.getColumna());
            auxiliar = null;
            
            Objeto retorno =  llamada.executar_funcion_usuario(nuevo_entorno);

            return retorno;
        }

        public void Validar_total_parametros(Funcion llamada)
        {
            if (this.parametros.Count != llamada.getParametros().Count)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                "La llamada de la funcion: " + llamada.getNombre() + "no tiene los parametros esperados");
                Maestra.getInstancia.addError(error);
                throw new Exception("La llamada de la funcion: " + llamada.getNombre() + "no tiene los parametros esperados");
            }
        }

        public bool Match_Parametro(Parametro parametro, Objeto actual)
        {
            
            if (parametro.getTipo() == actual.getTipo())
            {
                if (parametro.getTipo() == Objeto.TipoObjeto.ARRAY || parametro.getTipo() == Objeto.TipoObjeto.OBJECTS
                    && actual.getTipo() == Objeto.TipoObjeto.ARRAY || actual.getTipo() == Objeto.TipoObjeto.OBJECTS)
                {
                    if (parametro.getNombre() == actual.getNombre())
                    {
                        return true;
                    }
                    Error error1 = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico, "" +
                    "se esperaba un parametro de tipo: " + parametro.getNombre());
                    Maestra.getInstancia.addError(error1);
                    throw new Exception("se esperaba un parametro de tipo: " + parametro.getNombre());
                }

                return true;
            }

            Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico, "" +
                "se esperaba un parametro de tipo: " + parametro.getTipo().ToString());
            Maestra.getInstancia.addError(error);
            throw new Exception("se esperaba un parametro de tipo: " + parametro.getTipo().ToString());
        }

    }
}
