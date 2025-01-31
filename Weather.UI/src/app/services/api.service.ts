import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EnvService } from './env.service';

@Injectable()
export class ApiService {
    constructor(private readonly http: HttpClient, private readonly env: EnvService) {}

    get<T>(endpoint: string): Observable<T> {
        return this.http.get<T>(`${this.baseUrl}${endpoint}`);
    }

    get baseUrl(): string { return `${this.env.apiUrl}`; }
}

