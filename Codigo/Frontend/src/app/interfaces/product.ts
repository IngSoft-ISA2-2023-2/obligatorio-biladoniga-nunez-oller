export interface Product {
    id: number;
    code: number;
    name: string;
    description: string;
    price: number;
    pharmacy: {
      id: number;
      name: string;  
    };
    stock: number;
    quantity: number;
}

export class ProductRequest {
  code: number;
  name: string;
  description: string;
  stock: number;
  price: number;
  pharmacyName: string = "";

  constructor(code: number, name: string, description: string, stock: number, price: number, pharmacyName: string){
    this.code = code;
    this.name = name;
    this.description = description;
    this.stock = stock;
    this.price = price;
    this.pharmacyName = pharmacyName;
  }
}

export class UpdateProductRequest {
  name: string;
  description: string;
  price: number;

  constructor(name: string, description: string, price: number){
    this.name = name;
    this.description = description;
    this.price = price;
  }
}