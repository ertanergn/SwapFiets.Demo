export class City {
  Rank?:number
  City?: string;
  Latitude: number;
  Longitude: number;
  Country?: string;
  Proximity?: number;
  Stolen?: number;
  StolenWithinProximity?: number;
  NotStolen?: number;

  constructor(
    Rank: number,
    City: string,
    Latitude: number,
    Longitude: number,
    Country: string,
    Proximity: number,
    Stolen: number,
    StolenWithinProximity: number,
    NotStolen?: number,
  ) {
    this.Rank = Rank;
    this.City = City;
    this.Latitude = Latitude;
    this.Longitude = Longitude;
    this.Country = Country,
    this.Proximity = Proximity,
    this.Stolen = Stolen,
    this.StolenWithinProximity = StolenWithinProximity,
    this.NotStolen = NotStolen
  }
}
