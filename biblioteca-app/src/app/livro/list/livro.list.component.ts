import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { LivroService } from '../livro.service';
import { ToastrService } from 'ngx-toastr';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-livro.list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './livro.list.component.html',
  styleUrl: './livro.list.component.css'
})
export class LivroListComponent {
  livros: any;
  constructor(private livroService: LivroService, private toastr: ToastrService) {
    this.getAllLivros();
  }

  getAllLivros(): void {
    this.livroService.getAllLivros().subscribe(
      (data) => {
        console.log('All livros:', data);
        this.livros = data;
      },
      (error) => {
        console.error('Error fetching livros:', error);
      }
    );
  }

  delete(id: number) {
    let result = window.confirm("Tem certeza que deseja deletar o livro?");
    if (result == false) return;

    this.livroService.deleteLivro(id).subscribe(
      (data) => {
        this.toastr.error('Deletado com sucesso');
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao deletar');
      }
    );;
  }
}
