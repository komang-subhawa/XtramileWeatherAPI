import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class ApiService {
    constructor(private readonly http: HttpClient) {}
    baseUrl: string = 'https://localhost:5001/';

    get<T>(endpoint: string): Observable<T> {
        return this.http.get<T>(`${this.baseUrl}${endpoint}`);
    }
}

