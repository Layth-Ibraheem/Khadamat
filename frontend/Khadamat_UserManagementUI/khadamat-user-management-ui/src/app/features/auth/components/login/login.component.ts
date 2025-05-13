import {Component, inject} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {LoginRequest} from '../../models/LoginRequest';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatDivider} from '@angular/material/divider';
import {Router, RouterLink} from '@angular/router';
import {MatTooltipModule} from '@angular/material/tooltip';

@Component({
  selector: 'app-login',
  imports: [
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatDivider,
    RouterLink,
    MatTooltipModule,

  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  standalone: true
})
export class LoginComponent {
  private authService = inject(AuthService);
  private router = inject(Router);
  public loginForm: FormGroup = new FormGroup({});
  private loginRequest: LoginRequest = new LoginRequest('', '');
  public hidePassword: boolean = true;

  constructor(private fb: FormBuilder) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  login() {
    if (this.loginForm.valid) {
      this.loginRequest.UserName = this.username?.value as string;
      this.loginRequest.Password = this.password?.value as string;
      this.authService.login(this.loginRequest).subscribe({
        next: authResponse => {
          this.router.navigate(['/home']);

        }, error: error => {
          console.error(error);
        }
      })
    }

  }

  get username() {
    return this.loginForm.get('username');
  }

  get password() {
    return this.loginForm.get('password');
  }
}
