import { RawTransaction } from "./raw-transaction";


export interface Transaction {
    id: string;
    rawTransaction: RawTransaction;
    category?: Category;
}
