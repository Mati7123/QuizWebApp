import { Component } from '@angular/core'
import { FormBuilder, Validators } from '@angular/forms'
import { AuthService } from './auth.services'

@Component({
    templateUrl: './login.component.html'
})

export class LoginComponent{
    
    form: any
    constructor (public auth: AuthService, private fb: FormBuilder )
    {
        this.form = fb.group(
            {
                email: ['', Validators.required],
                password: ['', Validators.required]
            }
        )
    }

}

