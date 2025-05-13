export class ChangePasswordRequest {
  private password: string;
  private newPassword: string;
  constructor(password:string, newPassword: string) {
    this.password = password;
    this.newPassword = newPassword;
  }
  get Password(){
    return this.password;
  }
  set Password(password:string){
    this.password = password;
  }
  get NewPassword(){
    return this.newPassword;
  }
  set NewPassword(newPassword:string){
    this.newPassword = newPassword;
  }
}
