import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { EnvironmentService } from "./environment.service";

@Injectable({
    providedIn: 'root',
})
export class SuggestionsService {
    readonly #environmentService = inject(EnvironmentService);
    readonly #http = inject(HttpClient);

    getSuggestions(): Observable<any> {
        return this.#http.get(`${this.#environmentService.getEnvironment().apiUrl}/suggestions`);
    }

    applySuggestion(suggestionId: string): Observable<any> {
        return this.#http.post(`${this.#environmentService.getEnvironment().apiUrl}/suggestions/${suggestionId}/apply`, {});
    }
}