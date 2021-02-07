using Pascal_AirMax.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Expresion.Aritmetica
{
    public class Resta : Instruction
    {
        private int linea;
        private int columna;
        protected Instruction izq;
        protected Instruction der;

        public Resta(int linea, int columna, Instruction izquierda, Instruction Derecha)
        {
            this.linea = linea;
            this.columna = columna;
            this.izq = izquierda;
            this.der = Derecha;
        }

        public object execute()
        {
            throw new NotImplementedException();
        }
    }
}
