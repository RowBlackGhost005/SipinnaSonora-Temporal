import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';

import {
  FormGroup,
  AbstractControl,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';


@Component({
  selector: 'app-upload',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './upload.component.html',
  styleUrl: './upload.component.scss'
})


export class UploadComponent {

  @Output()
  public buttonClick = new EventEmitter<boolean>();
  public form!: FormGroup;
  public errors: string[] = [];


  validateFileExtension(file: string | undefined) {
    if (file === 'xls') return true;
    else return false;
  }

  


  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {

      const fileExtension = file.name.split(".")
      console.log(fileExtension[1])

      if (fileExtension[1] === "xlsx") {
        console.log('Archivo seleccionado:', file);
        console.log(file.name);
        alert("si");
      } else {
        alert("no");
      }

    }
  }

}
