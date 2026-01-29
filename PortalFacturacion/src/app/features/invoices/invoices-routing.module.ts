import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InvoiceListComponent } from './pages/invoice-list/invoice-list.component';
import { InvoiceFormComponent } from './pages/invoice-form/invoice-form.component';

const routes: Routes = [
  { path: '', component: InvoiceListComponent },
  { path: 'crear', component: InvoiceFormComponent },
  { path: 'ver/:id', component: InvoiceFormComponent } // View Only as typically Invoices are not edited after emission, or maybe edit? Check requirements. usually invoices are final or cancelled. I will assume View/Create.
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InvoicesRoutingModule { }
