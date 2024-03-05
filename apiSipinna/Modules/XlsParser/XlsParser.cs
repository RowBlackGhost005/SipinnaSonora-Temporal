using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Excel_Parser_Test;
using ExcelDataReader;
using System.Text.RegularExpressions;
using Aspose.Cells;

namespace apiSipinna.Modules.XlsParser;

/// <summary>
/// Class <c>XlsParser</c> reads a FileStream to extract its content.
/// </summary>
public class XlsParser
{

    private DataTable _xlsData;

    public XlsParser(FileStream xlsDataStream)
    {
        _xlsData = ExcelReaderFactory.CreateReader(xlsDataStream).AsDataSet().Tables[0];

        //Check if this is doable --!
        xlsDataStream.Close();
    }

    public String GetStatisticName()
    {
        //TODO : Create Format Excepction (?) IF data is NULL (data = DBNull.Value)
        return (String) _xlsData.Rows[0][0];
    }

    public int GetStatisticDate()
    {
        //TODO : Create Format Exception (?) IF data is NULL (data = DBNull.Value)
        return Int32.Parse(_xlsData.Rows[1][0].ToString() ?? "null");
    }

    public String GetStatisticScope()
    {
        //TODO : Create Format Exception (?) IF data is NULL (data = DBNull.Value)
        return (String) _xlsData.Rows[2][0];
    }

    public List<String> GetEntitiesNames()
    {
        List<String> entities = new List<String>();
        var rows = _xlsData.Rows;
        var columns = _xlsData.Columns;

        for(int i = 6; i < rows.Count ; i++){
    
            //DBNUll means empty cell, not equals to 'null'
            if(rows[i][0] == DBNull.Value){
                break;
            }

            //Get data
            //TODO: Check if null
            entities.Add(rows[i][0].ToString() ?? "null");
        }

        return entities;
    }

    public List<String> GetAgeRanges(){

        List<String> ageRanges = new List<String>();
        var rows = _xlsData.Rows;
        var columns = _xlsData.Columns;

        for(int i = 1; i < columns.Count ; i++){

            //DBNULL means value in table not initialized, not equals to 'null'.
            if(rows[4][i] == DBNull.Value){
                continue;
            }

            String ageRange = StripAgeRange((String) rows[4][i]);

            ageRanges.Add(ageRange);
        }

        return ageRanges;
    }

    public List<String> GetPopulations(){

        List<String> populations = new List<string>();
        var rows = _xlsData.Rows;
        var columns = _xlsData.Columns;

        for(int i = 1 ; i < columns.Count ; i++){

            if(rows[5][i] == DBNull.Value){
                continue;
            }

            String population = (String) rows[5][i];

            population = population.Trim();
            population = char.ToUpper(population[0]) + population.Substring(1);

            populations.Add(population);
        }

        return populations;

    }

    /// <summary>
    /// Returns an String representing an age range in the format XX-XX.
    /// </summary>
    /// <param name="ageRangeString">String with an age range</param>
    /// <returns>String representing age range</returns>
    private String StripAgeRange(String ageRangeString){
        //Pos 0 is the lesser and pos 1 is the greater age
        List<String> ageRange = new List<String>();
        string[] ageStrings = Regex.Split(ageRangeString, @"\D+");
        
        foreach (string age in ageStrings){
            if(!string.IsNullOrEmpty(age)){
                ageRange.Add(age);
            }
        }

        if(ageRange.Count < 2){
            //TODO: Exception, NOT an age range or new method for < 1 case
        }

        String ageRangeFormat = ageRange[0] + "-" + ageRange[1];
        return ageRangeFormat;
    }


    public List<Data> GetStatisticData()
    {
        var table = _xlsData.Rows;
        var columns = _xlsData.Columns;
        
        List<String> ageRanges = GetAgeRanges();
        List<String> populations = GetPopulations();
        List<String> entities = GetEntitiesNames();
        List<Data> statisticData = new List<Data>();

        int headerPerAgeRange = populations.Count / ageRanges.Count;
        
        for(int row = 6 , column = 1, columnsVisited = 0 , currentAgeRange = 0 ; ; row++){

            //Checks if reached end of table
            if(row == (entities.Count + 6)){
                row = 6;
                column++;
                columnsVisited++;

                //Checks if new ageRange is required
                if((columnsVisited % headerPerAgeRange) == 0){
                    currentAgeRange++;
                }
            }

            if(columnsVisited >= populations.Count){
                break;
            }

            String cellValue =  table[row][column].ToString() ?? "NA";
            float cellStatisticValue = 0;

            if(cellValue.Equals("NA" , StringComparison.OrdinalIgnoreCase)){
                cellStatisticValue = -1;
            }else{
                cellStatisticValue = float.Parse(cellValue);
            }

            Data data = new Data(entidad:entities[row-6], edades:ageRanges[currentAgeRange], poblacion:populations[columnsVisited], dato:cellStatisticValue);

            statisticData.Add(data);
        }

        return statisticData;
    }
}
