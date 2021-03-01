using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.Declaraciones
{
    public class CreacionProcedimiento : Nodo
    {
        private string nombre_procedimiento;
        private LinkedList<Nodo> instrucciones;
        private LinkedList<Nodo> parametros;

        public CreacionProcedimiento(int linea, int columna, string nombre, LinkedList<Nodo> instru, LinkedList<Nodo> para) : base(linea, columna)
        {
            this.nombre_procedimiento = nombre;
            this.instrucciones = instru;
            this.parametros = para;
        }
 

        public override Objeto execute(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}
