import {Component, inject} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {Subscription, timer} from 'rxjs';
import {Router, RouterLink} from '@angular/router';
import {MatSnackBar} from '@angular/material/snack-bar';
import {passwordMatchValidator} from '../change-password/change-password.component';
import {MatIconModule} from '@angular/material/icon';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {DecimalPipe} from '@angular/common';
import {AuthService} from '../../services/auth.service';
import {RequestResetPasswordCodeRequest} from '../../models/RequestResetPasswordCodeRequest';
import {ResetPasswordViaCodeRequest} from '../../models/ResetPasswordViaCodeRequest';

@Component({
  selector: 'app-forget-password',
  imports: [
    ReactiveFormsModule,
    MatIconModule,
    MatProgressSpinnerModule,
    DecimalPipe,
    RouterLink
  ],
  templateUrl: './forget-password.component.html',
  styleUrl: './forget-password.component.scss'
})
export class ForgetPasswordComponent {
  emailForm: FormGroup;
  resetForm: FormGroup;
  codeSent = false;
  loading = false;
  hidePassword = true;
  hideConfirmPassword = true;
  codeExpired = false;
  minutes = 2;
  seconds = 0;
  private countdownSub: Subscription | undefined;
  private authService = inject(AuthService);
  private requestResetCode: RequestResetPasswordCodeRequest = new RequestResetPasswordCodeRequest('');
  private resetPasswordViaCode: ResetPasswordViaCodeRequest = new ResetPasswordViaCodeRequest('', '', '');

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.emailForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });

    this.resetForm = this.fb.group({
      code: ['', [Validators.required, Validators.minLength(6)]],
      newPassword: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required]
    }, {validators: passwordMatchValidator('newPassword', 'confirmPassword')});
  }

  get email() {
    return this.emailForm.get('email');
  }

  get code() {
    return this.resetForm.get('code');
  }

  get newPassword() {
    return this.resetForm.get('newPassword');
  }

  get confirmPassword() {
    return this.resetForm.get('confirmPassword');
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    if (this.countdownSub) {
      this.countdownSub.unsubscribe();
    }
  }


  sendVerificationCode() {
    if (this.emailForm.invalid) return;

    this.loading = true;
    this.requestResetCode.Email = this.email?.value;

    this.authService.requestResetCode(this.requestResetCode).subscribe({
      next: res => {
        this.loading = false;
        this.codeSent = true;
        this.startCountdown(res.codeExpirationMinutes);
        this.snackBar.open('Verification code sent to your email', 'Close', {
          duration: 3000
        });
      }, error: error => {
        console.log(error);
        this.loading = false;
      }, complete: () => {
        this.loading = false;
      }
    })

  }

  resendCode() {
    this.loading = true;
    this.codeSent = false;
    this.sendVerificationCode();
  }

  startCountdown(minutes: number) {
    this.minutes = minutes;
    this.seconds = 0;

    if (this.countdownSub) {
      this.countdownSub.unsubscribe();
    }

    this.countdownSub = timer(0, 1000).subscribe(() => {
      if (this.seconds === 0) {
        if (this.minutes === 0) {
          this.codeExpired = true;
          this.countdownSub?.unsubscribe();
          return;
        }
        this.minutes--;
        this.seconds = 59;
      } else {
        this.seconds--;
      }
    });
  }

  resetPassword() {
    if (this.resetForm.invalid || this.codeExpired) return;

    this.loading = true;
    this.resetPasswordViaCode.Email = this.email?.value;
    this.resetPasswordViaCode.NewPassword = this.newPassword?.value;
    this.resetPasswordViaCode.Code = this.code?.value;
    this.authService.resetPasswordViaCode(this.resetPasswordViaCode).subscribe({
      next: success => {
        this.loading = false;
        this.snackBar.open('Password reset successfully', 'Close', {
          duration: 3000
        });
        this.router.navigate(['/login']);
      }, error: error => {
        console.log(error);
        this.loading = false;
      }, complete: () => {
        this.loading = false;
      }
    })

  }
}
