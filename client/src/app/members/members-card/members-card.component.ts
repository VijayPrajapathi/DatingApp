import { Component, input, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Member } from '../../_models/member';

@Component({
  selector: 'app-members-card',
  imports: [RouterLink],
  templateUrl: './members-card.component.html',
  styleUrl: './members-card.component.css'
})
export class MembersCardComponent {
  member = input.required<Member>();
}
