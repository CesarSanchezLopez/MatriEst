import { Materia } from "./materia.model";

export interface Estudiante {
  id: number;
  nombre: string;
  materias: Materia[];
}