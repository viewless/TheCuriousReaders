export class ApprovedSubscriptionModel {
    constructor(
    public bookTitle: string,
    public remainingDays: number,
    public returnBookDate: string,
    public isAdditionalTimeRequested: boolean,
    public id: number,
    public requestedDays: number
    ){}
}