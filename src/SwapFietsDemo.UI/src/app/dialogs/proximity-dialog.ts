import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

export interface DialogData {
    proximity: number;
  }
  
  @Component({
    selector: 'proximity-dialog',
    templateUrl: './proximity-dialog.html',
  })
  export class ProximityDialog {
    constructor(
      public dialogRef: MatDialogRef<ProximityDialog>,
      @Inject(MAT_DIALOG_DATA) public data: DialogData,
    ) {}
  
    onNoClick(): void {
      this.dialogRef.close();
    }
  }