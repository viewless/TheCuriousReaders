import {AddressModel} from "./addressmodel"

export class UserRegisterModel {
    constructor(
    public firstName: string,
    public lastName: string,
    public emailAddress: string,
    public phoneNumber: string,
    public password: string,
    public confirmPassword: string,
    public address: AddressModel
    ){}
  }