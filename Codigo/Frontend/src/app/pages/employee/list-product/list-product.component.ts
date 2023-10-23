import { Component, Input, OnInit } from "@angular/core";
import { Invitation } from "src/app/interfaces/invitation";
import { CommonService } from "src/app/services/CommonService";
import { InvitationService } from "src/app/services/invitation.service";
import { cilSync } from '@coreui/icons';
import { Router } from "@angular/router";
import { Pharmacy } from "src/app/interfaces/pharmacy";
import { PharmacyService } from "src/app/services/pharmacy.service";
import { RoleService } from "src/app/services/role.service";
import { Role } from "src/app/interfaces/role";
import { TitleStrategy } from "@angular/router";
import { Product } from "src/app/interfaces/product";
import { ProductService } from "src/app/services/product.service";

@Component({
    selector: 'app-list-product',
    templateUrl: './list-product.component.html',
    styleUrls: ['./list-product.component.css'],
})

export class ListProductComponent implements OnInit {
    products: Product[] = [];

    icons = { cilSync };

    constructor(
        private productService: ProductService,
        private route: Router){}

    ngOnInit(): void {
        this.getProducts();
    };

    getProducts(): void {
        this.productService
          .getProducts()
          .subscribe((p) => (this.products = p));
      }
    
    update(id: number): void {
        this.route.navigate(['employee/update-product', 
        { id: id }])
    }
}