import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthHelperService {
  get user(): any {
    return JSON.parse(localStorage.getItem('currentUser'));
  }

  get isAuthed(): boolean {
    return localStorage.getItem('currentUser') !== null;
  }

  get isAdmin(): boolean {
    return this.user ? this.user.isAdmin : false;
  }
}
