import { Component, inject } from '@angular/core';
import { ITable } from '../../models/table.model';
import { ApiService } from '../../../services/api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './table.component.html',
  styleUrl: './table.component.scss'
})
export class TableComponent {
  private _apiService = inject(ApiService);
  tableList: ITable[] = [];
  anios: number[] = [];
  anioSeleccionado?: number;
  indicadorSeleccionado?: String;

  ngOnInit(): void {
    this._apiService.getEstadistica().subscribe((data: ITable[]) => {
      this.tableList = data;
      this.consultarAnios();
      this.indicadorSeleccionado = data[0].categoriaNav.indicador;
      console.log(data);
    });
  }

  consultarAnios(){
    let list: number[] = [];
    for(let element of this.tableList){
      list.push(element.fechaNav.anio);
    }
    this.anios = Array.from(new Set(list));
    this.anioSeleccionado = this.anios[0];
  }

  cambiarAnio(anio: number) {
    this.anioSeleccionado = anio;
  }
}
