using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ExcelDataReader;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ExcelDataReader.Log;
using Microsoft.IdentityModel.Tokens;

namespace apiSipinna.Modules.XlsParser;

/// <summary>
/// Clase <c>XlsParser</c> utiliza un <c>Stream</c> de un archivo .xls/.xlsx para leer y extraer su contenido usando objetos <c>Data</c>.
/// Dentro de esta clase la información del archivo .xls/.xlsx solo existe en Memoria.
/// </summary>
public class XlsParser
{
    /// <summary> Compensación en filas para llegar al nombre de la estadística dentro del archivo excel. </summary>
    private readonly int nombreEstadisticaRowOffset = 0;
    /// <summary> Compensación en filas para llegar al año de la estadística dentro del archivo excel. </summary>
    private readonly int anioEstadisticaRowOffset = 1;
    /// <summary> Compensación en filas para llegar al alcance de la estadística dentro del archivo excel. </summary>
    private readonly int alcanceEstadisticaRowOffset = 2;
    /// <summary> Compensación en filas para llegar a las edades dentro del archivo excel. </summary>
    private readonly int edadesRowOffset = 4;
    /// <summary> Compensación en filas para llegar a las poblaciones dentro del excel. </summary>
    private readonly int poblacionRowOffset = 5;
    /// <summary> Compensación en filas para llegar a las entidades dentro del excel. </summary>
    private readonly int entidadesRowOffset = 6;
    /// <summary> Tabla de datos que almacena la hoja de cálculo de excel. </summary>
    private readonly DataTable _xlsData;

    /// <summary>
    /// Crea un objeto de tipo <c>XlsParser</c> utilizando el Stream dado como parámetro y carga en memoria el contenido de la primera
    /// página del archivo .xls para realizar las consultas.
    /// Una vez creado el objeto, el Stream no es requerido por esta clase.
    /// </summary>
    /// <param name="xlsDataStream">Stream de un archivo .xls/.xlsx</param>
    public XlsParser(Stream xlsDataStream)
    {
        try{
            _xlsData = ExcelReaderFactory.CreateReader(xlsDataStream).AsDataSet().Tables[0];
        }catch(Exception e){
            throw new XlsFormatException("Error al abrir el archivo .xls", e);
        }
        
    }

    /// <summary>
    /// Devuelve el nombre de la estadística que se encuentra dentro del documento.
    /// El nombre debe estar en la casilla [1,A]/[0,0], en caso contrario se lanzará una excepción.
    /// </summary>
    /// <returns>String del nombre de la estadística</returns>
    /// <exception cref="XlsFormatException">Excepción si no se encuentra el nombre en el documento.</exception>
    public String GetNombreEstadistica()
    {
        var nombreEstadistica = _xlsData.Rows[nombreEstadisticaRowOffset][0];

        if(nombreEstadistica == DBNull.Value){
            throw new XlsFormatException("No se encontró el nombre de la estadística");
        }
        
        return (String) nombreEstadistica;
    }

    /// <summary>
    /// Devuelve el año en el que se tomó la estadística que se encuentra dentro del documento.
    /// El año debe estar en la casilla [2,A]/[1,0], en caso contrario se lanzará una excepción.
    /// </summary>
    /// <returns>Int representando el año</returns>
    /// <exception cref="XlsFormatException">Excepción si no se encuentra el año en el documento.</exception>
    public int GetFechaEstadistica()
    {
        var anioEstadistica = _xlsData.Rows[anioEstadisticaRowOffset][0];

        if(anioEstadistica == DBNull.Value){
            throw new XlsFormatException("No se encontró el año de la estadística");
        }

        //TODO : Create Format Exception (?) IF data is NULL (data = DBNull.Value)
        return Int32.Parse(anioEstadistica.ToString() ?? "null");
    }

    /// <summary>
    /// Devuelve el alcance que tiene la estadística que se encuentra dentro del documento.
    /// El alcance debe estar en la casilla [3/A]/[2,0], en caso contrario se lanzará una excepción.
    /// </summary>
    /// <returns>String representando el alcance</returns>
    /// <exception cref="XlsFormatException">Excepción si no se encuentra el alcance en el documento.</exception>
    public String GetAlcanceEstadistica()
    {
        var alcanceEstadistica = _xlsData.Rows[alcanceEstadisticaRowOffset][0];

        if(alcanceEstadistica == DBNull.Value){
            throw new XlsFormatException("No se encontró el alcance de la estadística");
        }

        return (String) alcanceEstadistica;
    }

    /// <summary>
    /// Devuelve una lista con los nombres de las entidades que se encuentran dentro del documento.
    /// Los nombres deben estar a partir de la casilla [7,A]/[6,0] y se terminan de leer hasta encontrar una casilla en blanco.
    /// </summary>
    /// <returns>Lista de Strings con el nombre de las entidades</returns>
    public List<String> GetNombreEntidades()
    {
        List<String> entidades = new List<String>();
        var rows = _xlsData.Rows;

        for(int i = entidadesRowOffset; i < rows.Count ; i++){
    
            //DBNUll significa celda vacía.
            //Termina la lectura con un espacio vacío
            if(rows[i][0] == DBNull.Value){
                break;
            }

            entidades.Add(rows[i][0].ToString() ?? "null");
        }

        return entidades;
    }

