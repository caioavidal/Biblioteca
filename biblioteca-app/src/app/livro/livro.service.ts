import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LivroService {
  private apiUrl = `${environment.apiUrl}/api/livros`; // Replace with your actual API endpoint

  constructor(private http: HttpClient) { }

  // Get all livros
  getAllLivros(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  // Get livro by ID
  getLivroById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  // Create a new livro
  createLivro(livroData: any): Observable<any> {
    return this.http.post(this.apiUrl, livroData);
  }

  // Update livro by ID
  updateLivro(id: number, livroData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, livroData);
  }

  // Delete livro by ID
  deleteLivro(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  // Get all livros
  getReport(): Observable<any> {
    return this.http.get(`${this.apiUrl}/autores`);
  }
}
