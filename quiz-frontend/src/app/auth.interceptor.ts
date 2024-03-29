import { Injectable } from '@angular/core'
import { HttpInterceptor } from '@angular/common/http'
import { registerLocaleData } from '@angular/common'


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor() {}

    intercept(req: any, next: any) {
        var token = localStorage.getItem('token')
        var authRequest = req.clone({
            headers: req.headers.set('Authorization', `Bearer ${token}`)
        })
        return next.handle(authRequest)
    }
}