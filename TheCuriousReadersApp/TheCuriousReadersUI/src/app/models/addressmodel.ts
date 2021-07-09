export class AddressModel {
    constructor(
    public country: string,
    public city: string,
    public street: string,
    public streetNumber: string,
    public buildingNumber: string,
    public apartmentNumber: string,
    public additionalInfo: string
    ){}
}