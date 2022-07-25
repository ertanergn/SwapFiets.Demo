import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MapInfoWindow, MapMarker } from '@angular/google-maps';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { City } from '../city';
import { DataService } from '../data.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
})
export class MapComponent implements OnInit {
  @Input()
  cities: City[] = [];
  apiLoaded: Observable<boolean>;
  mapOptions: google.maps.MapOptions;
  markerOptions: google.maps.MarkerOptions;
  @ViewChild(MapInfoWindow) infoWindow: MapInfoWindow;
  infoContent: City | undefined = {
    Latitude: 0,
    Longitude: 0
  };
  center: google.maps.LatLng;
  constructor(httpClient: HttpClient, private dataService: DataService) {
    this.apiLoaded = httpClient
      .jsonp(
        `https://maps.googleapis.com/maps/api/js?key=${environment.gk}`,
        'callback'
      )
      .pipe(
        map(() => true),
        catchError(() => of(false))
      );
  }

  ngOnInit(): void {
    this.mapOptions = {
      zoom: 4,
    };

    this.markerOptions = {
      draggable: false,
    }
    this.dataService.selectedCity.subscribe(c => {
      let myLatlng = new google.maps.LatLng(c.Latitude, c.Longitude);
      this.center = myLatlng;
      this.infoWindow.close();
    })
  }

  getMarkerPosition(city: City) {
    const position = {
      lat: city?.Latitude,
      lng: city?.Longitude
    };
    return position as google.maps.LatLngLiteral;
  }

  openInfo(marker: MapMarker, city: City) {

    this.dataService.getCityBikeStats(city)
    .subscribe({
      next: (data: any) => {

        city.NotStolen = data.notStolen;
        city.StolenWithinProximity = data.stolenWithinProximity;
        city.Stolen = data.stolen;
        city.Proximity = this.dataService.proximity;

        this.infoContent = city;
      },
      error: (e) => {
        console.error(e)
        alert(`Cannot retrieve info for ${city.City}, please check logs.`);
      }
    });

    this.infoContent = undefined;
    this.infoWindow.open(marker);
    this.dataService.clearSelected.next(true);
  }
}
