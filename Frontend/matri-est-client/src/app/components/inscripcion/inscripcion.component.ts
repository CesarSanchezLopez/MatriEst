import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Materia } from '../../models/materia.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inscripcion',
  templateUrl: './inscripcion.component.html',
  styleUrls: ['./inscripcion.component.css']
})
export class InscripcionComponent implements OnInit {
  materias: Materia[] = [];
  materiasInscritas: Materia[] = [];
  seleccionadas: Materia[] = [];
  companerosPorMateria: { [materiaId: number]: string[] } = {};
  estudianteId: number = 0;
  mensaje: string = '';
  error: string = '';

  constructor(private apiService: ApiService, private router: Router) {}

  ngOnInit(): void {
    const id = localStorage.getItem('estudianteId');
    if (!id) {
      this.router.navigate(['/login']);
      return;
    }

    this.estudianteId = parseInt(id, 10);

    this.apiService.getMaterias().subscribe({
      next: (data) => this.materias = data,
      error: (err) => console.error(err)
    });

    this.cargarMateriasInscritas();
  }

  cargarMateriasInscritas(): void {
    this.apiService.getEstudiante(this.estudianteId).subscribe({
      next: (data) => {
      this.materiasInscritas = [...data.materias];
      },
      error: (err) => console.error('Error al cargar materias del estudiante', err)
    });
  }

  toggleSeleccion(materia: Materia): void {
    const yaSeleccionada = this.seleccionadas.find(m => m.id === materia.id);

    if (yaSeleccionada) {
      this.seleccionadas = this.seleccionadas.filter(m => m.id !== materia.id);
    } else {
      if (this.seleccionadas.length >= 3) {
        this.error = 'Solo puedes elegir 3 materias.';
        return;
      }

      const mismoProfesor = this.seleccionadas.some(m => m.profesorNombre === materia.profesorNombre);
      if (mismoProfesor) {
        this.error = 'No puedes elegir materias con el mismo profesor.';
        return;
      }

      this.seleccionadas.push(materia);
      this.error = '';
    }
  }

  enviar(): void {
    if (this.seleccionadas.length !== 3) {
      this.error = 'Debes seleccionar exactamente 3 materias.';
      return;
    }

    const materiaIds = this.seleccionadas.map(m => m.id);
    this.apiService.inscribirMaterias(this.estudianteId, materiaIds).subscribe({
      next: () => {
        this.mensaje = '¡Inscripción exitosa!';
        this.error = '';

       
        this.seleccionadas = [];
        this.companerosPorMateria = {};
        this.cargarMateriasInscritas();

       
        window.scrollTo({ top: 0, behavior: 'smooth' });
      },
      error: (err) => {
        this.error = 'Error al inscribir: ' + (err.error?.message || err.message || 'Error desconocido');
      }
    });
  }

  verCompaneros(materiaId: number): void {
    this.apiService.getCompañeros(materiaId).subscribe({
      next: (data) => {
        this.companerosPorMateria[materiaId] = data.map(c => c.nombre);
      },
      error: (err) => console.error('Error al cargar compañeros:', err)
    });
  }
  eliminarInscripcion(materiaId: number): void {
 // if (!confirm('¿Estás seguro de eliminar esta inscripción?')) return;

  this.apiService.eliminarInscripcion(this.estudianteId, materiaId).subscribe({
  next: (mensaje) => {
    this.mensaje = mensaje; // Recibe el texto del backend
    this.error = '';

    // Limpiar compañeros de esa materia (si se estaban mostrando)
    delete this.companerosPorMateria[materiaId];

    // Recargar materias inscritas actualizadas
    this.cargarMateriasInscritas();

    // Refrescar el componente sin recargar toda la página
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/inscripcion']);
    });
  },
  error: (err) => {
    this.error = 'Error al eliminar inscripción: ' + (err.error?.message || err.message || 'Error desconocido');
  }
});

}
}