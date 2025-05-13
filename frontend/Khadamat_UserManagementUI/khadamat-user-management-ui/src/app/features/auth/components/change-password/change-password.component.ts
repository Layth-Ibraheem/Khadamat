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
import {MatCard} from '@angular/material/card';
import {MatInput, MatInputModule, MatLabel, MatPrefix, MatSuffix} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {ChangePasswordRequest} from '../../models/ChangePasswordRequest';
import {Router} from '@angular/router';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatTooltipModule} from '@angular/material/tooltip';
import {ThemePalette} from '@angular/material/core';

@Component({
  selector: 'app-change-password',
  imports: [
    MatCard,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    MatButtonModule,
    MatInput,
    MatLabel,
    MatPrefix,
    MatSuffix,
    MatProgressBarModule,
    MatTooltipModule,
  ],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss'
})
export class ChangePasswordComponent {
  public changePasswordForm: FormGroup = new FormGroup({});
  private authService = inject(AuthService);
  private router = inject(Router);
  hidePassword = true;
  hideNewPassword = true;
  hideNewPasswordConfirm = true;
  private changePasswordRequest: ChangePasswordRequest = new ChangePasswordRequest('', '');

  constructor(private fb: FormBuilder) {
    this.changePasswordForm = this.fb.group({
      password: ['', Validators.required],
      newPassword: ['', Validators.required],
      newPasswordConfirm: ['', Validators.required],
    }, {validators: passwordMatchValidator('newPassword', 'newPasswordConfirm')});
  }

  get password() {
    return this.changePasswordForm.get('password');
  }

  get newPassword() {
    return this.changePasswordForm.get('newPassword');
  }

  get newPasswordConfirm() {
    return this.changePasswordForm.get('newPasswordConfirm');
  }

  changePassword() {
    this.changePasswordRequest.Password = this.password?.value as string;
    this.changePasswordRequest.NewPassword = this.newPassword?.value as string;
    this.authService.changePassword(this.changePasswordRequest).subscribe({
      next: (res): any => {
        console.log(res.message);
        console.log("Please login again")
        this.authService.logout();
      },
      error: error => {
        console.error(error);
      }
    })
  }

  getPasswordStrength(): number {
    const password = this.newPassword?.value;
    if (!password) return 0;

    let strength = 0;
    if (password.length >= 8) strength += 25;
    if (/[A-Z]/.test(password)) strength += 25;
    if (/[0-9]/.test(password)) strength += 25;
    if (/[^A-Za-z0-9]/.test(password)) strength += 25;

    return strength;
  }

  getPasswordStrengthColor(): ThemePalette {
    const strength = this.getPasswordStrength();
    if (strength < 50) return 'warn';
    if (strength < 75) return 'accent';
    return 'primary';
  }

  getPasswordStrengthLabel(): string {
    const strength = this.getPasswordStrength();
    if (strength < 25) return 'Very Weak';
    if (strength < 50) return 'Weak';
    if (strength < 75) return 'Good';
    if (strength < 100) return 'Strong';
    return 'Excellent';
  }

}

export function passwordMatchValidator(controlName: string, matchingControlName: string): ValidatorFn {
  return (formGroup: AbstractControl): ValidationErrors | null => {
    const control = formGroup.get(controlName);
    const matchingControl = formGroup.get(matchingControlName);
    if (!control || !matchingControl) {
      return null;
    }
    // Only validate if both fields have values
    if (matchingControl.errors && !matchingControl.errors['passwordMismatch']) {
      return null;
    }

    // Set error if mismatch
    if (control.value !== matchingControl.value) {
      matchingControl.setErrors({passwordMismatch: true});
    } else {
      matchingControl.setErrors(null);
    }
    return null;
  };
}
