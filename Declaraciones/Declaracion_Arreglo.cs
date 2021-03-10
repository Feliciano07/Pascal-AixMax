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
        private string nombre_tipo;

        public Declaracion_Arreglo(int linea, int columna, Nodo[] dim, string nombre, Objeto.TipoObjeto tipo, string nombre_tipo) : base(linea, columna)
        {
            this.dimensiones = dim;
            this.nombre = nombre;
            this.tipo = tipo;
            this.nombre_tipo = nombre_tipo;
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

            Arreglo arreglo = null;

            try
            {
                arreglo = Niveles_Arreglo(niveles, entorno, 0);
            }catch(Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.ToString());
            }

            arreglo.nombre = this.nombre.ToLower();

            Simbolo simbolo = new Simbolo(this.nombre, arreglo, base.getLinea(), base.getColumna());

            entorno.addSimbolo(simbolo, this.nombre);

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
                return new Arreglo(inferior, superior, get_tipo(entorno));
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


        public Objeto get_tipo(Entorno entorno)
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
                case Objeto.TipoObjeto.TYPES:
                    return Buscar_types(entorno);
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



        public Objeto Buscar_types(Entorno entorno)
        {
            Objeto salida = entorno.search_types_entornos(this.nombre_tipo);
            if (salida == null)
            {
                Error error = new Error(base.getLinea(), base.getColumna(), Error.Errores.Semantico,
                    "Error identificador no encontrado, no existe el type: " + this.nombre_tipo);
                Maestra.getInstancia.addError(error);
                throw new Exception("Identificador no encontrado");
            }
            return Copia(salida);
        }

        public Objeto Copia(Objeto entrada)
        {
            if (entrada.getTipo() == Objeto.TipoObjeto.ARRAY)
            {
                

                return entrada.Clonar_Objeto();
            }
            else
            {
                
                return entrada.Clonar_Objeto();
            }
        }

    }
}
