import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  template: `
    <footer class="navbar bg-dark fixed-bottom text-white font-weight-bold p-3">
      <div class="ml-auto">
        © {{year}} Copyright
      </div>
    </footer>
  `
})
export class FooterComponent {
  public year = new Date().getFullYear();
}
