using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Sentencias
{
    public class IfElse: Nodo
    {
        private Nodo expresion;
        private LinkedList<Nodo> instru_if;
        private LinkedList<Nodo> instru_else;

        public IfElse(int linea, int columna,Nodo exp, LinkedList<Nodo> instruif, LinkedList<Nodo> instru_else) : base(linea, columna)
        {
            this.expresion = exp;
            this.instru_if = instruif;
            this.instru_else = instru_else;
        }

        public override Objeto execute(Entorno entorno)
        {
            Objeto condicion = null;
            try
            {
                // captura de un error antes
                condicion = expresion.execute(entorno);


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.ToString());
            }

            if (condicion.getTipo() != Objeto.TipoObjeto.BOOLEAN)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "La instruccion if solo acepta valores booleanos");
                Maestra.getInstancia.addError(error);

                throw new Exception("se esperaba un valor boolean en el if");
            }

            if (bool.Parse(condicion.getValor().ToString()) == true) // por si es verdaderos
            {
                foreach (Nodo inst in this.instru_if)
                {
                    //TODO: validar si retorna algo
                    try
                    {
                        inst.execute(entorno);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            else // por si es falso
            {
                foreach (Nodo inst in this.instru_else)
                {
                    //TODO: validar si retorna algo
                    try
                    {
                        inst.execute(entorno);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            return null;
        }
    }
}
