import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { AccountService } from '../../../services';
import { rxResource } from '@angular/core/rxjs-interop';
import { MatTableModule } from '@angular/material/table';
import { Account, AccountInfo } from '../../../models';
import { RouterLink } from '@angular/router';

type AccountListColumns = keyof Account | keyof AccountInfo;

@Component({
  imports: [
    MatTableModule,
    RouterLink
  ],
  templateUrl: './accounts-list.component.html',
  styleUrl: './accounts-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AccountsListComponent {
  readonly #accountService = inject(AccountService);

  protected readonly displayedColumns: AccountListColumns[] = ['name', 'accountHolder'];

  protected readonly accountsRes = rxResource({
    loader: () => this.#accountService.getAccounts()
  })
}
