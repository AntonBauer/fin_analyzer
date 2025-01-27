import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateRegexRuleRequest } from "../models";
import { EnvironmentService } from "./environment.service";
import { Suggestion } from "../models/dtos/suggestion.model";

@Injectable({
    providedIn: 'root',
})
export class RuleService {
    readonly #environmentService = inject(EnvironmentService);
    readonly #http = inject(HttpClient);

    createRegexRule(request: CreateRegexRuleRequest): Observable<number> {
        return this.#http.post<number>(`${this.#environmentService.getEnvironment().apiUrl}/rules`, request);
    }

    deleteRule(ruleId: number): Observable<any> {
        return this.#http.delete(`${this.#environmentService.getEnvironment().apiUrl}/rules/${ruleId}`);
    }

    applyRule(ruleId: number): Observable<Suggestion[]> {
        return this.#http.post<Suggestion[]>(`${this.#environmentService.getEnvironment().apiUrl}/rules/${ruleId}/apply`, {});
    }
}