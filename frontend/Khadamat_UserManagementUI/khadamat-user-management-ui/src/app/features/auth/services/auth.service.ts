import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {LoginRequest} from '../models/LoginRequest';
import {BehaviorSubject, map, Observable, tap, timeout} from 'rxjs';
import {RegisterRequest} from '../models/RegisterRequest';
import {RequestResetPasswordCodeRequest} from '../models/RequestResetPasswordCodeRequest';
import {ResetPasswordViaCodeRequest} from '../models/ResetPasswordViaCodeRequest';
import {ChangePasswordRequest} from '../models/ChangePasswordRequest';
import {AuthenticationResponse} from '../models/AuthenticationResponse';
import {Router} from '@angular/router';
import {StorageService} from '../../../shared/services/storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http: HttpClient = inject(HttpClient);
  private storage = inject(StorageService);
  private readonly BASE_URL = `https://localhost:7296/users`;
  private currentAuthUserSubject: BehaviorSubject<AuthenticationResponse | null>;
  public currentAuthUser: Observable<AuthenticationResponse | null>;
  private router = inject(Router);

  constructor() {
    // Initialize with value from storage or null
    const authUser = this.storage.getItem('authUser');
    this.currentAuthUserSubject = new BehaviorSubject<AuthenticationResponse | null>(
      authUser ? JSON.parse(authUser) : null
    );
    this.currentAuthUser = this.currentAuthUserSubject.asObservable();
  }

  public get currentAuthUserValue(): AuthenticationResponse | null {
    return this.currentAuthUserSubject.value;
  }

  login(loginRequest: LoginRequest): Observable<AuthenticationResponse> {
    return this.http.post<AuthenticationResponse>(`${this.BASE_URL}/login`, loginRequest)
      .pipe(
        tap((response: AuthenticationResponse) => {
          this.storage.setItem('authUser', JSON.stringify(response));
          this.currentAuthUserSubject.next(response);
        })
      );
  }

  logout() {
    this.storage.removeItem('authUser');
    this.currentAuthUserSubject.next(null);
    this.router.navigate(['/login']);
  }

  register(registerRequest: RegisterRequest): Observable<AuthenticationResponse> {
    return this.http.post<AuthenticationResponse>(`${this.BASE_URL}/register`, registerRequest)
      .pipe(
        //timeout(3000)
      );
  }

  requestResetCode(resetCodeRequest: RequestResetPasswordCodeRequest) {
    return this.http.get<any>(`${this.BASE_URL}/requestResetPasswordCode/${resetCodeRequest.Email}`)
      .pipe(
        //timeout(3000)
      );
  }

  resetPasswordViaCode(resetPasswordViaCodeRequest: ResetPasswordViaCodeRequest) {
    return this.http.put(`${this.BASE_URL}/resetPasswordViaCode`, resetPasswordViaCodeRequest)
      .pipe(
        //timeout(3000)
      );
  }

  changePassword(changePasswordRequest: ChangePasswordRequest) {
    return this.http.put<any>(`${this.BASE_URL}/changePassword`, changePasswordRequest)
      .pipe(
        //timeout(3000)
      )
  }
}

