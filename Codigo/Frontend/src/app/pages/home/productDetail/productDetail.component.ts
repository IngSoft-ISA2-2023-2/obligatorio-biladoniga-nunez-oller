import { Component, OnInit} from '@angular/core';
import { cilCart, cilPlus, cilCompass } from '@coreui/icons';
import { IconSetService } from '@coreui/icons-angular';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/interfaces/product';
import { StorageManager } from '../../../utils/storage-manager';
import { Router } from '@angular/router'; 
import { CommonService } from '../../../services/CommonService';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './productDetail.component.html',
  styleUrls: ['./productDetail.component.css'],
})
export class ProductDetailComponent implements OnInit {
  product: Product | undefined;
  quantity: number = 1;
  cart: any[] = [];

  constructor(
    private route: ActivatedRoute,
    public iconSet: IconSetService,
    private productService: ProductService,
    private storageManager: StorageManager,
    private router: Router,
    private commonService: CommonService
  ) {
    iconSet.icons = { cilCart, cilPlus, cilCompass };
  }

  ngOnInit(): void {
    this.getProduct();
    this.storageManager.saveData('total', JSON.stringify(0));
  }

  getProduct(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    this.productService.getProducts()
      .subscribe(products => this.product = products.find(({id: currId}) => currId === id));
  }

  addToCart(product: Product) {
    if (product) {
      this.cart = JSON.parse(this.storageManager.getData('cart'));
      if (!this.cart) {
        this.cart = [];
        this.storageManager.saveData('cart', JSON.stringify(this.cart));
      }
      
      let exist: boolean = false;
      for (let item of this.cart) {
        if (item.code === product.code && item.id === product.id){
          item.quantity += this.quantity;
          exist = true;
          break;
        }
      }
      if (!exist){
        product.quantity = this.quantity;
        this.cart.push(product);
      }
      this.storageManager.saveData('cart', JSON.stringify(this.cart));
    }
    this.updateHeader(this.cart.length);
    this.router.navigate(['/home/cart']);
  }

  updateHeader(value: number){
    this.commonService.updateHeaderData(value);
   }

}
