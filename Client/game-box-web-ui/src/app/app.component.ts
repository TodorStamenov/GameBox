import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <app-navigation></app-navigation>
    <div class="container-fluid" style="margin-top: 70px; margin-bottom: 60px">
      <app-toast></app-toast>
      <router-outlet></router-outlet>
    </div>
    <app-footer></app-footer>
  `
})
export class AppComponent { }
