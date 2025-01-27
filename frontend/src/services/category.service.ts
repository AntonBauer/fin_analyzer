import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { EnvironmentService } from "./environment.service";
import { HttpClient } from "@angular/common/http";
import { CreateCategoryRequest, Transaction } from "../models";

@Injectable({
    providedIn: 'root',
})
export class CategoryService {
    readonly #http = inject(HttpClient);
    readonly #environmentService = inject(EnvironmentService);

    createCategory(request: CreateCategoryRequest): Observable<number> {
        return this.#http.post<number>(`${this.#environmentService.getEnvironment().apiUrl}/categories`, request);
    }

    deleteCategory(categoryId: number): Observable<any> {
        return this.#http.delete(`${this.#environmentService.getEnvironment().apiUrl}/categories/${categoryId}`);
    }

    assignCategory(accountId: string, transactionId: string, categoryId: number): Observable<Transaction> {
        return this.#http.put<Transaction>(`${this.#environmentService.getEnvironment().apiUrl}/accounts/${accountId}/transactions/${transactionId}/categories/${categoryId}/assign`, {});
    }

    removeCategory(accountId: string, transactionId: string): Observable<Transaction> {
        return this.#http.delete<Transaction>(`${this.#environmentService.getEnvironment().apiUrl}/accounts/${accountId}/transactions/${transactionId}/categories`);
    }
}