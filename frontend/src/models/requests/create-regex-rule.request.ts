export const enum TransactionProperty {
    // Define enum values if known
}

export interface CreateRegexRuleRequest {
    name: string;
    propertyToCheck: TransactionProperty;
    expression: string;
    suggestedCategoryId: number;
}
