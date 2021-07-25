import { Component } from '@angular/core';
import { AuthService } from './auth.services'

@Component({
  selector: 'nav',
  template: `
  <mat-toolbar>
    <button *ngIf="auth.isAuthenticated" mat-button routerLink="/"> Add Quiz </button>
    <button *ngIf="auth.isAuthenticated" mat-button routerLink="/favorite"> My Favorite </button>
    <button mat-button routerLink="/play">Play </button>
    <span style="flex: 1 1 auto;">
    <button *ngIf="!auth.isAuthenticated" mat-button routerLink="/register"> Register</button>
    <button *ngIf="!auth.isAuthenticated" mat-button routerLink="/login"> Login</button>
    <button *ngIf="auth.isAuthenticated" mat-button (click)="auth.logout()"> Logout</button>
    </span>
  <mat-toolbar>
  `
})
export class NavComponent {
  constructor(public auth: AuthService) {}
}
