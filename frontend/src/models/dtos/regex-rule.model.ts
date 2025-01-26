import { TransactionProperty } from "../enums/transaction-property.enum";
import { Category } from "./category.model";

export interface RegexRule {
    id: number; // uint is represented as number in TypeScript
    name: string;
    expression: string;
    propertyToCheck: TransactionProperty;
    suggestedCategory: Category;
}