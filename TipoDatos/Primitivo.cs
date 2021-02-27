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
    public class Primitivo: Objeto
    {
        public object valor;

        public Primitivo(TipoObjeto tipo, object valor) : base(tipo)
        {
            this.valor = valor;
        }



        public override object getValor()
        {
            return this.valor;
        }

        public override Simbolo get_atributo(string nombre)
        {
            throw new NotImplementedException();
        }

        public override object toString()
        {
            return this.valor.ToString();
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

        public override Simbolo get_posicion(int posicion)
        {
            throw new NotImplementedException();
        }
    }
}
