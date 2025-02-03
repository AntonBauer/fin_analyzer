import { ChangeDetectionStrategy, Component, effect, linkedSignal, signal } from '@angular/core';
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
  protected file = signal<File | undefined>(undefined);
  protected fileName = linkedSignal(() => this.file()?.name || 'No file selected');

  protected onFileSelected($event: Event): void {
    const file = ($event.target as HTMLInputElement)?.files?.[0] as File;
    this.file.set(file);
  }
}
