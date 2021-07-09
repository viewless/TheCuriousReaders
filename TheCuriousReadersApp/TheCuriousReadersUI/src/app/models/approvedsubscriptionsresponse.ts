import { ApprovedSubscriptionModel } from "./approvedsubscriptionmodel";

export class ApprovedSubscriptionsResponse {
    constructor(
    public id : number,
    public bookTitle: string,
    public remainingDays: number,
    public returnBookDate: string,
    public isAdditionalTimeRequested :boolean,
    public approvedSubscriptions: ApprovedSubscriptionModel[],
    public totalCount: number,
    public requestedDays: number
    ){}
}