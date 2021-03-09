using Pascal_AirMax.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.TipoDatos;

namespace Pascal_AirMax.Instruccion
{
    [Serializable]
    public class Declaracion : Nodo
    {
        private string[] ids;
        private Nodo expresion;
        private Objeto.TipoObjeto tipo;
        private string nombre_tipo;

        public Declaracion(int linea, int columna, string[] ids, Nodo exp, Objeto.TipoObjeto tipo, string nombre) : base(linea, columna)
        {
            this.ids = ids;
            this.expresion = exp;
            this.tipo = tipo;
            this.nombre_tipo = nombre;
        }

        public override Objeto execute(Entorno entorno)
        {

            foreach (string str in ids)
            {
                if (entorno.ExisteSimbolo(str))
                {
                    //TODO: posible retornar una exepcion
                    Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                   "Error nombre de simbolo duplicado: " + str);
                    Maestra.getInstancia.addError(error);
                }
                else
                {
                    if(this.tipo != Objeto.TipoObjeto.TYPES)
                    {
                        Objeto retorno = retornar_valor(entorno);
                        Verificar_Tipo_Valor(retorno, str);
                        retorno.setTipo(this.tipo);
                        Simbolo simbolo = new Simbolo(str, retorno, base.getLinea(), base.getColumna());
                        entorno.addSimbolo(simbolo, str);
                    }
                    else
                    {
                        Objeto retorno = Buscar_types(entorno);
                        Simbolo simbolo = new Simbolo(str, retorno, base.getLinea(), base.getColumna());
                        entorno.addSimbolo(simbolo, str);
                    }

                }

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


        public Objeto retornar_valor (Entorno entorno)
        {
            Objeto valor = null;
            try
            {
                valor = expresion.execute(entorno);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.ToString());
            }
            return valor;
        }


        public Objeto Buscar_types(Entorno entorno)
        {
            Objeto salida = entorno.search_types_entornos(this.nombre_tipo);
            if (salida == null)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "Error identificador no encontrado, no existe el type: " + this.nombre_tipo);
                Maestra.getInstancia.addError(error);
                throw new Exception("Identificador no encontrado");
            }
            return Copia(salida);
        }

        public Objeto Copia(Objeto entrada)
        {
            if(entrada.getTipo() == Objeto.TipoObjeto.OBJECTS)
            {
                Type_obj salida = new Type_obj();
                salida = (Type_obj)entrada.Clonar_Objeto();
                return salida;
            }
            else if(entrada.getTipo() == Objeto.TipoObjeto.ARRAY)
            {
                Arreglo salida = new Arreglo();
                salida = (Arreglo)entrada.Clonar_Objeto();
                return salida;
            }
            return null;
        }
    }
}
