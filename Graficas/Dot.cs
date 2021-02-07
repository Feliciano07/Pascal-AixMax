using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Irony.Parsing;

namespace Pascal_AirMax.Graficas
{
    public class Dot
    {
        private static int Contador;
        private static String grafo;
        

        private static string path = @"C:\compiladores2\AST\ast.txt";
        private static string Patherrores = @"C:\compiladores2\Error\error.txt";

        public static void GenerarAST(ParseTreeNode raiz)
        {
            String grafodot = getDot(raiz);

            SaveFile(grafodot, path);

            string imagen = path.Replace(".txt", ".jpg");

            try
            {
                var command = string.Format("dot -Tjpg {0} -o {1}", path, imagen);

                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C " + command);

                var proc = new System.Diagnostics.Process();

                proc.StartInfo = procStartInfo;

                proc.Start();

                proc.WaitForExit();

            }
            catch (Exception e)
            {

            }

        }

        public static void GenerarErrores(LinkedList<Pascal_AirMax.Environment.Error> entrada)
        {
            String salida = ReccorerErrores(entrada);
            SaveFile(salida,Patherrores);

            string imagen = Patherrores.Replace(".txt", ".jpg");

            try
            {
                var command = string.Format("dot -Tjpg {0} -o {1}", Patherrores, imagen);

                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C " + command);

                var proc = new System.Diagnostics.Process();

                proc.StartInfo = procStartInfo;

                proc.Start();

                proc.WaitForExit();

            }
            catch (Exception e)
            {

            }
        }

        public static void SaveFile(String cadena, string direccion)
        {
            TextWriter tw = new StreamWriter(direccion);
            tw.WriteLine(cadena);
            tw.Close();

        }



        public static String getDot(ParseTreeNode raiz)
        {
            grafo = "";
            grafo = "digraph  G {";
            grafo += "nodo0[label=\"" + raiz.ToString() + "\" ]; \n";
            Contador = 1;
            recorrerAST("nodo0", raiz);
            grafo += "}";
            return grafo;
        }

        public static void recorrerAST(String padre, ParseTreeNode hijos)
        {

            foreach (ParseTreeNode hijo in hijos.ChildNodes)
            {
                String nombreHijo = "nodo" + Contador.ToString();
                grafo += nombreHijo + "[label=\"" + Escapar(hijo.ToString()) + "\"];\n";
                grafo += padre + "->" + nombreHijo + ";\n";
                Contador++;
                recorrerAST(nombreHijo, hijo);

            }

        }
        public static String Escapar(String cade)
        {
            cade = cade.Replace("\\", "\\\\");
            cade = cade.Replace("\"", "\\\"");
            return cade;
        }

        //metodos para graficar la tabla de errores 

        public static String ReccorerErrores(LinkedList<Pascal_AirMax.Environment.Error> entrada)
        {
            string tabla;
            tabla = "digraph D { \n";
            tabla += "node [shape=plaintext] \n";
            tabla += "some_node [ \n";
            tabla += "label=< \n";
            tabla += "<table border=\"0\" cellborder=\"1\" cellspacing=\"0\"> \n";
            tabla += "<tr> <td bgcolor=\"lightblue\">Tipo</td> <td bgcolor=\"lightblue\">Descripcion</td> <td bgcolor=\"lightblue\">Linea</td> <td bgcolor=\"lightblue\">columna</td> </tr> \n";
            foreach(var item in entrada)
            {
                tabla += "<tr> <td>" + item.tipoError + "</td> <td>" + item.descripcion + "</td> <td> \n" +
                    +item.linea + "</td> <td>" + item.columna + "</td> </tr> \n";
            }
            tabla += " </table>>]; }";
            return tabla;
        }
        
    }
}
