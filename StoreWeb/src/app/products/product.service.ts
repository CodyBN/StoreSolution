import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';

export interface Product {
  id: number;
  name: string;
  description?: string;
  price: number;
  categoryName: string;
  stockQuantity: number;
}

@Injectable({ providedIn: 'root' })
export class ProductService {
    //Hard coded for simplicity. You would want to determine this by environment or app configs.
  private apiUrl = 'https://localhost:7097/api/store/products';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl)
      .pipe(
        catchError(err => {
          console.error('API error:', err);
          return throwError(() => new Error('Error: Failed to load products'));
        })
      );
  }
}
