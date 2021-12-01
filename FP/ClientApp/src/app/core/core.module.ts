import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrimaryInterceptor } from './interceptors/primary.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: PrimaryInterceptor,
      multi: true
    }
  ]
})
export class CoreModule { }
