import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpErrorResponse } from '@angular/common/http/src/response';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Stats } from './stats';

@Injectable()
export class StatsService {
    public stats_url = 'http://localhost:60440/api/stats';

    constructor(private http: HttpClient) { }

    getStats(): Observable<Stats> {
        return this.http.get<Stats>(this.stats_url).catch(this.errorHandler);
    }

    errorHandler(error: HttpErrorResponse) {
        return Observable.throw(error.message || "Server error");
    }
}