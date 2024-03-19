using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiSipinna.Modules.XlsParser;
using apiSipinna.Models;

namespace apiSipinna.Services.ArchivosEstadisticos;

public class ArchivosEstadisticos
{
    private readonly EstadisticaDAO estadisticaDAO;

    public ArchivosEstadisticos(EstadisticaDAO dao)
    {
            estadisticaDAO = dao;
    }
        
    public async Task<string> GuardarEstadisticas(XlsParser xls, string dominio, string categoria, string indicador, string anio)
    {
        List<Data> xlsData = xls.GetDatosEstadisticos();
        List<Estadistica> datosEstadisticos = new List<Estadistica>();

        foreach(Data data in xlsData)
        {
            datosEstadisticos.Add(
                new Estadistica(
                    0,
                    new Cobertura(0, xls.GetAlcanceEstadistica(), data.poblacion),
                    new Categoria(0, dominio, categoria, indicador),
                    new Edades(0, data.edades),
                    new Lugar(0, data.entidad),
                    new Fecha(0, xls.GetFechaEstadistica(), new DateTime().Month.ToString()),
                    data.dato)
            );
        }


        await estadisticaDAO.guardarArreglo(datosEstadisticos);

        return "Datos guardados con éxito";
    }
}