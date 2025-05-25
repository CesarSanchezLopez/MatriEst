import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MateriasComponent } from './components/materias/materias.component';
import { InscripcionComponent } from './components/inscripcion/inscripcion.component';
import { EstudiantesComponent } from './components/estudiantes/estudiantes.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'inscripcion', component: InscripcionComponent },
  { path: 'materias', component: MateriasComponent },
  { path: 'estudiantes', component: EstudiantesComponent },

  // Ruta por defecto
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  // Ruta comodín
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
