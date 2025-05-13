export class CreateNewRegisterApplicationRequest {
  username: string;
  password: string;
  email: string;

  constructor(username: string, password: string, email: string) {
    this.username = username;
    this.password = password;
    this.email = email;
  }

  get Username() {
    return this.username;
  }

  get Password() {
    return this.password;
  }

  get Email() {
    return this.email;
  }

  set Email(email: string) {
    this.email = email;
  }

  set Password(password: string) {
    this.password = password;
  }

  set Username(username: string) {
    this.username = username;
  }

}
