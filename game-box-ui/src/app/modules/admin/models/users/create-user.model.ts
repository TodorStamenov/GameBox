export class CreateUserModel {
  constructor(
    public username: string,
    public password: string,
    public repeatPassword: string
  ) { }
}
