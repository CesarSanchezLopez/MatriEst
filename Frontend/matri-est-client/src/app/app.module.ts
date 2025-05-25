import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MateriasComponent } from './components/materias/materias.component';
import { HttpClientModule } from '@angular/common/http';
import { InscripcionComponent } from './components/inscripcion/inscripcion.component';
import { EstudiantesComponent } from './components/estudiantes/estudiantes.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    MateriasComponent,
    InscripcionComponent,
    EstudiantesComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
