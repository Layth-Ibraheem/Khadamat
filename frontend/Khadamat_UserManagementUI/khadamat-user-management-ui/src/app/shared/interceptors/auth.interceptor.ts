import {HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {AuthService} from '../../features/auth/services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  let excludedRoutes = [
    '/users/login',
    '/users/register',
    '/users/requestResetPasswordCode',
    '/users/resetPasswordViaCode',
    '/RegisterApplications/createNew',
  ];
  let isExcludedRoute = (url: string): boolean => {
    return excludedRoutes.some(excludedRoute => {
      return url.includes(excludedRoute) || url.endsWith(excludedRoute);
    });
  }

  if (!isExcludedRoute(req.url)) {
    const authService = inject(AuthService);
    const token = authService.currentAuthUserValue?.token!;

    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }
  return next(req);
};
