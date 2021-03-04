using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.TipoDatos
{
    [Serializable]
    public class Arreglo : Objeto
    {

        private Simbolo[] valores;
        private int inferior;
        private int superior;
        private Objeto contenido;
        public string nombre;

        public Arreglo(int inferior, int superior, Objeto cont) : base(TipoObjeto.ARRAY)
        {
            this.inferior = inferior;
            this.superior = superior;
            this.contenido = cont;
            declarar_arreglo();
        }
 
        public Arreglo():base(TipoObjeto.ARRAY) { }

        public void declarar_arreglo()
        {
            int dimen = ((inferior - superior));
            if (dimen < 0) { dimen = (dimen * -1) +1 ; }
            this.valores = new Simbolo[dimen];

            for(int i = 0; i< this.valores.Length; i++)
            {
                Simbolo simbolo = new Simbolo("", this.contenido.Clonar_Objeto());
                this.valores[i] = simbolo;
            }
        }

        public override Simbolo get_posicion(int posicion)
        {
            if(posicion >= inferior && posicion <= superior)
            {
                posicion = posicion - inferior;
                return valores[posicion];
            }
            throw new Exception("Limites fuera del rango del arreglo: ");
        }


        public override object getValor()
        {
            throw new NotImplementedException();
        }

        public override object toString()
        {
            throw new NotImplementedException();
        }

        public override Simbolo get_atributo(string nombre)
        {
            throw new NotImplementedException();
        }

        public override Objeto Clonar_Objeto()
        {
            return Clone(this);
            
        }

        public static Objeto Clone<Objeto>(Objeto objeto1)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, objeto1);
            stream.Position = 0;
            return (Objeto)formatter.Deserialize(stream);
        }

        public override string getNombre()
        {
            return this.nombre;
        }
    }
}
