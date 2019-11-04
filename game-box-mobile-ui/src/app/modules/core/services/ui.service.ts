import { Injectable } from '@angular/core';

import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UIService {
  private drawerState$$ = new Subject<void>();
  public drawerState$ = this.drawerState$$.asObservable();

  public toggleDrawer(): void {
    this.drawerState$$.next();
  }
}
