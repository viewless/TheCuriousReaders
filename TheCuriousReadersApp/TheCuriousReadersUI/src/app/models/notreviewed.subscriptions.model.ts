export class NotReviewedSubscriptionsModel {
    constructor(
    public id: number,
    public firstName: string,
    public lastName: string,
    public bookTitle: string,
    public requestedSubscriptionDays: number
    ){}
}