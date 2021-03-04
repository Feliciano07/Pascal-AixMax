using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Funciones
{
    [Serializable]
    public class Parametro:Objeto
    {

        
        public enum Tipo_Parametro
        {
            VALOR,
            REFERENCIA
        }
        private string nombre; // parametros
        private Objeto valor;
        private Tipo_Parametro tipo;


        public Parametro(string nombre_parametro, Objeto.TipoObjeto tipo, Tipo_Parametro tipo_Parametro, Objeto valor): base(tipo)
        {
            this.nombre = nombre_parametro;
            this.valor = valor;
            this.tipo = tipo_Parametro;
        }

        public string getNombreParametro()
        {
            return this.nombre;
        }
        
        public Tipo_Parametro GetTipo_Parametro()
        {
            return this.tipo;
        }


        public override Objeto Clonar_Objeto()
        {
            throw new NotImplementedException();
        }

        public override object getValor()
        {
            throw new NotImplementedException();
        }

        public override Simbolo get_atributo(string nombre)
        {
            throw new NotImplementedException();
        }

        public override Simbolo get_posicion(int posicion)
        {
            throw new NotImplementedException();
        }

        public override object toString()
        {
            throw new NotImplementedException();
        }

        public override string getNombre()
        {
            return this.valor.getNombre();
        }
    }
}
