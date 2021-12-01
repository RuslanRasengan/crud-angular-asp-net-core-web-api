import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  public getToken(): string {
    return sessionStorage.getItem('token');
  }

  public setToken(token: string): void {
    sessionStorage.setItem('token', token);
  }
}
