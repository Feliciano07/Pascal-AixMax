using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Sentencias
{
    public class For : Nodo
    {
        private Nodo condicion;
        private Nodo expresion;
        private LinkedList<Nodo> instrucciones;
        private bool comportamiento;

        public For(int linea, int columna, Nodo con, Nodo exp, LinkedList<Nodo> ins, bool compor) : base(linea, columna)
        {
            this.condicion = con;
            this.expresion = exp;
            this.instrucciones = ins;
            this.comportamiento = compor;
        }

        public override Objeto execute(Entorno entorno)
        {
            //TODO: implementar asignacion, validar que sea integer


            
            Objeto exp = obtener_expresion(entorno);
            validar_integer(exp);

            int b = Int16.Parse(exp.getValor().ToString());

            // Asignacion se debe agregar aca
            int a = 5;

            if (comportamiento)
            {
                for(int i =a; i >= b; i--)
                {
                    foreach(Nodo instruccion in this.instrucciones)
                    {
                        try
                        {
                            //TODO: validar los retornos
                            instruccion.execute(entorno);
                        }catch(Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
            else
            {
                for (int i = a; i <= b; i++)
                {
                    foreach (Nodo instruccion in this.instrucciones)
                    {
                        try
                        {
                            //validar los retornos
                            instruccion.execute(entorno);
                        }catch(Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
            


            return null;
        }

        public Objeto obtener_expresion(Entorno entorno)
        {
            Objeto valor = null;

            try
            {
                valor = expresion.execute(entorno);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new Exception(e.ToString());
            }
            return valor;
        }

        public void validar_integer(Objeto valor)
        {
            if(valor.getTipo() != Objeto.TipoObjeto.INTEGER)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "Los valores dentro del for deben de ser integer");
                Maestra.getInstancia.addError(error);
                throw new Exception("El valor del for debe ser integer");
            }
        }

        
    }
}
