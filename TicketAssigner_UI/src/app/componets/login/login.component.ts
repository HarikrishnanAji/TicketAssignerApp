import { Component, Injectable } from '@angular/core';
import { UserModel } from '../models/UserModel';
import { EncryptionService } from '../services/encryption.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
@Injectable({
  providedIn: 'root'
})
export class LoginComponent {
  user = new UserModel();
  constructor(private authService:EncryptionService,private router: Router){}
  onLogin(user:UserModel){
        console.log("login");
    this.authService.login(user).subscribe((token:string)=>{
      localStorage.setItem('authToken',token);
    });
    this.router.navigate(['/dashboard']);
  }
  getMe(){
    this.authService.getMe().subscribe((name:string)=>{
      console.log(name);
    })
  }

}
