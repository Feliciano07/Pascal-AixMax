using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Funciones;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax.Declaraciones
{
    public class CreacionProcedimiento : Nodo
    {
        private string nombre_procedimiento;
        private LinkedList<Nodo> instrucciones;
        private LinkedList<Nodo> parametros;
        
        //tipo retorno sea VOID(no retorna nada)

        public CreacionProcedimiento(int linea, int columna, string nombre, LinkedList<Nodo> instru, LinkedList<Nodo> para) : base(linea, columna)
        {
            this.nombre_procedimiento = nombre;
            this.instrucciones = instru;
            this.parametros = para;
        }
 

        public override Objeto execute(Entorno entorno)
        {

            if (entorno.ExisteSimbolo(this.nombre_procedimiento))
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "el nombre del procedimiento: " + this.nombre_procedimiento + " es un nombre duplicado");
                Maestra.getInstancia.addError(error);
                throw new Exception("el nombre del procedimiento: " + this.nombre_procedimiento + " es un nombre duplicado");
            }

            Dictionary<string,Parametro> parametro_procedimiento = new Dictionary<string,Parametro>();

            foreach(Nodo nuevo_parametro in this.parametros)
            {
                try
                {
                    Objeto salida = nuevo_parametro.execute(entorno);

                    Parametro aux  = (Parametro)salida;

                    // si existe un parametro repite lo reporta
                    Verificar_nombre_parametros(parametro_procedimiento, aux.getNombreParametro(), nuevo_parametro);

                    parametro_procedimiento.Add(aux.getNombreParametro().ToLower(), aux);

                }catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw new Exception(e.ToString());
                }
            }
            Procedimiento nuevo = new Procedimiento(retornar_lista_parametos(parametro_procedimiento), this.instrucciones, this.nombre_procedimiento);

            entorno.addFuncion(nuevo);

            return null;
        }

        public void Verificar_nombre_parametros(Dictionary<string, Parametro> parametro_procedimiento, string nombre, Nodo instru)
        {
            if (parametro_procedimiento.ContainsKey(nombre))
            {
                Error error = new Error(instru.getLinea(), instru.getColumna(), Error.Errores.Semantico,
                    "nombre de variable duplicada: " + nombre+ " en los parametros del procedimiento: "+ this.nombre_procedimiento);
                Maestra.getInstancia.addError(error);
                throw new Exception("nombre de variable duplicada: " + nombre + " en los parametros del procedimiento: " + this.nombre_procedimiento);
            }
        }

        public LinkedList<Parametro> retornar_lista_parametos(Dictionary<string,Parametro> entrada)
        {
            LinkedList<Parametro> salida = new LinkedList<Parametro>();
            foreach(Parametro aux in entrada.Values)
            {
                salida.AddLast(aux);
            }

            return salida;
        }
    }
}
