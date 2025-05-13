export class ResetPasswordViaCodeRequest{
  private email: string;
  private code: string;
  private newPassword: string;
  constructor(email:string, code: string, newPassword: string) {
    this.email = email;
    this.code = code;
    this.newPassword = newPassword;
  }
  get Email(){
    return this.email;
  }
  set Email(email:string){
    this.email = email;
  }
  get Code(){
    return this.code;
  }
  set Code(code:string){
    this.code = code;
  }
  get NewPassword(){
    return this.newPassword;
  }
  set NewPassword(newPassword:string){
    this.newPassword = newPassword;
  }
}
