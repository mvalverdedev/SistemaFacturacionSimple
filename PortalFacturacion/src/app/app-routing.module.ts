import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { MainLayoutComponent } from './core/components/main-layout/main-layout.component';

const routes: Routes = [
  {
    path: 'login',
    loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', loadChildren: () => import('./features/dashboard/dashboard.module').then(m => m.DashboardModule) },
      { path: 'facturas', loadChildren: () => import('./features/invoices/invoices.module').then(m => m.InvoicesModule) },
      { path: 'clientes', loadChildren: () => import('./features/clients/clients.module').then(m => m.ClientsModule) },
      { path: 'productos', loadChildren: () => import('./features/products/products.module').then(m => m.ProductsModule) },
      { path: 'usuarios', loadChildren: () => import('./features/users/users.module').then(m => m.UsersModule) }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
