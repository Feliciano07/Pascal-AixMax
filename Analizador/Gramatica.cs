using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace Pascal_AirMax.Analizador
{
    public class Gramatica: Grammar
    {
        public Gramatica(): base(false)
        {
            #region ER

            //TODO: seccion de comentarios
            CommentTerminal comentarioSimple = new CommentTerminal("CMS", "//", "\r", "\n", "\r\n", "\u2085", "\u2028", "\u2029");
            CommentTerminal comentarioMulti = new CommentTerminal("CMM", "/*", "*/");
            base.NonGrammarTerminals.Add(comentarioSimple);
            base.NonGrammarTerminals.Add(comentarioMulti);
            // seccion de numero 
            var Entero = new RegexBasedTerminal("entero", "[0-9]+");
            var Decimal = new RegexBasedTerminal("decimal", "[0-9]+[.][0-9]+");
            // Seccion de cadena
            var Cadena = new RegexBasedTerminal("cadena", "\'[^\'\n]*[\']");


            #endregion

            #region TERMINALES

            #endregion


            #region NO_TERMINALES
            NonTerminal init = new NonTerminal("init");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal instruccion = new NonTerminal("instruccion");
            #endregion


            #region GRAMATICA

            init.Rule = instrucciones;

            instrucciones.Rule = MakePlusRule(instrucciones, instruccion);

            instruccion.Rule = Entero
                               | Decimal
                               | Cadena;

            #endregion


            #region PREFERENCIA
            this.Root = init;
            #endregion
        }

    }
}
