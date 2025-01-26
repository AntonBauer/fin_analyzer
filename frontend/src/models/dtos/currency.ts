import { CurrencyName } from "../enums/currency-name.enum";


export interface Currency {
    amount: number; // decimal is represented as number in TypeScript
    name: CurrencyName;
}
