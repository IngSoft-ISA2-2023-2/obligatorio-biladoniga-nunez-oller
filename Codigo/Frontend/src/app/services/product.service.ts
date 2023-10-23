import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Product, ProductRequest, UpdateProductRequest } from '../interfaces/product';
import { environment } from '../../environments/environment';
import { CommonService } from './CommonService';
import { StorageManager } from '../utils/storage-manager';

@Injectable({ providedIn: 'root' })
export class ProductService {

  private url = environment.apiUrl + '/api/products';

  constructor(
    private http: HttpClient,
    private commonService: CommonService,
    private storageManager: StorageManager) { }

  getHttpHeaders(): HttpHeaders {
    let login = JSON.parse(this.storageManager.getLogin());
    let token = login ? login.token : "";
    
    return new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', token);
  }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.url, {headers: this.getHttpHeaders() })
      .pipe(
        tap(),
        catchError(this.handleError<Product[]>('Get Products', []))
      );
  }

  createDrug(drug: ProductRequest): Observable<Product> {
    return this.http.post<Product>(this.url, drug, {headers: this.getHttpHeaders() })
    .pipe(
      tap(),
      catchError(this.handleError<Product>('Create Product'))
    );
  }

  deleteProduct(id: number): Observable<any> {
    const url = `${this.url}/${id}`;
    return this.http.delete<any>(url, {headers: this.getHttpHeaders() })
    .pipe(
      tap(),
      catchError(this.handleError<any>('Delete Drug'))
    );
  }

  updateProduct(id: number, product: UpdateProductRequest): Observable<Product> {
    const url = `${this.url}/${id}`;
    return this.http.put<Product>(url, product, {headers: this.getHttpHeaders() }).pipe(
      tap(),
      catchError(this.handleError<Product>('Update Product'))
    );
  }
  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      //console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a ProductService error with the MessageService */
  private log(message: string) {
    this.commonService.updateToastData(message, "danger", "Error");
  }
}
