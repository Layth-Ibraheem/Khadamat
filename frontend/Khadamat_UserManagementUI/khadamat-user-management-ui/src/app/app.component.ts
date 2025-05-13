import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {LoaderComponent} from './shared/components/loader/loader.component';
import {AsyncPipe} from '@angular/common';
import {LoaderService} from './shared/services/loader.service';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, LoaderComponent, AsyncPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  loaderService = inject(LoaderService);
  title = 'khadamat-user-management-ui';

}
