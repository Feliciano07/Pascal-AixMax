using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using System.IO;

namespace Pascal_AirMax.Instruccion
{
    public class GraficarTs : Funcion
    {

        public GraficarTs(int linea, int columna):base(linea,columna, new LinkedList<Funciones.Parametro>(), "graficar_ts")
        {

        }

        public override Objeto executar_funcion_usuario(Entorno entorno)
        {
            Crear_Archivo(entorno);
            return null;
        }

        public override Objeto execute(Entorno entorno)
        {
            entorno.addFuncion(this);
            return null;
        }



        public void Crear_Archivo(Entorno entorno)
        {
            string ruta = @"C:\compiladores2\Tablas";

            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            StreamWriter fichero = new StreamWriter(ruta +"\\" +entorno.nombre_entorno + ".html");
            fichero.WriteLine("<html>");
            fichero.WriteLine("<head><title>Fucion graficar_ts</title></head>");
            fichero.WriteLine("<body>");
            fichero.WriteLine("<h2>Funcion Graficar Linea: " + this.getLinea() + " Columna:" + this.getColumna() + "</h2>");
            fichero.WriteLine("<br></br>");
            fichero.WriteLine("<center>" +
            "<table border=3 width=60% height=7%>");
            fichero.WriteLine("<tr>");
            fichero.WriteLine("<th>Nombre</th>");
            fichero.WriteLine("<th>Tipo</th>");
            fichero.WriteLine("<th>Ambito</th>");
            fichero.WriteLine("<th>Fila</th>");
            fichero.WriteLine("<th>Columna</th>");
            fichero.WriteLine("</tr>");
            fichero.Write(entorno.Retornar_simbolos());
            fichero.Write("</table>");
            fichero.WriteLine("</center>" + "</body>" + "</html>");
            fichero.Close();
           
        }
        // esta se ejecuta
        public override Objeto executeFuntion(LinkedList<Objeto> actuales)
        {
            // escritura de archivo
            throw new NotImplementedException();
        }

        public override Objeto valor_retorno()
        {
            throw new NotImplementedException();
        }
    }
}
