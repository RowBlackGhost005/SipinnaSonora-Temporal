using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiSipinna.Modules.XlsParser
{
    public class Data
    {

        public String entidad {get; set;}
        public String edades {get; set;}        
        public String poblacion {get; set;}
        public float dato {get; set;}

        public Data(String entidad, String edades, String poblacion, float dato){
            this.entidad = entidad;
            this.edades = edades;
            this.poblacion = poblacion;
            this.dato = dato;
        }

        public override String ToString(){    
            //return entidad + " " + poblacion + " " + edades + " " + dato;
            return $"{entidad,-32} | {poblacion, -8} | {edades, -4} | {dato, -5:F2} |";
        }
    }
}