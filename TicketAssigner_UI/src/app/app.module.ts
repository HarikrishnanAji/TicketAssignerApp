import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { LoginComponent } from "./componets/login/login.component";
import { RegisterComponent } from "./componets/register/register.component";
import { AppComponent } from "./app.component";
import { HttpClientModule } from "@angular/common/http";
import { EncryptionService } from "./componets/services/encryption.service";
import { DashboardComponent } from "./componets/dashboard/dashboard.component";
import { HomeComponent } from "./componets/home/home.component";

@NgModule({
    declarations: [
        LoginComponent,
        RegisterComponent,
        DashboardComponent,
        HomeComponent
    ],
    imports: [
      
      BrowserModule,
      HttpClientModule,
      FormsModule,
      RouterModule
    ],
    providers: [EncryptionService],
    bootstrap: [AppModule]
  })
  export class AppModule { }