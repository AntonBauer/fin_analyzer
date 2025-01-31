import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';

@Component({
  imports: [MatDialogModule, MatButtonModule, MatIconModule],
  templateUrl: './load-transactions-dialog.component.html',
  styleUrl: './load-transactions-dialog.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoadTransactionsDialogComponent {
  protected onFileSelected($event: Event): void {
  }
}
