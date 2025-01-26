import { AccountInfo } from "./account-info.model";
import { Transaction } from "./transaction.model";


export interface Account {
    id: string; // Guid is represented as a string in TypeScript
    info: AccountInfo;
    transactions: Transaction[];
}
