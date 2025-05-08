import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { MembersCardComponent } from "../members-card/members-card.component";

@Component({
  selector: 'app-member-list',
  imports: [MembersCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  private membersService = inject(MembersService);
members: Member[] = [];
ngOnInit(): void {
  this.loadmembers();
}

loadmembers(){
  this.membersService.getMembers().subscribe({
    next : members => this.members = members
  })
}

}
