export class UserModel{
    id? : number;
    userName = "";
    email = "";
    password = "";
    isActive? : boolean;
    dc: Date = new Date();
    dd: Date = new Date();
    lu: Date = new Date();
}