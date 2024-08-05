import { Routes } from '@angular/router';
import {LivroListComponent} from './livro/list/livro.list.component';
import {AutorListComponent} from './autor/list/autor.list.component';
import {AssuntoListComponent} from './assunto/list/assunto.list.component';
import { AssuntoFormComponent } from './assunto/form/assunto.form.component';
import { AutorFormComponent } from './autor/form/autor.form.component';
import { LivroFormComponent } from './livro/form/livro.form.component';
import { LivroReportComponent } from './livro/report/livro.report.component';

export const routes: Routes = [
    { path: 'livros', component: LivroListComponent },
    { path: 'livros/novo', component: LivroFormComponent },
    { path: 'livros/edit/:id', component: LivroFormComponent },
    { path: 'report', component: LivroReportComponent },

    { path: 'autores', component: AutorListComponent },
    { path: 'autores/novo', component: AutorFormComponent },
    { path: 'autores/edit/:id', component: AutorFormComponent },

    { path: 'assuntos', component: AssuntoListComponent },
    { path: 'assuntos/novo', component: AssuntoFormComponent },
    { path: 'assuntos/edit/:id', component: AssuntoFormComponent }
];
