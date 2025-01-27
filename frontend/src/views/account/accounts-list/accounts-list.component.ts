import { Component, inject } from '@angular/core';
import { AccountService } from '../../../services';
import { rxResource } from '@angular/core/rxjs-interop';
import { MatTableModule } from '@angular/material/table';
import { Account, AccountInfo } from '../../../models';

type AccountListColumns = keyof Account | keyof AccountInfo;

@Component({
  imports: [MatTableModule],
  templateUrl: './accounts-list.component.html',
  styleUrl: './accounts-list.component.scss'
})
export class AccountsListComponent {
  readonly #accountService = inject(AccountService);

  protected readonly displayedColumns: AccountListColumns[] = ['name', 'accountHolder'];

  protected readonly accounts$ = rxResource({
    loader: () => this.#accountService.getAccounts()
  })
}
