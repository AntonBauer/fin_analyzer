import { Component, inject, input } from '@angular/core';
import { AccountService } from '../../../services';
import { rxResource } from '@angular/core/rxjs-interop';

@Component({
  imports: [],
  templateUrl: './account-details.component.html',
  styleUrl: './account-details.component.scss'
})
export class AccountDetailsComponent {
  readonly #accountService = inject(AccountService)

  accountId = input.required<string>();

  account$ = rxResource({
    request: () => this.accountId(),
    loader: ({request: accountId}) => this.#accountService.getAccount(accountId)
  })
}
