import { Currency } from "./currency";


export interface RawTransaction {
    id: string;
    booking: string; // DateOnly is typically represented as a string (ISO format)
    valueDate: string;
    payerOrPayee: string;
    bookingText: string;
    purpose: string;
    balance?: Currency;
    amount: Currency;
}
