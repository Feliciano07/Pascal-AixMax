using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Transferencia;

namespace Pascal_AirMax.Manejador
{
    public sealed class Maestra
    {
        // instancia unica
        private static readonly Maestra instancia = new Maestra();

        private LinkedList<Error> errores = new LinkedList<Error>();

        private LinkedList<Nodo> instrucciones = new LinkedList<Nodo>();

        private String Output = "";


        static Maestra() { }
        private Maestra() { }

        public static Maestra getInstancia
        {
            get
            {
                return instancia;
            }
        }

        public void add_output_writeln(string mensaje)
        {
            this.Output += mensaje;
        }

        public void add_output_write(string mensaje)
        {
            this.Output += mensaje;
        }

        public String getOutput()
        {
            return this.Output;
        }


        public void addError(Error nuevo_error)
        {
            this.errores.AddLast(nuevo_error);
        }

        public void addInstruccion(Nodo nodo)
        {
            this.instrucciones.AddLast(nodo);
        }

        public int total_errores_encontrados()
        {
            return this.errores.Count;
        }


        public void ejecutar()
        {
            Entorno entorno = new Entorno("global");
            foreach (Nodo nodo in instrucciones)
            {
                if(nodo != null)
                {
                    try
                    {
                        Objeto retorno = nodo.execute(entorno);

                        if (retorno != null)
                        {
                            if (retorno.getTipo() == Objeto.TipoObjeto.CONTINUE)
                            {
                                Sentencia_transferencia tem = (Sentencia_transferencia)retorno;
                                Error error = new Error(tem.linea, tem.columna, Error.Errores.Semantico,
                                    "Sentencia continue debe estar dentro de un ciclo");
                                this.addError(error);
                            }
                            else if (retorno.getTipo() == Objeto.TipoObjeto.BREAK)
                            {
                                Sentencia_transferencia tem = (Sentencia_transferencia)retorno;
                                Error error = new Error(tem.linea, tem.columna, Error.Errores.Semantico,
                                    "Sentencia break debe estar dentro de un ciclo o case");
                                this.addError(error);
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }    

            }
            entorno.Tabla_general();
        }

        public void clear()
        {
            this.errores.Clear();
            this.instrucciones.Clear();
            this.Output = "";
        }


        public string Errores_encontrados()
        {
            string salida = "";
            foreach(Error error in this.errores)
            {
                salida += "<tr>";
                salida += "<td>" + error.tipoError + "</td>\n";
                salida += "<td>" + error.descripcion + "</td>\n";
                salida += "<td>" + error.linea + "</td>\n";
                salida += "<td>" + error.columna + "</td>\n";
                salida += "</tr>";
            }
            return salida;
        }
        public void ReccorerErrores()
        {
            string ruta = @"C:\compiladores2";
            StreamWriter fichero = new StreamWriter(ruta + "\\" + "reporte_errores" + ".html");
            fichero.WriteLine("<html>");
            fichero.WriteLine("<head><title>Errores</title></head>");
            fichero.WriteLine("<body>");
            fichero.WriteLine("<h2>" + "Errores" + "</h2>");
            fichero.WriteLine("<br></br>");
            fichero.WriteLine("<center>" +
            "<table border=3 width=60% height=7%>");
            fichero.WriteLine("<tr>");
            fichero.WriteLine("<th>Tipo</th>");
            fichero.WriteLine("<th>Descripcion</th>");
            fichero.WriteLine("<th>Linea</th>");
            fichero.WriteLine("<th>Columna</th>");
            fichero.WriteLine("</tr>");
            fichero.WriteLine(Errores_encontrados());
            fichero.Write("</table>");
            fichero.WriteLine("</center>" + "</body>" + "</html>");
            fichero.Close();

        }

    }
}
