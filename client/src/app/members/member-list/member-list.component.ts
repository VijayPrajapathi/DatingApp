import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { MembersCardComponent } from "../members-card/members-card.component";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { AccountService } from '../../_services/account.service';
import { UserParams } from '../../_models/userParams';
import { FormsModule } from '@angular/forms';
import { ButtonsModule } from 'ngx-bootstrap/buttons';

@Component({
  selector: 'app-member-list',
  imports: [MembersCardComponent,PaginationModule,FormsModule,ButtonsModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  membersService = inject(MembersService);
  genderList = [{value:'male',display:'Males'},{value:'female',display:'females'}]

  ngOnInit(): void {
    //if (this.membersService.paginatedResult()) 
      this.loadmembers();
  }

  loadmembers() {
    this.membersService.getMembers();
  }
  
  resetFilters(){
    this.membersService.resetUserParams();
    this.loadmembers();
  }

  pageChanged(event:any){
    if(this.membersService.userParams().pageNumber != event.page){
      this.membersService.userParams().pageNumber = event.page;
      this.loadmembers;
    }
  }
}
