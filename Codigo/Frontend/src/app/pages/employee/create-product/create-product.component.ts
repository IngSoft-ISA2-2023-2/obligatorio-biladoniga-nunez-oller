import { Component, OnInit } from '@angular/core';
import { cilBarcode, cilPencil, cilPaint, cilAlignCenter, cilDollar, cilLibrary, cilLoop1, cilTask, cilShortText } from '@coreui/icons';
import { AbstractControl, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Pharmacy } from '../../../interfaces/pharmacy';
import { CommonService } from '../../../services/CommonService';
import { ProductService } from 'src/app/services/product.service';
import { ProductRequest } from 'src/app/interfaces/product';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css'],
})
export class CreateProductComponent implements OnInit {
  form: FormGroup | any;
  pharmacys: Pharmacy[] = [];

  icons = { cilBarcode, cilPencil, cilAlignCenter, cilLibrary,
    cilDollar, cilLoop1, cilTask, cilShortText, cilPaint };

  constructor(
    private commonService: CommonService,
    private productService: ProductService,
    private fb: FormBuilder
  ) {
    
    this.form = this.fb.group({
        name: new FormControl(),
        code: new FormControl(),
        description: new FormControl(),
        price: new FormControl(),
        stock: 10,
      });
  }

  ngOnInit(){}

  get name(): AbstractControl {
    return this.form.controls.name;
  }

  get code(): AbstractControl {
    return this.form.controls.code;
  }

  get description(): AbstractControl {
    return this.form.controls.description;
  }

  get stock(): AbstractControl {
    return this.form.controls.stock;
  }
  
  get price(): AbstractControl {
    return this.form.controls.price;
  }

  createProduct(): void {
    let name = this.name.value ? this.name.value : "";
    let code = this.code.value ? this.code.value : 0;
    let description = this.description.value ? this.description.value : "";
    let stock = this.stock.value ? this.stock.value : 0;
    let price = this.price.value ? this.price.value : 0;

    let productRequest = new ProductRequest(code, name, description, stock, price, "");
        this.productService.createDrug(productRequest).subscribe((product) => {
        this.form.reset();

        if (product){
          this.commonService.updateToastData(
            `Success creating "${product.code} - ${product.name}"`,
            'success',
            'Product created.'
          );
        }
      });
  }
}
