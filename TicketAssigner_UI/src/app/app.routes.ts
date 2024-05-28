import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { LoginComponent } from './componets/login/login.component';
import { RegisterComponent } from './componets/register/register.component';

export const routes: Routes = [
    {path:"login",component:LoginComponent},
    {path:"register",component:RegisterComponent}
];
