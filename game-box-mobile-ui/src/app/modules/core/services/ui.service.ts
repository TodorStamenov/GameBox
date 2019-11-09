import { Injectable } from '@angular/core';

import { Subject, BehaviorSubject } from 'rxjs';

import { Toasty, ToastDuration, ToastPosition } from 'nativescript-toasty';

@Injectable({
  providedIn: 'root'
})
export class UIService {
  private drawerState$$ = new Subject<void>();
  private drawerGestures$$ = new BehaviorSubject<boolean>(true);

  public drawerState$ = this.drawerState$$.asObservable();
  public drawerGestures$ = this.drawerGestures$$.asObservable();

  public toggleDrawer(): void {
    this.drawerState$$.next();
  }

  public toggleDrawerGestures(state: boolean): void {
    this.drawerGestures$$.next(state);
  }

  public showMessage(message: string): void {
    new Toasty({
      text: message,
      duration: ToastDuration.LONG,
      position: ToastPosition.CENTER
    }).show();
  }
}
