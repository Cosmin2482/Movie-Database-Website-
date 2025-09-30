import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class MovieService {
  constructor(private http: HttpClient) {}
  getPaged(page:number, pageSize:number, sort:string, dir:string){
    return this.http.get<any>(`/api/movies?page=${page}&pageSize=${pageSize}&sort=${sort}&dir=${dir}`);
  }
  search(q?:string, genre?:string, sort='title', dir='asc', yearMin?:number, yearMax?:number){
    const p = new URLSearchParams();
    if(q) p.set('q', q);
    if(genre) p.set('genre', genre);
    if(yearMin) p.set('yearMin', String(yearMin));
    if(yearMax) p.set('yearMax', String(yearMax));
    p.set('sort', sort); p.set('dir', dir);
    return this.http.get<any>('/api/movies/search?' + p.toString());
  }
  getById(id:number){ return this.http.get<any>(`/api/movies/${id}`); }
  addReview(id:number, text:string, stars:number){
    return this.http.post(`/api/movies/${id}/reviews`, { text, stars });
  }
}
