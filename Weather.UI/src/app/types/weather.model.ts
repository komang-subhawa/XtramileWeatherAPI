export class WeatherModel {
  location: LocationModel;
  time: number;
  wind: number;
  visibility: number;
  skyCondition: string;
  temperatureCelcius: number;
  temperatureFahrenheit: number;
  dewPoint: string;
  relativeHumidity: number;
  pressure: number;
}

export class LocationModel {
  longitude: number;
  latitude: number;
}
