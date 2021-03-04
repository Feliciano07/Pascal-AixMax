using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.Transferencia;

namespace Pascal_AirMax.Funciones
{
    [Serializable]
    public class Function : Funcion
    {

        private LinkedList<Nodo> instrucciones;
        private Objeto retorno;


        public Function(LinkedList<Parametro> para, LinkedList<Nodo> instru, Objeto retorno, string nombre):base(0,0, para, nombre)
        {
            this.instrucciones = instru;
            this.retorno = retorno;
        }


        public override Objeto executar_funcion_usuario(Entorno entorno)
        {
            foreach (Nodo instruccion in this.instrucciones)
            {
                try
                {
                    Objeto retorno = instruccion.execute(entorno);
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

                        }
                        else if (retorno.getTipo() == Objeto.TipoObjeto.NULO)
                        {
                            Sentencia_transferencia tem = (Sentencia_transferencia)retorno;
                            if (tem.valor != null)
                            {
                                Validar_retorno(tem.valor, instruccion);
                                return tem.valor;
                            }
                            else
                            {
                                Error error = new Error(tem.linea, tem.columna, Error.Errores.Semantico,
                                "La sentencia de exit en funcion debe de retornar un valor");
                                Captura_error(error);
                            }

                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw new Exception(e.ToString());
                }
            }
            //TODO: si llega hasta aca error, ya que tenia que a ver retornardo algo
            Error error2 = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                                "La funcion: "+ base.getNombre() + " no retorna ningun valor");
            Captura_error(error2);
            return null;
        }

        public void Captura_error(Error error)
        {
            Maestra.getInstancia.addError(error);
            throw new Exception(error.descripcion);
        }

        public bool Validar_retorno(Objeto retorno, Nodo instru)
        {
            if(retorno.getTipo() != Objeto.TipoObjeto.ARRAY && retorno.getTipo() != Objeto.TipoObjeto.OBJECTS)
            {
                if(retorno.getTipo() != this.retorno.getTipo() )
                {
                    if(this.retorno.getTipo() == Objeto.TipoObjeto.REAL && retorno.getTipo() == Objeto.TipoObjeto.INTEGER)
                    {
                        return true;
                    }

                    Error error = new Error(instru.getLinea(), instru.getColumna(), Error.Errores.Semantico,
                                "El valor retornado no es valido con el tipo de retorno de la funcion: " + base.getNombre()
                                +" tipos: " + this.retorno.getTipo().ToString() +" "+retorno.getTipo().ToString());
                    Captura_error(error);
                }
            }
            else
            {
                if(string.Compare(retorno.getNombre(), this.retorno.getNombre()) != 0)
                {
                    Error error = new Error(instru.getLinea(), instru.getColumna(), Error.Errores.Semantico,
                               "El valor retornado no es valido con el tipo de retorno de la fucnion: " + base.getNombre()
                               + " tipos: " + this.retorno.getTipo().ToString() + " " + retorno.getTipo().ToString());
                    Captura_error(error);
                }
            }
            return false;
        }

        public override Objeto execute(Entorno entorno)
        {
            throw new NotImplementedException();
        }
        
        public override Objeto executeFuntion(LinkedList<Objeto> actuales)
        {
            throw new NotImplementedException();
        }
    }
}
