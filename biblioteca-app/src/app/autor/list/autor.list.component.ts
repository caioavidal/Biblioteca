import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AutorService } from '../autor.service';
import { ToastrService } from 'ngx-toastr';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-autor.list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './autor.list.component.html',
  styleUrl: './autor.list.component.css'
})
export class AutorListComponent {
  autores: any = [];
  constructor(private autorService: AutorService, private toastr: ToastrService) {
    this.getAllAutores();
  }
  
  getAllAutores(): void {
    this.autorService.getAllAutores().subscribe(
      (data) => {
        this.autores = data;
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao obter a lista');
      }
    );
  }

  delete(id: number) {
    let result = window.confirm("Tem certeza que deseja deletar o autor?");
    if (result == false) return;

    this.autorService.deleteAutor(id).subscribe(
      (data) => {
        this.toastr.error('Deletado com sucesso');
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao deletar');
      }
    );;
  }
}
