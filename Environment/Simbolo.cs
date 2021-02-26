using System;
using System.Collections.Generic;
using System.Text;

using Pascal_AirMax.Abstract;

namespace Pascal_AirMax.Environment
{
    public class Simbolo
    {
 
        
        public enum Tipo_variable
        {
            VAR = 0,
            CONST =1
        }

        private string nombre;
        private Objeto sym; // primitivos, array, objectos
        private Tipo_variable tipo;

        //TODO: hace falta ver el entorno

        public Simbolo(string nombre, Objeto valor)
        {
            this.nombre = nombre;
            this.sym = valor;
            tipo = Tipo_variable.VAR;
        }

        public Simbolo(string nombre, Objeto valor, Tipo_variable tipo)
        {
            this.nombre = nombre;
            this.sym = valor;
            this.tipo = tipo;
        }

        public Simbolo() { }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public Objeto getValor()
        {
            return this.sym;
        }

        public void setValor(Objeto valor)
        {
            Objeto tem = this.sym;
            this.sym = valor;
            this.sym.setTipo(tem.getTipo());
        }

        public Tipo_variable GetTipo_Variable()
        {
            return this.tipo;
        }

    }
}
