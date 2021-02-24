using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;

namespace Pascal_AirMax.TipoDatos
{
    public class Arreglo : Objeto
    {

        private LinkedList<Objeto> valor;
        private int inferior;
        private int superior;
        private Objeto contenido;

        public Arreglo(int inferior, int superior, Objeto cont) : base(TipoObjeto.ARRAY)
        {
            this.inferior = inferior;
            this.superior = superior;
            this.contenido = cont;
        }


        public override object getValor()
        {
            throw new NotImplementedException();
        }

        public override object toString()
        {
            throw new NotImplementedException();
        }
    }
}
