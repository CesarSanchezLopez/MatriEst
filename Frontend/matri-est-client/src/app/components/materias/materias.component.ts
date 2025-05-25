import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Materia } from '../../models/materia.model';

@Component({
  selector: 'app-materias',
  templateUrl: './materias.component.html',
  styleUrls: ['./materias.component.css']
})
export class MateriasComponent implements OnInit {
  materias: Materia[] = [];
  loading = true;

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.apiService.getMaterias().subscribe({
      next: (data) => {
        this.materias = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error al obtener materias:', err);
        this.loading = false;
      }
    });
  }
}