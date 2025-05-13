import {Component, inject} from '@angular/core';
import {MatMenuModule} from '@angular/material/menu';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatListModule} from '@angular/material/list';
import {Router, RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';
import {MatCardModule} from '@angular/material/card';
import {AuthService} from '../../../features/auth/services/auth.service';
import {NgbCollapseModule, NgbDropdownModule} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  imports: [MatMenuModule, MatIconModule, MatButtonModule, MatListModule, RouterLink, RouterLinkActive, RouterOutlet, MatCardModule, NgbDropdownModule, NgbCollapseModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  username: string | undefined = 'admin';
  private router = inject(Router);
  private authService = inject(AuthService);
  sidebarCollapsed = false;

  toggleSidebar() {
    this.sidebarCollapsed = !this.sidebarCollapsed;
  }

  get isDashboardRoute(): boolean {
    return this.router.url === '/dashboard';
  }

  ngOnInit(): void {
    this.username = this.authService.currentAuthUserValue?.userName;

  }

  logout() {
    this.authService.logout();
  }

  goToChangePassword() {
    this.router.navigate(['/home/changePassword']);
  }
}
