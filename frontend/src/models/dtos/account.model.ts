import { AccountInfo } from "./account-info.model";
import { Transaction } from "./transaction.model";


export interface Account {
    id: string;
    info: AccountInfo;
    transactions: Transaction[];
}
