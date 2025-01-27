import { HttpClient, HttpParams } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { EnvironmentService } from "./environment.service";
import { Account, Transaction } from "../models";

@Injectable({
    providedIn: 'root',
})
export class AccountService {
    readonly #environmentService = inject(EnvironmentService);
    readonly #http = inject(HttpClient);

    getAccount(accountId: string): Observable<Account> {
        return this.#http.get<Account>(`${this.#environmentService.getEnvironment().apiUrl}/accounts/${accountId}`);
    }

    getTransactions(accountId: string, from?: string, to?: string): Observable<Transaction[]> {
        let params = new HttpParams();
        if (from) params = params.set('from', from);
        if (to) params = params.set('to', to);
        return this.#http.get<Transaction[]>(`${this.#environmentService.getEnvironment().apiUrl}/accounts/${accountId}/transactions`, { params });
    }

    uploadTransactionsFile(file: File): Observable<number> {
        const formData = new FormData();
        formData.append('transactionsFile', file);
        return this.#http.post<number>(`${this.#environmentService.getEnvironment().apiUrl}/upload-ing-data`, formData);
    }
}