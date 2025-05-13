import {Component, inject} from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators
} from '@angular/forms';
import {AuthService} from '../../services/auth.service';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatDividerModule} from '@angular/material/divider';
import {Router, RouterLink} from '@angular/router';
import {passwordMatchValidator} from '../change-password/change-password.component';
import {RegisterRequest} from '../../models/RegisterRequest';
import {CreateNewRegisterApplicationRequest} from '../../../applications/models/CreateNewRegisterApplicationRequest';
import {ApplicationsService} from '../../../applications/services/applications.service';

@Component({
  selector: 'app-register',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatIconModule,
    MatCardModule,
    MatInputModule,
    MatTooltipModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDividerModule,
    RouterLink
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {

  private router = inject(Router);
  private applicationService = inject(ApplicationsService);
  private newApplicationRequest: CreateNewRegisterApplicationRequest = new CreateNewRegisterApplicationRequest('', '', '');
  hidePassword = true;
  hideConfirmPassword = true;

  registerForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(4)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required],
      terms: [false, Validators.requiredTrue]
    }, {validators: passwordMatchValidator('password', 'confirmPassword')})

  }

  get username() {
    return this.registerForm.get('username');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  get terms() {
    return this.registerForm.get('terms');
  }

  get confirmPassword() {
    return this.registerForm.get('confirmPassword');
  }

  register() {
    if (this.registerForm.valid) {
      // Handle registration logic here
      this.newApplicationRequest.Password = this.password?.value;
      this.newApplicationRequest.Email = this.email?.value;
      this.newApplicationRequest.Username = this.username?.value;
      this.applicationService.createNewRegisterApplication(this.newApplicationRequest).subscribe({
        next: authResponse => {
          console.log("please log in with your account");
          this.router.navigate(['/login']);
        }, error: error => {
          console.error(error);
        }
      })
    }
  }

}
