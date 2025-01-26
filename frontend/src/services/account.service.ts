import { HttpClient, HttpParams } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { EnvironmentService } from "./environment.service";

@Injectable({
    providedIn: 'root',
})
export class AccountService {
    readonly #environmentService = inject(EnvironmentService);
    readonly #http = inject(HttpClient);

    getAccount(accountId: string): Observable<any> {
        return this.#http.get(`${this.#environmentService.getEnvironment().apiUrl}/accounts/${accountId}`);
    }

    getTransactions(accountId: string, from?: string, to?: string): Observable<any> {
        let params = new HttpParams();
        if (from) params = params.set('from', from);
        if (to) params = params.set('to', to);
        return this.#http.get(`${this.#environmentService.getEnvironment().apiUrl}/accounts/${accountId}/transactions`, { params });
    }

    uploadTransactionsFile(file: File): Observable<any> {
        const formData = new FormData();
        formData.append('transactionsFile', file);
        return this.#http.post(`${this.#environmentService.getEnvironment().apiUrl}/upload-ing-data`, formData);
    }
}