import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { RegistrationService } from 'src/app/core/services/registration/registration.service';
import { IRegisterResponse, IUser } from 'src/app/shared/interfaces';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.sass']
})
export class RegistrationComponent implements OnInit {

  public registrationForm: FormGroup;
  public hide: boolean = true;

  private destroyer$: Subject<void> = new Subject();

  constructor(
    private _registerService: RegistrationService,
    private _formBuilder: FormBuilder,
    private _router: Router,
    private _toastr: ToastrService
  ) {
    this.registrationForm = this._formBuilder.group({
      login: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email, Validators.minLength(8)]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
  }


  public register(user: IUser): void {
    this._registerService.register(user).pipe(
      takeUntil(this.destroyer$)
    ).subscribe((response: IRegisterResponse) => {
      if (response.status === "Success") {
        this._toastr.success(response.message, 'Success');
        this._router.navigate(['login']);
      }
    })
  }

  public toLogin(): void {
    this._router.navigate(['login']);
  }

}
