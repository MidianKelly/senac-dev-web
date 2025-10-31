import { Component, signal} from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NgIcons],
  templateUrl: './app.html',
  viewProviders: [provideIcons({ featherAirplay, heroUsers })],
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('MeuCorre');
}
