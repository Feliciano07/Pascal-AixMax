using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Funciones;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Instruccion
{
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

            LinkedList<Objeto> actuales = new LinkedList<Objeto>();

            foreach(Nodo node in parametros)
            {
                try
                {
                    actuales.AddLast(node.execute(entorno));
                }
                catch( Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw new Exception(e.ToString());
                }

            }

            Validar_parametros(actuales, retorno);

            return retorno.executeFuntion(actuales);

        }

        public void Validar_parametros(LinkedList<Objeto> actual, Funcion llamada)
        {
            if (string.Compare(llamada.getNombre(), "write") != 0 && string.Compare(llamada.getNombre(), "writeln") != 0)
            {
                if (actual.Count == llamada.getParametros().Count)
                {
                    Parametro[] parame_aux = null;

                    llamada.getParametros().CopyTo(parame_aux, 0);

                    int contador = 0;

                    foreach (Objeto aux in actual)
                    {
                        if (aux.getTipo() == parame_aux[contador].getTipo())
                        {
                            if (aux.getTipo() == Objeto.TipoObjeto.TYPES | aux.getTipo() == Objeto.TipoObjeto.ARRAY)
                            {
                                if (string.Compare(aux.getNombre().ToLower(), parame_aux[contador].getNombre().ToLower()) != 0)
                                {
                                    Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                                        "El parametro: " + parame_aux[contador].getNombreParametro() + " y su valor enviado no coninciden");
                                    Maestra.getInstancia.addError(error);
                                    throw new Exception("El parametro: " + parame_aux[contador].getNombreParametro() + " y su valor enviado no coninciden");
                                }
                            }
                        }
                    }

                }
                else
                {
                    Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                        "La llamada de la funcion: " + llamada.getNombre() + "no tiene los parametros esperados");
                    Maestra.getInstancia.addError(error);
                    throw new Exception("La llamada de la funcion: " + llamada.getNombre() + "no tiene los parametros esperados");
                }
            }
        }

    }
}
