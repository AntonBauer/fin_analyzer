import { AccountInfo } from "./account-info";
import { Transaction } from "./transaction";


export interface Account {
    id: string; // Guid is represented as a string in TypeScript
    info: AccountInfo;
    transactions: Transaction[];
}
