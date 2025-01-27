import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AccountService } from '../../services';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'fan-root',
  imports: [RouterOutlet, MatButtonModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  readonly #accountService = inject(AccountService)

  protected uploadTransactionsFile(): void {}
}
