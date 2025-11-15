import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoriaModel } from './models/categoria.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CategoriaService {

  private apiUrl = 'https://localhost:7160/Categoria';

  private http = inject(HttpClient);

  obterTodasPorUsuario(): Observable<CategoriaModel[]>
  {
    return this.http.get<CategoriaModel[]>(this.apiUrl + "?UsuarioId=7716f22a-022f-4fb7-8563-c2daff42dfff");
  }
}
