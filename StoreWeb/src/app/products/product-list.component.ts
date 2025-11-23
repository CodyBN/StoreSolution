import { Component, OnInit, signal } from '@angular/core';
import { ProductService, Product } from './product.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product.HTML',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {
  products = signal<Product[]>([]);
  errorMessage = signal<string>('');

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.productService.getProducts().subscribe({
      next: data => this.products.set(data),
      error: err => this.errorMessage.set(err.message)
    });
  }
}
