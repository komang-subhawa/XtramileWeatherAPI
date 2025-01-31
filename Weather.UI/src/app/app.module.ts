import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { httpInterceptorProviders } from './http-interceptors';
import { WeatherService } from './services/weather.service';
import { ApiService } from './services/api.service';
import { HttpClientModule } from '@angular/common/http';
import { EnvServiceProvider } from './env.service.provider';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    NoopAnimationsModule,
    MatSelectModule,
    MatCardModule,
    MatGridListModule,
    HttpClientModule
  ],
  providers: [
    httpInterceptorProviders,
    ApiService,
    WeatherService,
    EnvServiceProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
