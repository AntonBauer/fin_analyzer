import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AccountService } from '../../services';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { LoadTransactionsDialogComponent } from '../load-transactions-dialog/load-transactions-dialog.component';
import { filter, switchMap } from 'rxjs';

@Component({
  selector: 'fan-root',
  imports: [
    RouterOutlet,
    MatButtonModule,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  readonly #accountService = inject(AccountService)
  readonly dialog = inject(MatDialog);

  protected uploadTransactionsFile(): void {
    const dialogRef = this.dialog.open(LoadTransactionsDialogComponent);

    dialogRef.afterClosed()
      .pipe(
        filter(file => file !== undefined),
        switchMap(file => this.#accountService.uploadTransactionsFile(file))
      ).subscribe();
  }
}
