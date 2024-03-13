import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './core/components/header/header.component';
import { FooterComponent } from './core/components/footer/footer.component';
import { ApiService } from './services/api.service';
import { ITable } from './core/models/table.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,HeaderComponent,FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  private _apiService = inject(ApiService);

  ngOnInit(): void {
      this._apiService.getEstadistica().subscribe((data: ITable[]) => {
        console.log(data);
      })
  }
}
