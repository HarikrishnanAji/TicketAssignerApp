import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserModel } from '../models/UserModel';
import { AppModule } from '../../app.module';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})

export class EncryptionService {
   AppUrl = 'https://localhost:44373/api/Auth/';
  constructor(private http: HttpClient) { }
    public register(user : UserModel): Observable<any>{
        console.log(user);
        return this.http.post<any>(
          this.AppUrl+'register',user
        )
    }
    public login(user : UserModel): Observable<string>{
      return this.http.post(
        this.AppUrl+'login',user,{
        responseType:'text',
    });
  }
    public getMe(): Observable<string>{
      return this.http.get<string>(this.AppUrl);
  }
}
