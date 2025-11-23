import { Component, signal } from '@angular/core';
import { RouterModule, Routes, RouterOutlet } from '@angular/router';
import { ProductListComponent } from './products/product-list.component';

const routes: Routes = [
  { path: '', component: ProductListComponent }
];

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, RouterOutlet, ProductListComponent],
  template: `<router-outlet></router-outlet>`,
})
export class App {}
