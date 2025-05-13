import {Injectable} from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  private isLoading = new BehaviorSubject<boolean>(false);
  private loadingText = new BehaviorSubject<string>('');

  get isLoading$() {
    return this.isLoading.asObservable();
  }

  get loadingText$() {
    return this.loadingText.asObservable();
  }

  show(text: string = '') {
    this.loadingText.next(text);
    this.isLoading.next(true);
  }

  hide() {
    this.isLoading.next(false);
    this.loadingText.next('');
  }
}
