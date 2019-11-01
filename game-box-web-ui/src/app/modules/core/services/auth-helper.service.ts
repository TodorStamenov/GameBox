import { Injectable } from '@angular/core';

import { IUser } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthHelperService {
  private currentUser: IUser;

  get user(): IUser {
    return this.currentUser || JSON.parse(localStorage.getItem('currentUser'));
  }

  set user(user: IUser) {
    this.currentUser = user;
    localStorage.setItem('currentUser', JSON.stringify(this.currentUser));
  }

  get isAuthed(): boolean {
    if (!this.user || !this.user.expirationDate) {
      return false;
    }

    return Date.parse(new Date().toString()) < Date.parse(this.user.expirationDate.toString());
  }

  get isAdmin(): boolean {
    return this.isAuthed && this.user.isAdmin;
  }

  public logout(): void {
    this.currentUser = null;
    localStorage.clear();
  }
}
