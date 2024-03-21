import { Component, ElementRef, ViewChild, asNativeElements, inject, viewChild } from '@angular/core';
import { Chart } from 'chart.js/auto';
import { ApiService } from '../../../services/api.service';
import { ITable, Poblacion, RangoEdades } from '../../models/table.model';
import ChartDataLabels from 'chartjs-plugin-datalabels';


@Component({
  selector: 'app-bar-chart',
  standalone: true,
  imports: [],
  templateUrl: './bar-chart.component.html',
  styleUrl: './bar-chart.component.scss'
})
export class BarChartComponent {

  //Referencias para agregarle el scroll al chart
  @ViewChild('subcontainer') chartSubcontainer!: ElementRef;
  @ViewChild('container') chartContainer!: ElementRef;

  private _apiService = inject(ApiService);
  public chart!: Chart;


  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.

    this._apiService.getEstadistica().subscribe((dataFile: ITable[]) => {


      console.log('DATOS DESDE EL COMPONENTE DE LA TABLA')
      console.log(dataFile[0])

      ///NOTA: Cuando hayan múltiples rangos de edades, el filter va a tener la referencia del valor seleccionado del combobox

      const filteredValues = dataFile.filter(function(entidad){
        //Cambiar esta comparacion por los demas rangos de edades cuando se ocupen más
        return entidad.edadesNav.rangoEdades == RangoEdades.The57
      })


      //NumericValue tiene almacenado todos los datos numéricos de la tabla de excel que pueden ser usados
      //para darle los valores a las gráficas
      const numericValues = filteredValues.map(function(object){
        return object.dato
      })

      //Devuelve la entidad del dato
      const RawlabelValues = filteredValues.map(function(object){
        return object.lugarNav.entidad
      })

      const labelValues:string[] = RawlabelValues.filter((item,index)=>{
        return RawlabelValues.indexOf(item) === index;
      })

      //para saber si se trata de Total, hombres o mujeres
      const tipoPoblacion = filteredValues.map(function(object){
        return object.coberturaNav.poblacion
      })


      //Para sacar los datos de los hombres

      const totalM = filteredValues.filter(function(entidad){
        //Cambiar esta comparacion por los demas rangos de edades cuando se ocupen más (me refiero al RangoEdades.The57)
        return entidad.edadesNav.rangoEdades == RangoEdades.The57 && entidad.coberturaNav.poblacion == Poblacion.Hombre
      })

      const numericValuesM = totalM.map(function(object){
        return object.dato
      })

      //Para sacar los datos de las mujeres

      const totalF = filteredValues.filter(function(entidad){
        //Cambiar esta comparacion por los demas rangos de edades cuando se ocupen más (me refiero al RangoEdades.The57)
        return entidad.edadesNav.rangoEdades == RangoEdades.The57 && entidad.coberturaNav.poblacion == Poblacion.Mujer
      })

      const numericValuesF = totalF.map(function(object){
        return object.dato
      })      

      //Para sacar los datos del total

      const totalT = filteredValues.filter(function(entidad){
        //Cambiar esta comparacion por los demas rangos de edades cuando se ocupen más (me refiero al RangoEdades.The57)
        return entidad.edadesNav.rangoEdades == RangoEdades.The57 && entidad.coberturaNav.poblacion == Poblacion.Total
      })

      const numericValuesT = totalT.map(function(object){
        return object.dato
      })      
      


      console.log(numericValues)
      console.log(labelValues)
      console.log(tipoPoblacion)


      console.log("Ya con filtro")
      console.log(filteredValues)


      const data = {
        labels: labelValues,
        datasets:[
          {
            label:"Total",
            data: numericValuesT,
            fill: false,
            backgroundColor: '#8B8A8A',
            borderColor: '#8B8A8A',
            borderWidth: 2,
            tension: 0.1,
          },
          {
          label:"Hombres",
          data: numericValuesM,
          fill: false,
          backgroundColor: '#67C593',
          borderColor: '#67C593',
          borderWidth: 2,
          tension: 0.1,
        },
        {
          label:"Mujeres",
          data: numericValuesF,
          fill: false,
          backgroundColor: '#F499B7',
          borderColor: '#F499B7',
          borderWidth: 2,
          tension: 0.1,
        },
      ]
      };

      Chart.register(ChartDataLabels)
      this.chart = new Chart("chart", {
        type:"bar",
        data: data,
        options: {
          indexAxis: 'y',
          responsive: true,
          maintainAspectRatio:false,
          plugins:{
            title: {
              display:true,
              text:filteredValues[1].categoriaNav.indicador
            },
            datalabels: {
              display: true,
              anchor: 'end',
              align: 'end',
              font: {
                weight: 'bold'
                
              }
            },
            tooltip:{
              enabled:true,
              mode: 'index'
            }
          },
        }
      })
    })
  }
}
