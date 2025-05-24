import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { LikesService } from '../_services/likes.service';
import { FormsModule } from '@angular/forms';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { MembersCardComponent } from '../members/members-card/members-card.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-lists',
  imports: [FormsModule, ButtonsModule, MembersCardComponent, PaginationModule],
  templateUrl: './lists.component.html',
  styleUrl: './lists.component.css'
})
export class ListsComponent implements OnInit,OnDestroy {
  

  likeService = inject(LikesService);

  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;
  ngOnInit(): void {
    this.loadLikes();
  }

  getTitle() {
    switch (this.predicate) {
      case 'liked': return 'People you liked';
      case 'likedBy': return 'People who liked you';
      default: return 'Mutual';
    }
  }

  loadLikes() {
    this.likeService.getLikes(this.predicate, this.pageNumber, this.pageSize);
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadLikes();
    }
  }

  ngOnDestroy(): void {
    this.likeService.pagginatedResult.set(null)
  }
}
