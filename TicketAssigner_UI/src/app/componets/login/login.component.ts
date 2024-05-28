import { Component, Injectable } from '@angular/core';
import { UserModel } from '../models/UserModel';
import { EncryptionService } from '../services/encryption.service';

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
  constructor(private authService:EncryptionService){}
  onLogin(user:UserModel){
        console.log("login");
    this.authService.login(user).subscribe((token:string)=>{
      localStorage.setItem('authToken',token);
    });
  }
  getMe(){
    this.authService.getMe().subscribe((name:string)=>{
      console.log(name);
    })
  }

}
