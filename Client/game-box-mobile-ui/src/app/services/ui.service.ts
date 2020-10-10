import { Injectable } from '@angular/core';
import { RouterExtensions } from '@nativescript/angular';

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

  constructor(private router: RouterExtensions) { }

  public toggleDrawer(): void {
    this.drawerState$$.next();
  }

  public toggleDrawerGestures(state: boolean): void {
    this.drawerGestures$$.next(state);
  }

  public changeThumbnailUrls(thumbnailUrl: string, videoId: string): string {
    return thumbnailUrl || `https://i.ytimg.com/vi/${videoId}/maxresdefault.jpg`;
  }

  public navigate(path: string, params: any[] = [], clearHistory = false): void {
    this.router.navigate([path, ...params], {
      clearHistory: clearHistory,
      transition: { name: 'slideLeft' }
    });
  }

  public showMessage(message: string): void {
    new Toasty({
      text: message,
      duration: ToastDuration.LONG,
      position: ToastPosition.CENTER
    }).show();
  }
}
