using Pascal_AirMax.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Expresion.Aritmetica
{
    public class Suma : Instruction
    {
        private int linea;
        private int columna;
        protected Instruction izq;
        protected Instruction der;
        

        public Suma(int linea, int columna, Instruction izquierda, Instruction Derecha)
        {
            this.linea = linea;
            this.columna = columna;
            this.izq = izquierda;
            this.der = Derecha;
        }


        public object execute()
        {
            if(this.izq != null && this.der != null)
            {
                Object salida_izq = izq.execute();
                Object salida_der = der.execute();

                return int.Parse(salida_izq.ToString()) + int.Parse(salida_der.ToString());
            }
            return null;
        }
    }
}
