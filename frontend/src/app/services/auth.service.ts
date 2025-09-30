import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class AuthService {
  token: string | null = localStorage.getItem('token');
  username: string | null = localStorage.getItem('username');

  constructor(private http: HttpClient) {}

  login(username:string, password:string){
    return this.http.post<any>('/api/auth/login', { username, password });
  }

  register(username:string, password:string){
    return this.http.post('/api/auth/register', { username, password });
  }

  setSession(username:string, token:string){
    this.username = username; this.token = token;
    localStorage.setItem('username', username);
    localStorage.setItem('token', token);
  }

  logout(){
    this.username = null; this.token = null;
    localStorage.removeItem('username'); localStorage.removeItem('token');
  }
}
