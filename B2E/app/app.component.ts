import { Component, OnInit } from "@angular/core";
import { StatsService } from "./stats/stats.service";
import { Stats } from "./stats/stats"

@Component({
    selector: 'app-stats',
    templateUrl: './stats/stats.component.html'
})
export class AppComponent implements OnInit  {
    stats: Stats;

    constructor(private statsService: StatsService) { }

    ngOnInit() {
        this.getStats();
    }

    getStats() {
        this.statsService.getStats().subscribe(dados => this.stats = dados);
    }
}