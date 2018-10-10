import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterModel } from './models/register.model';
import { LoginModel } from './models/login.model';

const loginUrl = 'http://localhost:5000/auth/login';
const registerUrl = 'http://localhost:5000/auth/signup';

@Injectable()
export class AuthService {
  constructor(private http: HttpClient) { }

  get user() {
    return JSON.parse(localStorage.getItem('currentUser'));
  }

  register(body: RegisterModel) {
    return this.http.post(registerUrl, body);
  }

  login(body: LoginModel) {
    return this.http.post(loginUrl, body);
  }

  logout(): void {
    localStorage.clear();
  }

  isAuthenticated(): boolean {
    return localStorage.getItem('currentUser') !== null;
  }

  isAdmin(): boolean {
    if (this.user) {
      return this.user.isAdmin;
    }

    return false;
  }
}