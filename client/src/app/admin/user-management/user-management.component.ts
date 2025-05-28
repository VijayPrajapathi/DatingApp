import { Component, inject, OnInit } from '@angular/core';
import { AdminService } from '../../_services/admin.service';
import { User } from '../../_models/user';
import { CommonModule } from '@angular/common';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { RolesModalComponent } from '../../_modal/roles-modal/roles-modal.component';

@Component({
  selector: 'app-user-management',
  imports: [CommonModule],
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.css'
})
export class UserManagementComponent implements OnInit {

  private adminService = inject(AdminService);
  private modalService = inject(BsModalService);
  users: User[] = [];
  bfModalRef: BsModalRef<RolesModalComponent> = new BsModalRef<RolesModalComponent>();

  ngOnInit(): void {
    this.getUsersWithRoles();
  }

  openRolesModal(user: User){
    const initialstate : ModalOptions ={
      class: 'modal-lg',
      initialState : {
        title : 'User Roles',
        username: user.username,
        selectedRoles: [...user.roles],
        availableRoles: ['Admin','Moderator','Member'],
        users: this.users,
        rolesUpdated: false
      }
    }
    this.bfModalRef = this.modalService.show(RolesModalComponent,initialstate);
    this.bfModalRef.onHide?.subscribe({
      next: () => {
        if (this.bfModalRef.content && this.bfModalRef.content.rolesUpdated){
          const selectedRoles = this.bfModalRef.content.selectedRoles;
          this.adminService.updateUseRoles(user.username,selectedRoles).subscribe({
            next: roles => user.roles = roles
          })
        }
      }
    })
  }

  getUsersWithRoles() {
    this.adminService.getUserWithRoles().subscribe({
      next: users => {
        // Ensure users is always an array
        this.users = Array.isArray(users) ? users : [];
      }
    })
  }
  
}