import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthHelperService {
  get user() {
    return JSON.parse(localStorage.getItem('currentUser'));
  }

  public isAuthenticated(): boolean {
    return localStorage.getItem('currentUser') !== null;
  }

  public isAdmin(): boolean {
    return this.user ? this.user.isAdmin : false;
  }
}
