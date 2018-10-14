import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../modules/authentication/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent {
  constructor(
    private router: Router,
    private authService: AuthService
    ) { }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}