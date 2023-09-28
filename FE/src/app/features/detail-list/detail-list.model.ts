export interface OrderDto {
    customerName: string;
    customerDob: string;
    customerEmail: string;
    shopName: string;
    shopLocation: string | null;
    productName: string;
    productImage: string | null;
    price: number;
    quantity: number;
    customerDobStr: string;
}