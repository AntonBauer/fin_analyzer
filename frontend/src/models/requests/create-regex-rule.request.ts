import { TransactionProperty } from "../enums/transaction-property.enum";

export interface CreateRegexRuleRequest {
    name: string;
    propertyToCheck: TransactionProperty;
    expression: string;
    suggestedCategoryId: number;
}
