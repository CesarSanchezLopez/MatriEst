import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Materia } from '../models/materia.model';
import { Estudiante } from '../models/estudiante.model';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  //private apiUrl = 'https://localhost:7249/api'; // Cambia si tu backend está en otro puerto
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getMaterias(): Observable<Materia[]> {
    return this.http.get<Materia[]>(`${this.apiUrl}/materias`);
  }

  getCompañeros(materiaId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/materias/${materiaId}/compañeros`);
  }

  inscribirMaterias(estudianteId: number, materiaIds: number[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/materias/inscribir?estudianteId=${estudianteId}`, materiaIds);
  }

  getEstudiante(id: number): Observable<Estudiante> {
    return this.http.get<Estudiante>(`${this.apiUrl}/estudiantes/${id}`);
  }

  getEstudiantes(): Observable<Estudiante[]> {
    return this.http.get<Estudiante[]>(`${this.apiUrl}/estudiantes`);
  }
}