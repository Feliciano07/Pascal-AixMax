using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
namespace Pascal_AirMax.Expresion
{
    public class Constante : Nodo
    {
        Objeto valor;

        public Constante(int linea, int columna, Objeto valor): base(linea, columna)
        {
            this.valor = valor;
        }

       
        public override Objeto execute(Entorno entorno)
        {
            return this.valor;
        }
    }
}
