using System;
using System.Collections.Generic;
using System.Text;

namespace Pascal_AirMax.Environment
{
    public class Error
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public String tipoError { get; set; }
        public String descripcion { get; set; }
        public String ambito { get; set; }

        public enum Errores
        {
            Lexico,
            Sintactico,
            Semantico
        }

        public Error(int linea, int columna,Errores tipo, string descripcion)
        {
            this.linea = linea;
            this.columna = columna;
            this.descripcion = descripcion;
            if (tipo == Errores.Lexico) {
                this.tipoError = "Lexico";
            }else if(tipo == Errores.Sintactico)
            {
                this.tipoError = "Sintactico";
            }
            else
            {
                this.tipoError = "Semantico";
            }
        }

        //TODO: posible constructuro de errores especificado el ambito

    }
}
