import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AutorService } from '../autor.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-autor.form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './autor.form.component.html',
  styleUrl: './autor.form.component.css'
})
export class AutorFormComponent {
  autor: any = {};
  autorId: number = 0;

  constructor(private autorService: AutorService, private route: ActivatedRoute,
    private toastr: ToastrService, private router: Router) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let assuntoId = params['id'];
      if (assuntoId == null) return;
      this.autorId = assuntoId;
      this.getById();
    });
  }

  getById(): void {
    this.autorService.getAutorById(this.autorId).subscribe(
      (data) => {
        this.autor = data;
      },
      (error) => {
      }
    );
  }

  submit(): void {
    if (this.autor.nome == null || this.autor.descricao == '') {
      this.toastr.error('Campo nome é obrigatório');
      return;
    }

    if (this.autorId == 0) {
      this.handleResponse(this.autorService.createAutor(this.autor));
      return;
    }

    this.handleResponse(this.autorService.updateAutor(this.autorId, this.autor));
  }

  handleResponse(request: Observable<any>): any {
    request.subscribe(
      (data) => {
        this.toastr.success('Autor salvo com sucesso!');
        window.setTimeout(() => this.router.navigate(['/autores']), 1000);
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao salvar');
      }
    );
  }
}
