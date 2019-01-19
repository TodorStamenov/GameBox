export class UserModel {
  constructor(
    public username: string,
    public isLocked: boolean,
    public isAdmin: boolean
  ) { }
}
