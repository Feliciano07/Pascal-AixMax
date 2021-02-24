using System;
using System.Collections.Generic;
using System.Text;
using Pascal_AirMax.Abstract;
using Pascal_AirMax.Environment;
using Pascal_AirMax.Manejador;
using Pascal_AirMax.TipoDatos;

namespace Pascal_AirMax.Declaraciones
{
    public class Declaracion_Arreglo : Nodo
    {
        private Nodo[] dimensiones;
        private string nombre;
        private Objeto.TipoObjeto tipo;

        public Declaracion_Arreglo(int linea, int columna, Nodo[] dim, string nombre, Objeto.TipoObjeto tipo) : base(linea, columna)
        {
            this.dimensiones = dim;
            this.nombre = nombre;
            this.tipo = tipo;
        }

        public override Objeto execute(Entorno entorno)
        {
            
            if (entorno.ExisteSimbolo(this.nombre))
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "Error simbolo duplicado: " + this.nombre);
                Maestra.getInstancia.addError(error);

                throw new Exception("error simbolo duplicado");
            }

            int niveles = this.dimensiones.Length / 2;

            Objeto arreglo = null;

            try
            {
                arreglo = Niveles_Arreglo(niveles, entorno, 0);
            }catch(Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.ToString());
            }


            Simbolo simbolo = new Simbolo(this.nombre, arreglo);
            entorno.addSimbolo(simbolo,this.nombre);

            return null;
             
        }


        public Arreglo Niveles_Arreglo(int niveles, Entorno entorno, int contador)
        {
            if(niveles == 1)
            {
                int inferior = retornar_limites(entorno,this.dimensiones[contador]);
                contador++;
                int superior = retornar_limites(entorno, this.dimensiones[contador]);
                Validar_limites(inferior, superior);
                return new Arreglo(inferior, superior, get_tipo());
            }
            else
            {
                int inferior = retornar_limites(entorno, this.dimensiones[contador]);
                contador++;
                int superior = retornar_limites(entorno, this.dimensiones[contador]);
                Validar_limites(inferior, superior);
                contador++;
                return new Arreglo(inferior, superior, Niveles_Arreglo(niveles - 1, entorno,contador));
            }
        }


        public Objeto get_tipo()
        {
            switch (this.tipo)
            {
                case Objeto.TipoObjeto.STRING:
                    return new Primitivo(this.tipo, "\'\'");
                case Objeto.TipoObjeto.INTEGER:
                    return new Primitivo(this.tipo, 0);
                case Objeto.TipoObjeto.REAL:
                    return new Primitivo(this.tipo, 0.0);
                case Objeto.TipoObjeto.BOOLEAN:
                    return new Primitivo(this.tipo, false);

                    //TODO: validar si es un tipo objeto o arreglo
            }
            return null;
        }

        public int retornar_limites(Entorno entorno, Nodo limite)
        {
            Objeto retorno = null;
            try
            {
                retorno = limite.execute(entorno);

                if(retorno.getTipo() != Objeto.TipoObjeto.INTEGER)
                {
                    Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "Tipo de dato no valido al expresar el limite del arreglo: " + this.nombre +
                    "Se esperaba un integer y se obtuvo: " + retorno.getTipo().ToString());
                    Maestra.getInstancia.addError(error);

                    throw new Exception("Tipo de dato no valido al expresar el limite del arreglo");
                }
                return int.Parse(retorno.getValor().ToString());

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.ToString());
            }
            
        }

        public void Validar_limites(int inferior, int superior)
        {
            if(inferior > superior)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "el limite superior debe ser mayor o igual al inferior, del arreglo " + this.nombre);
                Maestra.getInstancia.addError(error);
                throw new Exception("Limites incorrestos");
            }
        }


    }
}
