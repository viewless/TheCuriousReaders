import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal, NgbRatingConfig } from '@ng-bootstrap/ng-bootstrap';
import { switchMap } from 'rxjs/operators';
import { paginationComments } from 'src/app/constants/paginationcomments';
import { routePaths } from 'src/app/constants/routes';
import { Errors } from 'src/app/enums/errors';
import { BookData } from 'src/app/models/book-data';
import { CommentModel } from 'src/app/models/commentmodel';
import { AuthService } from 'src/app/services/auth.service';
import { BookService } from 'src/app/services/book.service';
import { SubscriptionService } from 'src/app/services/subscription.service';
import { NgxSpinnerService } from "ngx-spinner"; 

@Component({
  selector: 'book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.scss'],
  providers: [NgbRatingConfig]
})
export class BookDetailsComponent implements OnInit {
  bookId: string;
  bookData?: BookData;
  commentModel = new CommentModel(-1,0,"","", "",false);
  ratings = [1, 2, 3, 4, 5];
  totalItems: number = 0; 
  comments: CommentModel[] = [];
  pageNumber: number = paginationComments.pageNumber;
  pageSize: number = paginationComments.pageSize;
  copiesAmount: number = 1;
  cover = new File([], "");
  commentApproved = false;
  @ViewChild('modalDelete') modalDelete : TemplateRef<any> | undefined;

  errorMsg: string = '';
  successMsg: string = '';

  totalSubscribers : number = 0;

  currentRate : number = 0;

  constructor(private activatedRoute: ActivatedRoute, private bookService: BookService,
     private subscriptionService : SubscriptionService,  private authService: AuthService, 
     private router: Router, config: NgbRatingConfig, private SpinnerService: NgxSpinnerService,
     private modalService: NgbModal) {
    this.bookId = this.activatedRoute.snapshot.params.id;
    config.max = 5;
  }
  
  ngOnInit(): void {
    this.bookService.getBookData(this.bookId).subscribe((data: BookData) => {
      this.bookData = data;
    }, error => {
      
    })

    this.getCurrentPageData(this.pageNumber);

    this.subscriptionService.getTotalSubscribersForABook(parseInt(this.bookId))
      .subscribe(subscriptions => {
        this.totalSubscribers =  subscriptions.totalSubscribers;
      })

    if(this.isUserAdmin()){
        this.bookService.getComments(this.pageNumber, this.pageSize, this.bookId)
      .subscribe(newComments => {
     this.comments =  newComments.paginatedCommentResponses;
     this.totalItems = newComments.totalCount;
      });
    }else{
        this.bookService.getApprovedComments(this.pageNumber, this.pageSize, this.bookId)
      .subscribe(newComments => {
     this.comments =  newComments.paginatedCommentResponses;
     this.totalItems = newComments.totalCount;
      });
    }     
  };

  OnApproveComment(id: number, isApproved: boolean) {
    isApproved = true;

    this.bookService.reviewComment(id,isApproved).subscribe(c => {

    if(this.isUserAdmin()){
        this.bookService.getComments(this.pageNumber, this.pageSize, this.bookId)
      .subscribe(newComments => {
     this.successMsg = "Comment approved!"
     
     this.comments =  newComments.paginatedCommentResponses;
     this.totalItems = newComments.totalCount;
      });
    }else{
        this.bookService.getApprovedComments(this.pageNumber, this.pageSize, this.bookId)
      .subscribe(newComments => {
     this.comments =  newComments.paginatedCommentResponses;
     this.totalItems = newComments.totalCount;
      });
    }     
  });
  }

  onSelectedFile($event: any){
    this.cover = $event.target.files[0]  
  }

  editBook(){
    const trimmedName = this.bookData!.author.name.trim();
    const trimmedGenre = this.bookData!.genre.name.trim();
    this.bookData!.author.name = trimmedName;
    this.bookData!.genre.name = trimmedGenre;
    const data = {
      title: this.bookData!.title.trim(),
      description: this.bookData!.description.trim(),
      quantity: this.bookData!.quantity,
      author: this.bookData!.author,
      genre: this.bookData!.genre
    }

    const jsonRequest = JSON.stringify(data);
    
    if(this.cover.size > 0){
      this.SpinnerService.show();
    this.bookService.updateBook(jsonRequest, parseInt(this.bookId)).pipe(
      switchMap((res: BookData) => this.bookService.addCover(this.cover, this.activatedRoute.snapshot.params.id)))
    .subscribe(() => {
      this.SpinnerService.hide();
      this.successMsg = 'Book has successfully been updated.';
      this.bookService.getBookData(this.bookId)
      .subscribe((data: BookData) => {
         this.bookData = data;
       })   
    }, error => {
      this.SpinnerService.hide();
      if(error.status == Errors.Conflict){
        this.errorMsg = "This book title from this author already exists in the database.";
      }
      if(error.status == Errors.BadRequest){
        this.errorMsg = "Bad request. Check for required fields or invalid genre."
      }
    })
  }
  else{
    this.SpinnerService.show();
    this.bookService.updateBook(jsonRequest, parseInt(this.bookId))
    .subscribe(() => {
      this.successMsg = 'Book has successfully been updated.';
      this.SpinnerService.hide();
      this.bookService.getBookData(this.bookId)
      .subscribe((data: BookData) => {
         this.bookData = data;
       })   
    }, error => {
      this.SpinnerService.hide();
      if(error.status == Errors.Conflict){
        this.errorMsg = "This book title from this author already exists in the database.";
      }
      if(error.status == Errors.BadRequest){
        this.errorMsg = "Bad request. Check for required fields or invalid genre."
      }
    })
  }
  }

