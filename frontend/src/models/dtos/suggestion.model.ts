import { Category } from "./category.model";
import { Transaction } from "./transaction.model";

export interface Suggestion {
    readonly id: string;
    readonly transaction: Transaction;
    readonly suggestedCategory: Category;
}