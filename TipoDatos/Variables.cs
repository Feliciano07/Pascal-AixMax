using Pascal_AirMax.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

using Irony.Parsing;

namespace Pascal_AirMax.TipoDatos
{
    static class Variables
    {

        public static Nodo Lista_variable(ParseTreeNode entrada)
        {
            foreach( ParseTreeNode node in entrada.ChildNodes)
            {
                evaluar(node);
            }
            return null;
        }


        public static Nodo evaluar(ParseTreeNode entrada)
        {
            if(entrada.ChildNodes.Count == 2)
            {

            }
        }



    }
}
