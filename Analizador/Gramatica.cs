using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace Pascal_AirMax.Analizador
{
    public class Gramatica: Grammar
    {
        public Gramatica() : base( caseSensitive: false)
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

            // SECCION DE OPERADORES aritmeticos
            var Tsuma = ToTerm("+");
            var Tresta = ToTerm("-");
            var Tpor = ToTerm("*");
            var Tdiv = ToTerm("/");
            var Tmod = ToTerm("mod");
            var Tfalse = ToTerm("false");
            var Ttrue = ToTerm("true");
            //Seccion de operadores relacionales
            var Tmayorq = ToTerm(">");
            var Tmenorq = ToTerm("<");
            var Tmayori = ToTerm(">=");
            var Tmenori = ToTerm("<=");
            var Tigual = ToTerm("="); // para comparar
            var Tdiferencia = ToTerm("<>"); // son diferentes

            //seccion de operaciones logicas
            var Tand = ToTerm("and");
            var Tor = ToTerm("or");
            var Tnot = ToTerm("not");

            var TparA = ToTerm("(");
            var TparC = ToTerm(")");

            //declaracion de variables y constantes, asi mismo inicar con un valor
            var Tvar = ToTerm("var");
            var TCon = ToTerm("const");
            var Tpuntocoma = ToTerm(";");
            var Tcoma = ToTerm(",");
            var Tdospunto = ToTerm(":");
            var Tasignar = ToTerm(":=");


            //TIPO DE DATOS VALIDOS
            var Tstring = ToTerm("string");
            var Tinteger = ToTerm("integer");
            var Treal = ToTerm("real");
            var Tboolean = ToTerm("boolean");

            //TODO: falta los tipos de datos void, objects y array

            var Ttype = ToTerm("type");
            var Tarray = ToTerm("array");
            var Tof = ToTerm("of");
            var Tdimension = ToTerm("..");
            var TcorA = ToTerm("[");
            var TcorC = ToTerm("]");

            var Tobject = ToTerm("object");
            var Tend = ToTerm("end");
            var Tbegin = ToTerm("begin");

            var Tif = ToTerm("if");
            var Tthen = ToTerm("then");
            var Twriteln = ToTerm("writeln");
            var Telse = ToTerm("else");
            


            #endregion


            #region NO_TERMINALES
            NonTerminal init = new NonTerminal("init");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal exp = new NonTerminal("exp");
            NonTerminal variable = new NonTerminal("variable");
            NonTerminal tipo_dato = new NonTerminal("tipo_dato");
            NonTerminal lista_id = new NonTerminal("lista_id");
            NonTerminal lista_dec = new NonTerminal("lista_dec");
            NonTerminal lista_variable = new NonTerminal("lista_variable");

            NonTerminal constante = new NonTerminal("constante");
            NonTerminal lista_constante = new NonTerminal("lista_constante");
            NonTerminal id_constante = new NonTerminal("id_constante");

            NonTerminal arrays = new NonTerminal("arrays");
            NonTerminal lista_arrays = new NonTerminal("lista_arrays");
            NonTerminal id_arrays = new NonTerminal("id_arrays");

            NonTerminal objectos = new NonTerminal("objectos");


            NonTerminal lista_main = new NonTerminal("lista_main");
            NonTerminal main = new NonTerminal("main");
            NonTerminal opciones_main = new NonTerminal("opciones_main");

            NonTerminal asignacion = new NonTerminal("asignacion");

            NonTerminal writeln = new NonTerminal("writeln");


            NonTerminal ifthen = new NonTerminal("ifthen");
            NonTerminal opcion_if = new NonTerminal("opcion_if");
            NonTerminal opcion_else = new NonTerminal("opcion_else");

            NonTerminal ifelse = new NonTerminal("ifelse");
            NonTerminal optionIfElse = new NonTerminal("optionIfElse");


            #endregion


            #region GRAMATICA

            init.Rule = opciones_main;

            instrucciones.Rule = MakePlusRule(instrucciones, instruccion);

            // todo esto es lo que estaria arriba antes del main, exepto exp
            instruccion.Rule = exp
                              | variable
                              | constante
                              | arrays
                              | objectos
                              ;

            opciones_main.Rule = Tbegin + lista_main + Tend + Tpuntocoma
                                | lista_main;

            exp.Rule = exp + Tsuma + exp
                       | exp + Tresta + exp
                       | exp + Tpor + exp
                       | exp + Tdiv + exp
                       | exp + Tmod + exp
                       | exp + Tmayorq + exp
                       | exp + Tmenorq + exp
                       | exp + Tmayori + exp
                       | exp + Tmenori + exp
                       | exp + Tigual + exp
                       | exp + Tdiferencia + exp
                       | exp + Tand + exp
                       | exp + Tor + exp
                       | Tnot + exp
                       | Tresta + exp
                       | Entero
                       | Decimal
                       | Cadena
                       | Ttrue
                       | Tfalse
                       | TparA + exp + TparC;

            tipo_dato.Rule = Tstring
                             | Tinteger
                             | Treal
                             | Tboolean;

            //************************ DECLARACION DE VARIABLES
            variable.Rule = Tvar + lista_variable;

            lista_variable.Rule = MakePlusRule(lista_variable, lista_dec);

            //problema conflicto id o id,
            lista_dec.Rule = lista_id + Tdospunto + tipo_dato + Tpuntocoma
                             | Id + Tdospunto + tipo_dato + Tigual + exp + Tpuntocoma
                             | Id + Tdospunto + tipo_dato + Tpuntocoma;


            lista_id.Rule = MakeListRule(lista_id, Tcoma, Id);

            //*************************** DECLARACION DE CONSTANTES

            constante.Rule = TCon + lista_constante;

            lista_constante.Rule = MakePlusRule(lista_constante, id_constante);

            id_constante.Rule = Id + Tdospunto + tipo_dato + Tigual + exp + Tpuntocoma
                               | Id + Tigual + exp + Tpuntocoma;


            //***************************** DECLARACION DE UN ARRAY

            arrays.Rule = Ttype + lista_arrays;

            lista_arrays.Rule = MakePlusRule(lista_arrays, id_arrays);

            id_arrays.Rule = Id + Tigual + Tarray + TcorA + exp + Tdimension + exp +TcorC + Tof + tipo_dato + Tpuntocoma;

            //******************************* DECLARACION DE OBJECTOS

            objectos.Rule = Ttype + Id + Tigual + Tobject + instrucciones + Tend + Tpuntocoma
                           | Ttype + Id + Tigual + Tobject + Tend + Tpuntocoma; ;


            // ******************* flujo interno del programa, sentencias y asignaciones
            lista_main.Rule = MakePlusRule(lista_main, main);



            main.Rule = asignacion + Tpuntocoma
                       | writeln + Tpuntocoma
                       | ifthen
                       | ifelse;

            asignacion.Rule = Id + Tasignar + exp;

            writeln.Rule = Twriteln + TparA + exp + TparC;

            //**************************** sentencias if then

            ifthen.Rule = Tif + exp + Tthen + main
                          | Tif + exp + Tthen + Tbegin + lista_main + Tend + Tpuntocoma;



            //**************************** SENTENCIA IF THEN ELSE

            ifelse.Rule = Tif + exp + Tthen + opcion_if + Telse + opcion_else ;


            optionIfElse.Rule = asignacion
                               | writeln;

            opcion_if.Rule = optionIfElse
                            | Tbegin + lista_main + Tend;

            opcion_else.Rule = main
                              | Tbegin + lista_main + Tend + Tpuntocoma;


            #endregion


            #region PREFERENCIA
            this.Root = init;
            RegisterOperators(3, Associativity.Left, Tor);
            RegisterOperators(4, Associativity.Left, Tand);
            RegisterOperators(5, Associativity.Left, Tigual, Tdiferencia);
            RegisterOperators(6, Associativity.Left, Tmayorq, Tmenorq, Tmayori, Tmenori);
            RegisterOperators(7, Associativity.Left, Tsuma, Tresta);
            RegisterOperators(8, Associativity.Left, Tpor, Tdiv, Tmod); // aca se agrega el modulo
            RegisterOperators(9, Associativity.Right, Tresta, Tnot);
            RegisterOperators(10, Associativity.Left, TparA, TparC);

            #endregion
        }

    }
}
