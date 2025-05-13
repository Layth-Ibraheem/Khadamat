import {HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {LoaderService} from '../services/loader.service';
import {finalize} from 'rxjs';

export const loaderInterceptor: HttpInterceptorFn = (req, next) => {
  const startTime = Date.now();
  const MIN_SHOW_TIME = 500; // ms

  let loaderService = inject(LoaderService);
  loaderService.show('Loading...');
  return next(req).pipe(
    finalize(() => {
      const elapsed = Date.now() - startTime;
      const remaining = Math.max(0, MIN_SHOW_TIME - elapsed);
      setTimeout(() => loaderService.hide(), remaining);
    })
  )
};
