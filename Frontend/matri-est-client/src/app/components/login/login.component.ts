import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Estudiante } from '../../models/estudiante.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  estudiantes: Estudiante[] = [];
  estudianteId: number | null = null;
  error: string = '';

  constructor(private apiService: ApiService, private router: Router) {}

  ngOnInit(): void {
    this.apiService.getEstudiantes().subscribe({
      next: (data) => this.estudiantes = data,
      error: () => this.error = 'Error al cargar estudiantes'
    });
  }

  entrar(): void {
    if (!this.estudianteId) {
      this.error = 'Debes seleccionar un estudiante';
      return;
    }

    const estudiante = this.estudiantes.find(e => e.id === this.estudianteId);
    if (!estudiante) {
      this.error = 'Estudiante inválido';
      return;
    }

    localStorage.setItem('estudianteId', estudiante.id.toString());
    localStorage.setItem('estudianteNombre', estudiante.nombre);

    this.router.navigate(['/inscripcion']);
  }
}