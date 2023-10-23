import { Component, OnInit } from "@angular/core";
import { AbstractControl, FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { Pharmacy } from "src/app/interfaces/pharmacy";
import { Role } from "src/app/interfaces/role";
import { CommonService } from "src/app/services/CommonService";
import { InvitationRequest } from 'src/app/interfaces/invitation';
import { InvitationService } from "src/app/services/invitation.service";
import { PharmacyService } from "src/app/services/pharmacy.service";
import { RoleService } from "src/app/services/role.service";
import { cilShortText, cilPencil, cilSync } from '@coreui/icons';
import { ActivatedRoute, Router } from "@angular/router";
import { Invitation } from "src/app/interfaces/invitation";
import { Product, UpdateProductRequest } from "src/app/interfaces/product";
import { ProductService } from "src/app/services/product.service";

@Component({
    selector: 'app-update-product',
    templateUrl: './update-product.component.html',
    styleUrls: ['./update-product.component.css'],
})

export class UpdateProductComponent implements OnInit {
    form: FormGroup | any;
    products: Product[] = [];
    currProduct: Product | any = null;

    icons = { cilShortText, cilPencil, cilSync }

    constructor(
        private fb: FormBuilder,
        private productService: ProductService,
        private commonService: CommonService,
        private route: ActivatedRoute) {
            this.form = fb.group({
                name: new FormControl(),
                description: new FormControl(),
                price: 0
            });
        };

    ngOnInit(): void {
        this.getProducts();

    }

    getProducts(): void {
        this.productService
        .getProducts()
        .subscribe((p) => {
            this.products = p;
            let id = this.route.snapshot.paramMap.get('id');
            this.currProduct = this.products.find(({id:pId}) => `${pId}` == id);
            this.form = this.fb.group({
              name: this.currProduct.name,
              description: this.currProduct.description,
              price: this.currProduct.price
          });
    })
    }

    setDetaultName(name: string): void {
            this.form.controls.name.setValue(name);
    }
    setDefaultDescription(description: string): void {
        this.form.controls.description.setValue(description);
    }
    setDetaultPrice(price: string): void {
        this.form.controls.price.setValue(price);
    }

    get name(): AbstractControl {
        return this.form.controls.name;
    }

    get description(): AbstractControl {
        return this.form.controls.description;
    }

    get price(): AbstractControl {
        return this.form.controls.price;
    }

    get product_id() {
        return Number(this.route.snapshot.paramMap.get('id'));;
    }

    updateProduct(): void {
        let name = this.name.value ? this.name.value : "";
        let description = this.description.value ? this.description.value : "";
        let price = this.price.value ? this.price.value : "";
        let id = this.product_id;

        let productRequest = new UpdateProductRequest(name, description, price);
        this.productService.updateProduct(id, productRequest).subscribe((invitation) => {
            if (invitation){
                this.commonService.updateToastData("Success updating invitation", 'success', 'Invitation updated.');
            }
        });
    }
}