using System;
using System.Collections.Generic;
using System.Text;

using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Sentencias
{
    [Serializable]
    public class IFthen: Nodo
    {
        private Nodo exp;
        private LinkedList<Nodo> instrucciones;
        

        public IFthen(int linea, int columna, Nodo exp, LinkedList<Nodo> instru):base(linea, columna)
        {
            this.exp = exp;
            this.instrucciones = instru;
        }

        public override Objeto execute(Entorno entorno)
        {
            Objeto condicion = null;
            try
            {
                // captura de un error antes
                condicion = exp.execute(entorno);


            }catch(Exception e) { 
                Console.WriteLine(e);
                throw new Exception(e.ToString());
            }

            if(condicion.getTipo() != Objeto.TipoObjeto.BOOLEAN)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "La instruccion if solo acepta valores booleanos");
                Maestra.getInstancia.addError(error);

                throw new Exception("se esperaba un valor boolean en el if");
            }

            if(bool.Parse(condicion.getValor().ToString()) == true)
            {
                foreach(Nodo instruccion in this.instrucciones)
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
                        else if (retorno.getTipo() == Objeto.TipoObjeto.NULO)
                        {
                            return retorno;
                        }
                    }
                }
            }
            return null;
            
        }
    }
}
