using System;
using System.Collections.Generic;
using System.Text;

using Irony.Parsing;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Expresion;
using Pascal_AirMax.Instruccion;

namespace Pascal_AirMax.Analizador
{
    public static class Main
    {

        
        public static LinkedList<Nodo> lista_expresion(ParseTreeNode entrada)
        {
            LinkedList<Nodo> parametros = new LinkedList<Nodo>();

            foreach(ParseTreeNode node in entrada.ChildNodes)
            {
                parametros.AddLast(Expresion.Expresion.evaluar(node));
            }

            return parametros;
        }

        public static Nodo Inst_Writeln(ParseTreeNode entrada)
        {
            int linea = entrada.Span.Location.Line;
            int columna = entrada.Span.Location.Column;

            if (entrada.ChildNodes.Count == 4)
            {
                LinkedList<Nodo> tem;
                tem = lista_expresion(entrada.ChildNodes[2]);
                Manejador.Maestra.getInstancia.addInstruccion(new Writeln(linea, columna));
                return new LlamadaFuncion(linea, columna, "writeln", tem);

            }else if(entrada.ChildNodes.Count == 3)
            {
                LinkedList<Nodo> tem = new LinkedList<Nodo>();
                Manejador.Maestra.getInstancia.addInstruccion(new Writeln(linea, columna));
                return new LlamadaFuncion(linea, columna, "writeln", tem);
            }
            return null;
        }

    }
}
