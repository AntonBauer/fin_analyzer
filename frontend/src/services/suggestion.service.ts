import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { EnvironmentService } from "./environment.service";
import { Suggestion } from "../models/dtos/suggestion.model";

@Injectable({
    providedIn: 'root',
})
export class SuggestionsService {
    readonly #environmentService = inject(EnvironmentService);
    readonly #http = inject(HttpClient);

    getSuggestions(): Observable<Suggestion[]> {
        return this.#http.get<Suggestion[]>(`${this.#environmentService.getEnvironment().apiUrl}/suggestions`);
    }

    applySuggestion(suggestionId: string): Observable<any> {
        return this.#http.post(`${this.#environmentService.getEnvironment().apiUrl}/suggestions/${suggestionId}/apply`, {});
    }
}