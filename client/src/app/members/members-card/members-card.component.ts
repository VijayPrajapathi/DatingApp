import { Component, computed, inject, input, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Member } from '../../_models/member';
import { LikesService } from '../../_services/likes.service';

@Component({
  selector: 'app-members-card',
  imports: [RouterLink],
  templateUrl: './members-card.component.html',
  styleUrl: './members-card.component.css'
})
export class MembersCardComponent {
  private likeService = inject(LikesService);
  member = input.required<Member>();
  hasLiked = computed(() => this.likeService.likeIds().includes(this.member().id));

  toggleLike() {
    this.likeService.toggleLike(this.member().id).subscribe({
      next: () => {
       if(this.hasLiked()){
        this.likeService.likeIds.update(i => i.filter(x => x !== this.member().id));
       }
       else{
        this.likeService.likeIds.update(i => [...i, this.member().id]);
       }
      }
    })
  }
}
