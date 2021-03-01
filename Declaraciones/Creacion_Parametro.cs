using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Funciones;

namespace Pascal_AirMax.Declaraciones
{
    public class Creacion_Parametro : Nodo
    {
        private string id;
        private Objeto.TipoObjeto tipo;
        private string nombre_tipo;
        public Parametro.Tipo_Parametro parametro; 
        public Creacion_Parametro(int linea, int columna, string id, Objeto.TipoObjeto tipoObjeto, string nombre_tipo,
            Parametro.Tipo_Parametro parametro):base(linea,columna)
        {
            this.id = id;
            this.tipo = tipoObjeto;
            this.nombre_tipo = nombre_tipo;
            this.parametro = parametro;
        }


        public override Objeto execute(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}
