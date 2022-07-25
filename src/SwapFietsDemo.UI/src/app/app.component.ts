import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { City } from './city';
import { DataService } from './data.service';
import { ProximityDialog } from './dialogs/proximity-dialog';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  cities: City[] = [];
  title = 'Bike indexes for cities';

  constructor(private dataService: DataService, public dialog: MatDialog) {}
  ngOnInit(): void {
    this.dataService.getData().subscribe((d: City[]) => {
      this.cities = d;
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ProximityDialog, {
      width: '250px',
      data: {proximity: this.dataService.proximity},
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');

      if(result){
        this.dataService.setProximity(result);
      }
    });
  }
}