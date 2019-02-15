import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class StatsService {

    statsUrl = "http://localhost:60440/api/stats"

    constructor(private http: HttpClient) { }

    getStats() {
        return this.http.get<any>(`${this.statsUrl}`);
    }
}