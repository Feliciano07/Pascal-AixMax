using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;

namespace Pascal_AirMax.Manejador
{
    public static class TablaTipo
    {
        //tabla para la suma

        public static Objeto.TipoObjeto[,] tabla = new Objeto.TipoObjeto[,]
        {
            {Objeto.TipoObjeto.STRING, Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.NULO },
            { Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.INTEGER, Objeto.TipoObjeto.REAL, Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO},
            {Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.REAL, Objeto.TipoObjeto.REAL, Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.NULO },
            {Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.BOOLEAN, Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO },
            {Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.VOID, Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO },
            {Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.ARRAY, Objeto.TipoObjeto.NULO },
            {Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO,Objeto.TipoObjeto.NULO, Objeto.TipoObjeto.OBJECTS }
        };


   

    }
}
