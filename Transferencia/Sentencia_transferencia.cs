﻿using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.Transferencia
{
    [Serializable]
    public class Sentencia_transferencia : Objeto
    {
        public int linea;
        public int columna;
        public Objeto valor;

        public Sentencia_transferencia(Objeto.TipoObjeto tipo, int linea, int columna): base(tipo)
        {
            this.linea = linea;
            this.columna = columna;
        }

        public Sentencia_transferencia(Objeto.TipoObjeto tipo, int linea, int columna, Objeto valor) : base(tipo)
        {
            this.linea = linea;
            this.columna = columna;
            this.valor = valor;
        }

        public override Objeto Clonar_Objeto()
        {
            throw new NotImplementedException();
        }

        public override string getNombre()
        {
            throw new NotImplementedException();
        }

        public override object getValor()
        {
            throw new NotImplementedException();
        }

        public override Simbolo get_atributo(string nombre)
        {
            throw new NotImplementedException();
        }

        public override Simbolo get_posicion(int posicion)
        {
            throw new NotImplementedException();
        }

        public override object toString()
        {
            throw new NotImplementedException();
        }
    }
}
