using System;
using System.Collections.Generic;
using System.Text;

using Pascal_AirMax.Abstract;

namespace Pascal_AirMax.Environment
{
    public class Simbolo : Nodo
    {
        private string nombre;
        private Objeto sym;
        private Objeto.TipoObjeto tipo;

        public Simbolo(int linea, int columna, string nombre, Objeto valor, Objeto.TipoObjeto tipo) : base(linea, columna)
        {
            this.nombre = nombre;
            this.sym = valor;
            this.tipo = tipo;
        }

        public override Objeto execute()
        {
            throw new NotImplementedException();
        }

    }
}