    /// <summary>
    /// Devuelve una lista de Strings representando los rangos de edades que se encuentran dentro del documento.
    /// Los rangos de edades deben estar a partir de la casilla [5,B]/[4,1] y pueden o no abarcar múltiples celdas usando celdas combinadas.
    /// Estos valores se leerán hasta el fin de las columnas del documento.
    /// </summary>
    /// <returns>Lista de Strings con los rangos de edades encontrados en el documento.</returns>
    /// <exception cref="XlsFormatException">Excepción si no se puede obtener el rango de edad.</exception>
    public List<String> GetRangoEdades(){

        List<String> rangoEdades = new List<String>();
        var rows = _xlsData.Rows;
        var columns = _xlsData.Columns;

        for(int i = 1; i < columns.Count ; i++){

            //DBNUll significa celda vacía.
            //Solo la primera celda de la derecha contiene el valor en caso de celdas combinadas.
            if(rows[edadesRowOffset][i] == DBNull.Value){
                continue;
            }

            string rangoEdad;

            try{
                rangoEdad = FormatRangoEdades((String) rows[edadesRowOffset][i]);
            }catch(Exception e){
                throw new XlsFormatException("Error al convertir los rangos de edades" , e);
            }

            rangoEdades.Add(rangoEdad);
        }

        return rangoEdades;
    }

    /// <summary>
    /// Devuelve una lista de Strings representando las poblaciones que se encuentran dentro del documento.
    /// Las poblaciones deben estar a partir de la casilla [6,B]/[5,1] y se leerán las columnas hasta encontrar una vacía.
    /// </summary>
    /// <returns>Lista de Strings con el nombre de las poblaciones en orden de aparición en el documento.</returns>
    public List<String> GetPoblaciones(){

        List<String> poblaciones = new List<string>();
        var rows = _xlsData.Rows;
        var columns = _xlsData.Columns;

        for(int i = 1 ; i < columns.Count ; i++){

            if(rows[poblacionRowOffset][i] == DBNull.Value){
                continue;
            }

            String poblacion = (String) rows[poblacionRowOffset][i];

            poblacion = poblacion.Trim();
            poblacion = char.ToUpper(poblacion[0]) + poblacion.Substring(1);

            poblaciones.Add(poblacion);
        }

        return poblaciones;

    }

    /// <summary>
    /// Formatea un String representando un rango de edades en el formato: XX-XX
    /// </summary>
    /// <param name="rangoEdadString">String conteniendo dos edades separadas</param>
    /// <returns>String con formato representando un rango de edades</returns>
    /// <exception cref="FormatException">Excepción si no se encuentran edades dentro del String.</exception>
    private String FormatRangoEdades(string rangoEdadString){
        //Posicion 0 es la edad menor y la 1 es la mayor.
        List<string> rangoEdad = new List<string>();
        string[] stringEdades = Regex.Split(rangoEdadString, @"\D+");

        if(stringEdades.Length == 0){
            throw new FormatException("No se encontraron edades dentro del String");
        }
        
        foreach (string edad in stringEdades){
            if(!string.IsNullOrEmpty(edad)){
                rangoEdad.Add(edad);
            }
        }

        string rangoEdadFormat;

        if(rangoEdad.Count < 2){
            rangoEdadFormat = "<" + rangoEdad[0];
        }else{
            rangoEdadFormat = rangoEdad[0] + "-" + rangoEdad[1];
        }

        
        return rangoEdadFormat;
    }

    /// <summary>
    /// Obtiene todos los datos estadísticos almacenados dentro del documento, los relaciona con sus atributos y los almacena en objetos Data.
    /// Si dentro de los datos se encuentra un valor diferente de Float o "NA", el valor por defecto será -1 ("NA" en Base de Datos).
    /// </summary>
    /// <returns>Lista de objetos <c>Data</c> representando cada una de las celdas dentro del documento.</returns>
    public List<Data> GetDatosEstadisticos()
    {
        var tabla = _xlsData.Rows;
        
        List<String> rangoEdades = GetRangoEdades();
        List<String> poblacion = GetPoblaciones();
        List<String> entidades = GetNombreEntidades();
        List<Data> dataEstadistica = new List<Data>();

        int encabezadosPorRangoEdades = poblacion.Count / rangoEdades.Count;
        
        for(int row = entidadesRowOffset , columnaActual = 1, columnasVisitadas = 0 , currentAgeRange = 0 ; ; row++){

            //Revisa si se llegó al final de la tabla (Verticalmente).
            if(row == (entidades.Count + 6)){
                row = entidadesRowOffset;
                columnaActual++;
                columnasVisitadas++;

                //Revisa si se requiere un nuevo rango de edades
                if((columnasVisitadas % encabezadosPorRangoEdades) == 0){
                    currentAgeRange++;
                }
            }

            if(columnasVisitadas >= poblacion.Count){
                break;
            }

            float valorCelda; 
            
            try{
                valorCelda = CheckValorCelda(tabla[row][columnaActual].ToString());
            }
            catch(Exception e)
            {
                throw;
            }
            

            Data data = new Data(entidad:entidades[row-entidadesRowOffset], edades:rangoEdades[currentAgeRange], poblacion:poblacion[columnasVisitadas], dato:valorCelda);

            dataEstadistica.Add(data);
        }

        return dataEstadistica;
    }

    /// <summary>
    /// Devuelve el valor en Float del String representando el valor de una celda del documento.
    /// </summary>
    /// <param name="celda">String? representando el valor de una celda</param>
    /// <returns>Float del valor de la celda, -1 en caso de N/A</returns>
    /// <exception cref="FormatException">Excepción si se encontró un valor diferente a un decimal o "NA" dentro de la celda.</exception>
    private float CheckValorCelda(String? celda)
    {
        float valorCelda;

        if(celda == null || celda == "NA")
        {
            valorCelda = -1;
        }else{
            try{
                valorCelda = float.Parse(celda);
            }
            catch(Exception e){
                throw new XlsFormatException("Se encontró un valor diferente a un Decimal o \"NA\" dentro de los datos estadísticos.",e);
            }
            
        }

        return valorCelda;
    }
}
