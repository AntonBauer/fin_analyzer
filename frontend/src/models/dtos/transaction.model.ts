import { Category } from "./category.model";
import { RawTransaction } from "./raw-transaction.model";


export interface Transaction {
    id: string;
    rawTransaction: RawTransaction;
    category?: Category;
}
