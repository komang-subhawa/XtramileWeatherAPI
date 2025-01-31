import { Component, OnInit } from '@angular/core';
import { CountryModel } from './types/country.model';
import { WeatherService } from './services/weather.service';
import { CityModel } from './types/city.model';
import { WeatherModel } from './types/weather.model';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Weather.UI';
  countries: Array<CountryModel>;
  cities: Array<CityModel>;
  selectedCountryId: string;
  selectedCity: string;
  weatherDetail: WeatherModel;

  constructor(private readonly weatherService: WeatherService) {}

  ngOnInit(): void {
    this.weatherService.getCountries().subscribe(response => {
      this.countries = response;
    });
  };

  onCountryChange(): void {
    this.selectedCity = undefined;
    this.weatherDetail = undefined;
    this.weatherService.getCities(this.selectedCountryId).subscribe(response => {
      this.cities = response;
    });
  }

  onCityChange(): void {
    if (!this.selectedCity) {
      return;
    }

    this.weatherService.getWeather(this.selectedCity).subscribe(response => {
      this.weatherDetail = response;
    });
  }
}
