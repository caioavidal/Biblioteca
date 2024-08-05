import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AutorService {
  private apiUrl = `${environment.apiUrl}/api/autores`; // Replace with your actual API endpoint

  constructor(private http: HttpClient) {}

    // Get all autors
    getAllAutores(): Observable<any> {
      return this.http.get(this.apiUrl);
    }
  
    // Get autor by ID
    getAutorById(id: number): Observable<any> {
      return this.http.get(`${this.apiUrl}/${id}`);
    }
  
    // Create a new autor
    createAutor(autorData: any): Observable<any> {
      return this.http.post(this.apiUrl, autorData);
    }
  
    // Update autor by ID
    updateAutor(id: number, autorData: any): Observable<any> {
      return this.http.put(`${this.apiUrl}/${id}`, autorData);
    }
  
    // Delete autor by ID
    deleteAutor(id: number): Observable<any> {
      return this.http.delete(`${this.apiUrl}/${id}`);
    }
}