  OnSubscribe(){
    const data = {
      BookId: this.bookId,
      Copies: this.copiesAmount
      }
      this.SpinnerService.show();
    this.bookService.subscribeToBook(data)
    .subscribe(newBooks => {
      this.successMsg = 'You have been subscribed for that book but your subscription needs to be approved by an admin first.';
      this.bookService.getBookData(this.bookId).subscribe((data: BookData) => {
        this.bookData = data;
        this.SpinnerService.hide();
      })

      this.subscriptionService.getTotalSubscribersForABook(parseInt(this.bookId))
      .subscribe(subscriptions => {
        this.totalSubscribers =  subscriptions.totalSubscribers;
      })
    }, (error => {
      this.SpinnerService.hide();
      if(error.error.error === 'User already subscribed for that book.'){
        this.errorMsg = 'You are already subscribed for that book.';
      }
      else{
        this.errorMsg = error.error.error;
      }
    }));
  }

  clearErrorMessage(): void {
    this.errorMsg = '';
  }

  clearSuccessMessage(): void {
    this.successMsg = '';
  }

  OnComment(){
    const data = {
      rating: this.currentRate,
      commentBody: this.commentModel.commentBody
      }

      if (!data.rating) {
        this.errorMsg = "Rating is required!";
      }
      else if(!data.commentBody){
        this.errorMsg = "Comment is required!";
      }
      else if(!data.commentBody.trim()){
        this.errorMsg = "You cannot comment with whitespaces!";
      }
      else{
        this.SpinnerService.show();
        this.bookService.addReview(data, this.bookId)
        .subscribe(newReview => {
          this.SpinnerService.hide();
          this.successMsg = "Comment submitted but yet to be reviewed!";
          
          if(this.isUserAdmin()){
            this.bookService.getComments(this.pageNumber, this.pageSize, this.bookId)
          .subscribe(newComments => {
         this.comments =  newComments.paginatedCommentResponses;
         this.totalItems = newComments.totalCount;
        });
          }else{
            this.bookService.getApprovedComments(this.pageNumber, this.pageSize, this.bookId)
          .subscribe(newComments => {
         this.comments =  newComments.paginatedCommentResponses;
         this.totalItems = newComments.totalCount;
       });
        }     
        }, (error => {
         this.SpinnerService.hide();
         this.errorMsg = error.error.error;
        }));
      }
  }

  getCurrentPageData(pageNumber: number){
    if(this.isUserAdmin()){
      this.bookService.getComments(this.pageNumber, this.pageSize, this.bookId)
    .subscribe(newComments => {
   this.comments =  newComments.paginatedCommentResponses;
   this.totalItems = newComments.totalCount;
    });
    }else{
      this.bookService.getApprovedComments(this.pageNumber, this.pageSize, this.bookId)
    .subscribe(newComments => {
   this.comments =  newComments.paginatedCommentResponses;
   this.totalItems = newComments.totalCount;
    });
    }     
  }

  OnDeleteBook(){
    const modalResult = this.modalService.open(this.modalDelete).result;

    modalResult.then((value) => {
      if(!!value){
      this.SpinnerService.show();
      this.bookService.deleteBook(this.bookId)
    .subscribe(deleteBook => {
        this.SpinnerService.hide();
        this.router.navigate([routePaths.homePage]);
    }, error =>{
      this.SpinnerService.hide();
      this.errorMsg = error.error;
    });
  }
  })
  }

  OnDeleteComment(id: number){
    const modalResult = this.modalService.open(this.modalDelete).result;

    modalResult.then((value) => {
      if(!!value){
        this.bookService.deleteComment(id).subscribe(deleteComment => {   
            
        this.bookService.getComments(this.pageNumber, this.pageSize, this.bookId)
          .subscribe(newComments => {
         this.comments =  newComments.paginatedCommentResponses;
         this.totalItems = newComments.totalCount;
       })
      }, error =>{
        this.errorMsg = error.error;
      })
    }
    })
  }
  
  isUserAdmin() : boolean{
    return this.authService.isUserAdmin();
  }

  onModalAcceptedDeletion() : boolean{
    return true;
  }
  
  checkIfUserIsAuthenticated() : boolean{
    return this.authService.isUserAuthenticated();
  }

  changeBookAvailability(available: boolean){
    this.SpinnerService.show();
    this.bookService.changeBookAvailability(this.activatedRoute.snapshot.params.id, available)
    .subscribe(() => {
      this.SpinnerService.hide();
      if(available){
        this.successMsg = 'The book is now available for subscriptions.';
      }
      else{
        this.successMsg = 'The book is now unavailable for subscriptions.';
      }
      this.bookService.getBookData(this.bookId)
      .subscribe((data: BookData) => {
        this.bookData = data;
      })
    },() => {
      this.SpinnerService.hide();
      this.errorMsg = 'Something occured. Please try again.';
    })
  }
}
