using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Funciones;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Declaraciones
{
    public class Creacion_Parametro : Nodo
    {
        private string id;
        private Objeto.TipoObjeto tipo;
        private string nombre_tipo;
        public Parametro.Tipo_Parametro parametro;
        private Nodo expresion;
        public Creacion_Parametro(int linea, int columna, string id, Objeto.TipoObjeto tipoObjeto, string nombre_tipo,
            Parametro.Tipo_Parametro parametro, Nodo exp):base(linea,columna)
        {
            this.id = id;
            this.tipo = tipoObjeto;
            this.nombre_tipo = nombre_tipo;
            this.parametro = parametro;
            this.expresion = exp;
        }


        public override Objeto execute(Entorno entorno)
        {
            if(this.tipo != Objeto.TipoObjeto.TYPES)
            {
                Objeto retorno = retornar_valor(entorno);
                Verificar_Tipo_Valor(retorno, this.id);
                retorno.setTipo(this.tipo);
                return new Parametro(this.id, this.tipo, this.parametro, retorno);
            }
            else
            {
                Objeto retorno = Buscar_types(entorno);
                return new Parametro(this.id, retorno.getTipo(), this.parametro, retorno);
            }
        }


        public Objeto retornar_valor(Entorno entorno)
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

        public object Verificar_Tipo_Valor(Objeto resultado, string nombre)
        {
            if (resultado.getTipo() == tipo)
            {
                return true;
            }
            else if (resultado.getTipo() == Objeto.TipoObjeto.INTEGER && tipo == Objeto.TipoObjeto.REAL)
            {
                return true;
            }
            Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                  "La variable: " + nombre + "No es compatible con el dato asignado " + resultado.getValor().ToString());
            Maestra.getInstancia.addError(error);

            throw new Exception("La variable: " + nombre + "No es compatible con el dato asignado " + resultado.getValor().ToString());
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
            return entrada.Clonar_Objeto();
        }
    }
}
