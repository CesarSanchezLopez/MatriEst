import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Estudiante } from '../../models/estudiante.model';

@Component({
  selector: 'app-estudiantes',
  templateUrl: './estudiantes.component.html',
  styleUrls: ['./estudiantes.component.css']
})
export class EstudiantesComponent implements OnInit {
  estudiantes: Estudiante[] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.apiService.getEstudiantes().subscribe({
      next: (data) => this.estudiantes = data,
      error: (err) => console.error('Error al cargar estudiantes:', err)
    });
  }
}