import { ChangeDetectionStrategy, Component, inject, input } from '@angular/core';
import { AccountService } from '../../../services';
import { rxResource } from '@angular/core/rxjs-interop';
import { MatTableModule } from '@angular/material/table';
import { RawTransaction, Transaction } from '../../../models';

type TransactionColumns = keyof Transaction | keyof RawTransaction;

@Component({
  imports: [
    MatTableModule,
  ],
  templateUrl: './account-details.component.html',
  styleUrl: './account-details.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AccountDetailsComponent {
  readonly #accountService = inject(AccountService)

  protected readonly accountId = input.required<string>();

  protected readonly accountRes = rxResource({
    request: () => this.accountId(),
    loader: ({request: accountId}) => this.#accountService.getAccount(accountId)
  })

  protected readonly displayedColumns: TransactionColumns[] = ['category', 'purpose', 'amount', 'valueDate']
}
