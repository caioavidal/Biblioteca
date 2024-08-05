import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { LivroService } from '../livro.service';
import { ToastrService } from 'ngx-toastr';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-livro.report',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './livro.report.component.html',
  styleUrl: './livro.report.component.css'
})
export class LivroReportComponent {
  autores: Array<any> = [];
  constructor(private livroService: LivroService, private toastr: ToastrService) {
    this.getReport();
  }

  getReport(): void {
    this.livroService.getReport().subscribe(
      (data) => {
        this.autores = data;
      },
      (error) => {
      }
    );
  }
}
