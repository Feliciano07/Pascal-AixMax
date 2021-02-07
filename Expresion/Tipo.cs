using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;

namespace Pascal_AirMax.Expresion
{
    public class Tipo : Instruction
    {
        private int linea;
        private int columna;
        private String valor;
        private TIPOS type;

        public enum TIPOS
        {
            Entero
        }

        public Tipo(int linea, int columna, String valor, TIPOS type)
        {
            this.linea = linea;
            this.columna = columna;
            this.valor = valor;
            this.type = type;
        }

        public object execute()
        {
            if(this.type == TIPOS.Entero)
            {
                return int.Parse(valor);
            }
            return null;
        }
    }
}
