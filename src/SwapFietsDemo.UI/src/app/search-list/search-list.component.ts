import { AfterContentChecked, AfterContentInit, AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { City } from '../city';
import { DataService } from '../data.service';

@Component({
  selector: 'app-search-list',
  templateUrl: './search-list.component.html',
  styleUrls: ['./search-list.component.scss'],
})
export class SearchListComponent implements OnInit {
  @Input()
  cities: City[] = [];
  filteredCities: City[] = [];
  searchTerm = '';
  selectedCity: City;
  @ViewChild('cityList') cityList: MatSelectionList;
  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    this.dataService.clearSelected.subscribe(d => {
      this.cityList.deselectAll();
    })
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.searchTerm = filterValue.toLowerCase();
  }

  selectedOption(list: MatListOption[]) {
    this.dataService.selectedCity.next(list[0].value as City);
  }
}
