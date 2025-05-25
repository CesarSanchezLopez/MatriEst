import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  menuOpen = false;
  estudianteNombre: string | null = null;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.cargarEstudiante();

    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.cargarEstudiante();
      }
    });
  }

  cargarEstudiante() {
    this.estudianteNombre = localStorage.getItem('estudianteNombre');
  }

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  closeMenu() {
    this.menuOpen = false;
  }

  logout() {
    localStorage.removeItem('estudianteId');
    localStorage.removeItem('estudianteNombre');
    this.cargarEstudiante();
    this.router.navigate(['/login']);
  }
}
