import { CommentModel } from "./commentmodel";

export class PaginatedNewCommentModel {
    constructor(
    public paginatedCommentResponses: CommentModel[],
    public totalCount: number
    ){}
}