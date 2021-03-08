using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Funciones;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Instruccion
{
    [Serializable]
    public class Write : Funcion
    {
        public Write(int linea, int columna):base(linea,columna, new LinkedList<Parametro>(), "write")
        {

        }

        public override Objeto executar_funcion_usuario(Entorno entorno)
        {
            throw new NotImplementedException();
        }

        public override Objeto execute(Entorno entorno)
        {
            entorno.addFuncion(this);
            return null;
        }

        public override Objeto executeFuntion(LinkedList<Objeto> actuales)
        {
            if (actuales.Count == 0)
            {
                Maestra.getInstancia.add_output_write("");
                return null;
            }

            foreach (Objeto obj in actuales)
            {
                Maestra.getInstancia.add_output_write(obj.getValor().ToString());
            }

            return null;
        }

        public override Objeto valor_retorno()
        {
            throw new NotImplementedException();
        }
    }
}
