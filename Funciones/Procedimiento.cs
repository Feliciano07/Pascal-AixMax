using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.TipoDatos;
using Pascal_AirMax.Transferencia;

namespace Pascal_AirMax.Funciones
{
    [Serializable]
    public class Procedimiento : Funcion
    {

        private LinkedList<Nodo> instruciones; // instruciones validas
        
        public Procedimiento(LinkedList<Parametro> para, LinkedList<Nodo> instru, string nombre):base(0,0,para, nombre)
        {
            this.instruciones = instru;
        }



        public override Objeto execute(Entorno entorno)
        {
            // TODO:
            throw new NotImplementedException();
        }

        public override Objeto executeFuntion(LinkedList<Objeto> actuales)
        {
            throw new NotImplementedException();
        }


        public override Objeto executar_funcion_usuario(Entorno entorno)
        {
            foreach(Nodo instruccion in this.instruciones)
            {
                try
                {
                    Objeto retorno  = instruccion.execute(entorno);
                    if (retorno != null)
                    {
                        if (retorno.getTipo() == Objeto.TipoObjeto.CONTINUE)
                        {
                            Sentencia_transferencia tem = (Sentencia_transferencia)retorno;
                            Error error = new Error(tem.linea, tem.columna, Error.Errores.Semantico,
                                "Sentencia continue debe estar dentro de un ciclo");
                            Captura_error(error);
                        }
                        else if (retorno.getTipo() == Objeto.TipoObjeto.BREAK)
                        {
                            Sentencia_transferencia tem = (Sentencia_transferencia)retorno;
                            Error error = new Error(tem.linea, tem.columna, Error.Errores.Semantico,
                                "Sentencia break debe estar dentro de un ciclo o case");
                            Captura_error(error);
                            
                        }else if(retorno.getTipo() == Objeto.TipoObjeto.NULO)
                        {
                            Sentencia_transferencia tem = (Sentencia_transferencia)retorno;
                            if(tem.valor != null)
                            {
                                Error error = new Error(tem.linea, tem.columna, Error.Errores.Semantico,
                                "La sentencia de exit no debe retornar valor dentro del procedimiento");
                                Captura_error(error);
                            }
                            // contrario termina la ejecucion
                            return new Nulo();
                        }
                    }

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw new Exception(e.ToString());
                }
            }

            return new Nulo();
        }


        public void Captura_error(Error error)
        {
            Maestra.getInstancia.addError(error);
            throw new Exception(error.descripcion);
        }
 

    }
}
