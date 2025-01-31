import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { CountryModel } from "../types/country.model";
import { Observable } from "rxjs";
import { CityModel } from "../types/city.model";
import { WeatherModel } from "../types/weather.model";

@Injectable()
export class WeatherService {
    constructor(private readonly apiService: ApiService
    ) {}

    getCountries(): Observable<Array<CountryModel>> {
        return this.apiService.get<Array<CountryModel>>('countries');
    }

    getCities(selectedCountryId: string): Observable<Array<CityModel>> {
      return this.apiService.get<Array<CityModel>>(`cities?country=${selectedCountryId}`);
    }

    getWeather(city: string): Observable<WeatherModel> {
      return this.apiService.get<WeatherModel>(`weathers?city=${city}`);
    }
}
