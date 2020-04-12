import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  template: `
    <!-- <footer class="bg-dark text-white p-3 text-right font-weight-bold">
      © {{year}} Copyright
    </footer> -->
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
