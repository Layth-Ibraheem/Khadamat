import {Component, inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormControl, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatCardModule} from '@angular/material/card';
import {MatDividerModule} from '@angular/material/divider';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-reject-application-dialog',
  imports: [MatCardModule, MatDividerModule, MatFormFieldModule, MatInputModule, FormsModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './reject-application-dialog.component.html',
  styleUrl: './reject-application-dialog.component.scss'
})
export class RejectApplicationDialogComponent {
  private dialogRef = inject(MatDialogRef<RejectApplicationDialogComponent>);
  public data = inject(MAT_DIALOG_DATA);

  reasonControl = new FormControl('', [Validators.required, Validators.minLength(10)]);

  onReject() {
    if (this.reasonControl.valid) {
      this.dialogRef.close({
        action: 'reject',
        reason: this.reasonControl.value
      });
    }
  }

  onCancel() {
    this.dialogRef.close({action: 'cancel'});
  }
}
