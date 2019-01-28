import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Stats } from "../app/stats/stats";

@Component({
    selector: 'my-app',
    templateUrl: './stats.component.html'
})
export class AppComponent implements OnInit {
    restItemsUrl = 'http://localhost:60440/api/stats';
    restItems: Stats[];

    constructor(private http: HttpClient) { }

    ngOnInit() {
        this.getRestItems();
    }

    getRestItems(): void {
        this.restItemsServiceGetRestItems()
            .subscribe(
                restItems => {
                    this.restItems = restItems;
                    console.log(this.restItems);
                }
            )
    }

    // Rest Items Service: Read all REST Items
    restItemsServiceGetRestItems() {
        return this.http
            .get<Stats[]>(this.restItemsUrl)
            .pipe(map(data => data));
    }
}
