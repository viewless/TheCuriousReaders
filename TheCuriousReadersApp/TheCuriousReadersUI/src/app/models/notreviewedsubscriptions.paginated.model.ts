import { NotReviewedSubscriptionsModel } from "./notreviewed.subscriptions.model";

export class NotReviewedSubscriptionsPaginatedModel {
    constructor(
    public nonReviewedSubscriptions: NotReviewedSubscriptionsModel[],
    public totalCount: number
    ){}
}