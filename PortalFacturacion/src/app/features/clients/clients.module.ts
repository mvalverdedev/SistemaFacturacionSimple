import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientsRoutingModule } from './clients-routing.module';
import { ClientListComponent } from './pages/client-list/client-list.component';
import { ClientFormComponent } from './pages/client-form/client-form.component';
import { SharedModule } from '../../shared/shared.module';
import { GeneralFilterComponent } from '../../shared/components/general-filter/general-filter.component';

@NgModule({
  declarations: [
    ClientListComponent,
    ClientFormComponent
  ],
  imports: [
    CommonModule,
    ClientsRoutingModule,
    SharedModule,
    GeneralFilterComponent
  ]
})
export class ClientsModule { }
