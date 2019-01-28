"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var Observable_1 = require("rxjs/Observable");
require("rxjs/add/operator/catch");
require("rxjs/add/observable/throw");
var StatsService = /** @class */ (function () {
    function StatsService(http) {
        this.http = http;
        this.stats_url = 'http://localhost:60440/api/stats';
    }
    StatsService.prototype.getStats = function () {
        return this.http.get(this.stats_url).catch(this.errorHandler);
    };
    StatsService.prototype.errorHandler = function (error) {
        return Observable_1.Observable.throw(error.message || "Server error");
    };
    StatsService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], StatsService);
    return StatsService;
}());
exports.StatsService = StatsService;
//# sourceMappingURL=stats.service.js.map