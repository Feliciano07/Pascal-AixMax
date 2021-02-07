using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace Pascal_AirMax.Analizador
{
    public class Gramatica: Grammar
    {
        public Gramatica() : base(false)
        {
            #region ER

            //TODO: seccion de comentarios
            CommentTerminal comentarioSimple = new CommentTerminal("CMS", "//", "\r", "\n", "\r\n", "\u2085", "\u2028", "\u2029");
            CommentTerminal comentarioMulti = new CommentTerminal("CMM", "(*", "*)");
            CommentTerminal comentariMulti2 = new CommentTerminal("CMM2", "{", "}");
            base.NonGrammarTerminals.Add(comentarioSimple);
            base.NonGrammarTerminals.Add(comentarioMulti);
            base.NonGrammarTerminals.Add(comentariMulti2);
            // seccion de numero 
            var Entero = new RegexBasedTerminal("entero", "[0-9]+");
            var Decimal = new RegexBasedTerminal("decimal", "[0-9]+[.][0-9]+");
            // Seccion de cadena
            var Cadena = new RegexBasedTerminal("cadena", "\'[^\'\n]*[\']");
            //Seccion de id
            IdentifierTerminal Id = new IdentifierTerminal("Id");
            #endregion

            #region TERMINALES

            // SECCION DE OPERADORES
            var Tsuma = ToTerm("+");
            var Tresta = ToTerm("-");
            var Tpor = ToTerm("*");
            var Tdiv = ToTerm("/");
            var Tfalse = ToTerm("false");
            var Ttrue = ToTerm("true");

            #endregion


            #region NO_TERMINALES
            NonTerminal init = new NonTerminal("init");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal exp = new NonTerminal("exp");
            #endregion


            #region GRAMATICA

            init.Rule = instrucciones;

            instrucciones.Rule = MakePlusRule(instrucciones, instruccion);

            instruccion.Rule = exp;


            exp.Rule = exp + Tsuma + exp
                       | exp + Tresta + exp
                       | exp + Tpor + exp
                       | exp + Tdiv + exp
                       | Entero
                       | Decimal
                       | Cadena
                       | Ttrue
                       | Tfalse;


            #endregion


            #region PREFERENCIA
            this.Root = init;
            RegisterOperators(1, Associativity.Left, Tsuma, Tresta);
            RegisterOperators(2, Associativity.Left, Tpor, Tdiv);
            #endregion
        }

    }
}
