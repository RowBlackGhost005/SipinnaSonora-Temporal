using System.Collections;
using System.ComponentModel;
using System.Numerics;
using Excel_Parser_Test;
using ExcelDataReader;

//Requiered to work with ReaderClass
// Required in a MultiPart JSON/POST while in API.
// Then convert to FileStream
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
String filepath = "apiSipinna/Assests/Test Formato.xlsx";
FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
///

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