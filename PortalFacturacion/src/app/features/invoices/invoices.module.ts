import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InvoicesRoutingModule } from './invoices-routing.module';
import { InvoiceListComponent } from './pages/invoice-list/invoice-list.component';
import { InvoiceFormComponent } from './pages/invoice-form/invoice-form.component';
import { SharedModule } from '../../shared/shared.module';
import { GeneralFilterComponent } from '../../shared/components/general-filter/general-filter.component';

@NgModule({
  declarations: [
    InvoiceListComponent,
    InvoiceFormComponent
  ],
  imports: [
    CommonModule,
    InvoicesRoutingModule,
    SharedModule,
    GeneralFilterComponent
  ]
})
export class InvoicesModule { }
