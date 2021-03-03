using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Funciones;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Instruccion
{ 
    public class Writeln : Funcion
    {

        public Writeln(int linea, int columna):base(linea,columna,new LinkedList<Parametro>(), "writeln")
        {

        }

        public override Objeto executar_funcion_usuario(Entorno entorno)
        {
            throw new NotImplementedException();
        }

        public override Objeto execute(Entorno entorno)
        {
            //mandamos a guardar la funcion, pero como nativa
            entorno.addFuncion(this);
            return null;
        }

        public override Objeto executeFuntion(LinkedList<Objeto> actuales)
        {
            if(actuales.Count == 0)
            {
                Maestra.getInstancia.add_output_writeln("");
                return null;
            }

            foreach(Objeto obj in actuales)
            {
                Maestra.getInstancia.add_output_writeln(obj.getValor().ToString());
            }
            Maestra.getInstancia.add_output_writeln("\n");

            return null;
        }
    }
}
