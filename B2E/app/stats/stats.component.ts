import { Component, OnInit } from "@angular/core";
import { StatsService } from "./stats.service";
import { Stats } from "./stats"

@Component({
    selector: 'app-stats',
    templateUrl: './stats.component.html',
    styleUrls: ['./stats.component.css']
})

export class StatsComponent implements OnInit {
    stats: Stats;

    constructor(private statsService: StatsService) { }

    ngOnInit() {
        this.getStats();
    }

    getStats() {
        this.statsService.getStats().subscribe(dados => this.stats = dados);
    }
}