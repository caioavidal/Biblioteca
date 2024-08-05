import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AssuntoService } from '../assunto.service';
import { RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-assunto.list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './assunto.list.component.html',
  styleUrl: './assunto.list.component.css'
})
export class AssuntoListComponent {

  assuntos: any = [];
  constructor(private assuntoService: AssuntoService, private toastr: ToastrService) {
    this.getAllAssuntos();
  }

  getAllAssuntos(): void {
    this.assuntoService.getAllAssuntos().subscribe(
      (data) => {
        this.assuntos = data;
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao obter a lista');
      }
    );
  }

  delete(id: number) {
    let result = window.confirm("Tem certeza que deseja deletar o assunto?");
    if (result == false) return;

    this.assuntoService.deleteAssunto(id).subscribe(
      (data) => {
        this.toastr.error('Deletado com sucesso');
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao deletar');
      }
    );;
  }
}
