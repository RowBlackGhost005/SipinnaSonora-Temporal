using apiSipinna.Modules.XlsParser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ExcelDataReader;
using System.Text.RegularExpressions;

public class XlsTest{

    public XlsTest(){
        String filepath = "apiSipinna/Assests/Test Formato.xlsx";
        FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
        XlsParser xlsParser = new XlsParser(stream);

        //Fetching data XLS Data

        /*
        Console.WriteLine(xlsParser.GetStatisticName());
        Console.WriteLine(xlsParser.GetStatisticDate());
        Console.WriteLine(xlsParser.GetStatisticScope());
        foreach (var item in xlsParser.GetEntitiesNames())
        {
            Console.WriteLine(item);
        }

        foreach (var item in xlsParser.GetAgeRanges())
        {
            Console.WriteLine(item);
        }

        foreach(var item in xlsParser.GetPopulations())
        {
            Console.WriteLine(item);
        }
        */

        foreach(var item in xlsParser.GetStatisticData())
        {
            Console.WriteLine(item);
        }
    }
}
