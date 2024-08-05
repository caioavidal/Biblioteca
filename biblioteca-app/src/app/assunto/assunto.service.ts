import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AssuntoService {
  private apiUrl = `${environment.apiUrl}/api/assuntos`; // Replace with your actual API endpoint

  constructor(private http: HttpClient) {}

    // Get all assuntos
    getAllAssuntos(): Observable<any> {
      return this.http.get(this.apiUrl);
    }
  
    // Get assunto by ID
    getAssuntoById(id: number): Observable<any> {
      return this.http.get(`${this.apiUrl}/${id}`);
    }
  
    // Create a new assunto
    createAssunto(assuntoData: any): Observable<any> {
      return this.http.post(this.apiUrl, assuntoData);
    }
  
    // Update assunto by ID
    updateAssunto(id: number, assuntoData: any): Observable<any> {
      return this.http.put(`${this.apiUrl}/${id}`, assuntoData);
    }
  
    // Delete assunto by ID
    deleteAssunto(id: number): Observable<any> {
      return this.http.delete(`${this.apiUrl}/${id}`);
    }
}
