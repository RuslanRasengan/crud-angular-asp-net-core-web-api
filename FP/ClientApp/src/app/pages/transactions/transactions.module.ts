import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionsComponent } from './transactions.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { MaterialModule } from 'src/app/material/material.module';
import { TransactionsRoutingModule } from './transactions-routing.module';



@NgModule({
  declarations: [
    TransactionsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MaterialModule,
    TransactionsRoutingModule
  ]
})
export class TransactionsModule { }
