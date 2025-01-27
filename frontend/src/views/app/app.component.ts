import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AccountService } from '../../services';

@Component({
  selector: 'fan-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  readonly #accountService = inject(AccountService)

  protected uploadTransactionsFile(): void {}
}
