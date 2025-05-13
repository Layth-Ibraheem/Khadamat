import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {NgbAlert} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NgbAlert],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'khadamat-seller-portal-ui';

}
