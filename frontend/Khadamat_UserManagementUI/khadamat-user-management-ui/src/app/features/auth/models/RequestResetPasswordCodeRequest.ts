export class RequestResetPasswordCodeRequest {
  private email: string;
  constructor(email: string) {
    this.email = email;
  }
  get Email(){
    return this.email;
  }
  set Email(email:string) {
    this.email = email;
  }
}
