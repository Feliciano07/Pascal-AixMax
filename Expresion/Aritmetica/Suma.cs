﻿using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.TipoDatos;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.Environment;

namespace Pascal_AirMax.Expresion.Aritmetica
{
    [Serializable]
    public class Suma : Nodo
    {
        private Nodo left;
        private Nodo right;

       


        public Suma(int linea, int columna, Nodo left, Nodo right): base(linea, columna)
        {
            this.left = left;
            this.right = right;

        }

       
        public override Objeto execute(Entorno entorno)
        {

            Objeto res_left = left.execute(entorno); //izq
            Objeto res_right = right.execute(entorno); // der

            Objeto.TipoObjeto tipo_dominante = TablaTipo.tabla[res_left.getTipo().GetHashCode(), res_right.getTipo().GetHashCode()];
            
            if(tipo_dominante == Objeto.TipoObjeto.INTEGER)
            {
                return new Primitivo(tipo_dominante, int.Parse(res_left.getValor().ToString()) + int.Parse(res_right.getValor().ToString()));
            }else if(tipo_dominante == Objeto.TipoObjeto.STRING)
            {
                return new Primitivo(tipo_dominante, res_left.getValor().ToString() + res_right.getValor().ToString());
            }else if(tipo_dominante == Objeto.TipoObjeto.REAL)
            {
                return new Primitivo(tipo_dominante, double.Parse(res_left.getValor().ToString()) + double.Parse(res_right.getValor().ToString()));
            }
            else
            {
                
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico, 
                    "No se pueden sumar tipos de datos "+ res_left.getTipo().ToString() + " con " + res_right.getTipo().ToString());
                Maestra.getInstancia.addError(error);
            }

            throw new Exception("No se pueden sumar tipos de datos " + res_left.getTipo().ToString() + " con " + res_right.getTipo().ToString());
        }
    }
}
