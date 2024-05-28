import { Component,Injectable } from '@angular/core';
import { UserModel } from '../models/UserModel';
import { EncryptionService } from '../services/encryption.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
@Injectable({
  providedIn: 'root'
})
export class RegisterComponent {
  user = new UserModel();
  
  constructor(private authService:EncryptionService){}
  onRegister(user:UserModel){
    this.authService.register(user).subscribe(res=>{
      console.log(res);
    });
  }

}
