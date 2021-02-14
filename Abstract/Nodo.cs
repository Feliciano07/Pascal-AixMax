using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Abstract
{
    public abstract class Nodo
    {
        private int linea;
        private int columna;

        public Nodo(int linea, int columna)
        {
            this.linea = linea;
            this.columna = columna;
        }

        public int getLinea()
        {
            return this.linea;
        }
        public int getColumna()
        {
            return this.columna;
        }


        public abstract Objeto execute();


    }
}
