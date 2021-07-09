export class CommentModel {
    constructor(
    public id: number,
    public rating: number,
    public userFirstName: string,
    public userLastName: string,
    public commentBody: string,
    public isApproved: boolean,
    ){}
}