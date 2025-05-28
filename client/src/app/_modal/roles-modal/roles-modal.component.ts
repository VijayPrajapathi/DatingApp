import { Component, inject } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-roles-modal',
  imports: [],
  templateUrl: './roles-modal.component.html',
  styleUrl: './roles-modal.component.css'
})
export class RolesModalComponent {
bsModalRef = inject(BsModalRef);
username = '';
title = '';
availableRoles : string[] = [];
selectedRoles: string[] =[];
rolesUpdated = false;

updateChecked(checkedValues: string){
  if(this.selectedRoles.includes(checkedValues)){
    this.selectedRoles = this.selectedRoles.filter(r => r !== checkedValues)
  }
  else {
    this.selectedRoles.push(checkedValues);
  }
}

onSelectRoles(){
  this.rolesUpdated = true;
  this.bsModalRef.hide();
}


}
