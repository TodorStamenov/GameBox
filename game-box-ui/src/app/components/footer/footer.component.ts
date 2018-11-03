import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  template: `
    <footer style="padding-top: 20px; padding-bottom: 20px;" class="page-footer font-small blue">
      <div class="footer-copyright text-right">© {{year}} Copyright</div>
    </footer>`
})
export class FooterComponent {
  public year: number;

  constructor() {
    this.year = new Date().getFullYear();
  }
}