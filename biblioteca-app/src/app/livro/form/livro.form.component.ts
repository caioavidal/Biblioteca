import { Component } from '@angular/core';
import { LivroService } from '../livro.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { Observable } from 'rxjs';
import { AutorService } from '../../autor/autor.service';
import { AssuntoService } from '../../assunto/assunto.service';

@Component({
  selector: 'app-livro.form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './livro.form.component.html',
  styleUrl: './livro.form.component.css'
})
export class LivroFormComponent {
  livro: any = { precos: [{ formaCompra: 1, preco: 0 }, { formaCompra: 2, preco: 0 }, { formaCompra: 3, preco: 0 }] };
  autores: Array<any> = [];
  assuntos: Array<any> = [];
  livroId: number = 0;

  constructor(private livroService: LivroService, private autorService: AutorService, private assuntoService: AssuntoService, private route: ActivatedRoute,
    private toastr: ToastrService, private router: Router) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let livroId = params['id'];
      if (livroId == null) {

        this.getAllAssuntos();
        this.getAllAutores();
        return;
      }
      this.livroId = livroId;
      this.getById();
    });
  }

  getAllAutores(): void {
    this.autorService.getAllAutores().subscribe(
      (data: Array<any>) => {
        if (this.livro.autores != null) {
          data.forEach(a => a.checked = (this.livro.autores as Array<any>).some(au => au.id == a.id));
        }
        this.autores = data;
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao obter a lista de autores');
      }
    );
  }

  getAllAssuntos(): void {
    this.assuntoService.getAllAssuntos().subscribe(
      (data: Array<any>) => {
        if (this.livro.assuntos != null) {
          data.forEach(a => a.checked = (this.livro.assuntos as Array<any>).some(au => au.id == a.id));
        }
        this.assuntos = data;
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao obter a lista de assuntos');
      }
    );
  }

  getById(): void {
    this.livroService.getLivroById(this.livroId).subscribe(
      (data) => {
        this.livro = data;

        this.getAllAutores();
        this.getAllAssuntos();
      },
      (error) => {
      }
    );
  }

  submit(): void {
    if (this.livro.titulo == null || this.livro.titulo == '') {
      this.toastr.error('Campo título é obrigatório');
      return;
    }

    let autores = this.autores.filter((autor) => autor.checked);
    this.livro.autores = autores;

    let assuntos = this.assuntos.filter((assunto) => assunto.checked);
    this.livro.assuntos = assuntos;

    if (this.livroId == 0) {
      this.handleResponse(this.livroService.createLivro(this.livro));
      return;
    }

    this.handleResponse(this.livroService.updateLivro(this.livroId, this.livro));
  }

  handleResponse(request: Observable<any>): any {
    request.subscribe(
      (data) => {
        this.toastr.success('Livro salvo com sucesso!');
        window.setTimeout(() => this.router.navigate(['/livros']), 1000);
      },
      (error) => {
        this.toastr.error('Ocorreu um erro ao salvar');
      }
    );
  }


  // transformPrice(element) {
  //   this.livro.precos = this.currencyPipe.transform(this.formattedAmount, '$');

  //   element.target.value = this.formattedAmount;
  // }
}
