import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { LoginService } from 'src/app/core/services/login/login.service';
import { ILoginResponse, IUser } from 'src/app/shared/interfaces';
import { takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';
import { TokenService } from 'src/app/core/services/token/token.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit, OnDestroy {

  public loginForm: FormGroup;
  public hide: boolean = true;

  private destroyer$: Subject<void> = new Subject();

  constructor(
    private _loginService: LoginService,
    private _tokenService: TokenService,
    private _formBuilder: FormBuilder,
    private _router: Router
  ) {
    this.loginForm = this._formBuilder.group({
      login: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit(): void { }

  public login(user: IUser): void {
    this._loginService.login(user).pipe(
      takeUntil(this.destroyer$)
    ).subscribe((response: ILoginResponse) => {
      if (response.token) {
        this._tokenService.setToken(response.token);
        this._router.navigate(['transactions']);
      }
    });
  }

  public toRegistration(): void {
    this._router.navigate(['registration']);
  }

  ngOnDestroy(): void {
    this.destroyer$.next();
    this.destroyer$.complete();
  }

}
