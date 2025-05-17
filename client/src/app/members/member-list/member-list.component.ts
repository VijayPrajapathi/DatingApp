import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { MembersCardComponent } from "../members-card/members-card.component";
import { PaginationModule } from 'ngx-bootstrap/pagination';



@Component({
  selector: 'app-member-list',
  imports: [MembersCardComponent,PaginationModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  membersService = inject(MembersService);
  pageNumber = 1;
  pageSize = 5;
  ngOnInit(): void {
    if (this.membersService.paginatedResult()) this.loadmembers();
  }

  loadmembers() {
    this.membersService.getMembers(this.pageNumber,this.pageSize);
  }


  pageChanged(event:any){
    if(this.pageNumber != event.page){
      this.pageNumber = event.page;
      this.loadmembers;
    }
  }
}
