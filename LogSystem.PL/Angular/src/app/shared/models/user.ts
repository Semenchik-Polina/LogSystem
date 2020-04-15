export class User {
  constructor(
    public UserID?: number,
    public UserName?: string,
    public FirstName?: string,
    public LastName?: string,
    public Password?: string,
    public ConfirmPassword?: string,
    public Email?: string,
    public Type?: number,
    public RegistrationDate?: Date
  ) {}
}
