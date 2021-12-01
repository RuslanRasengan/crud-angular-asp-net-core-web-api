import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenService } from '../services/token/token.service';

@Injectable()
export class PrimaryInterceptor implements HttpInterceptor {

  constructor(private _tokenService: TokenService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (this._tokenService.getToken() && !request.url.includes('/login') && !request.url.includes('/register') ) {
      request = request.clone({ setHeaders: { Authorization: 'Bearer ' + this._tokenService.getToken() } });
    }

    return next.handle(request);
  }

}
