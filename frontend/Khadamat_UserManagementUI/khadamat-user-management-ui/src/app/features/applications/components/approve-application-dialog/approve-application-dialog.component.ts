import {Component, Inject, inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {UserRole} from '../../models/UserRole';
import {MatCardModule} from '@angular/material/card';
import {MatDividerModule} from '@angular/material/divider';
import {FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {DecimalPipe, JsonPipe} from '@angular/common';

@Component({
  selector: 'app-approve-application-dialog',
  imports: [MatCardModule, MatDividerModule, FormsModule, MatButtonModule, MatCheckboxModule, JsonPipe, DecimalPipe, ReactiveFormsModule],
  templateUrl: './approve-application-dialog.component.html',
  styleUrl: './approve-application-dialog.component.scss'
})
export class ApproveApplicationDialogComponent {
  roleOptions = [
    {value: UserRole.Admin, name: 'Administrator', description: 'Full system access'},
    {value: UserRole.ListUsers, name: 'List Users', description: 'View user accounts'},
    {value: UserRole.AddUser, name: 'Add Users', description: 'Create new users'},
    {value: UserRole.UpdateUser, name: 'Update Users', description: 'Modify user accounts'},
    {value: UserRole.DeleteUser, name: 'Delete Users', description: 'Remove user accounts'},
    {value: UserRole.ManageUsers, name: 'Manage Users', description: 'Full user management'}
  ];

  roleForm!: FormGroup;
  showDebugInfo = true; // Set to true for debugging
  private adminIndex: number;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<ApproveApplicationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.adminIndex = this.roleOptions.findIndex(role => role.value === UserRole.Admin);
  }

  ngOnInit() {
    this.initForm();

  }

  getControl(index: number): FormControl<boolean> {
    return this.rolesArray.at(index) as FormControl<boolean>;
  }

  initForm() {
    const formControls = this.roleOptions.map(role => {
      return this.fb.control(false);
    });

    this.roleForm = this.fb.group({
      roles: this.fb.array(formControls)
    });

    this.roleForm.get('roles')?.valueChanges.subscribe(values => {
      this.handleRoleChanges(values);
    });
  }

  get rolesArray() {
    return this.roleForm.get('roles') as FormArray;
  }

  get selectedRoles(): UserRole[] {
    return this.roleOptions
      .filter((_, index) => this.rolesArray.controls[index].value)
      .map(role => role.value);
  }

  get selectedRoleValue(): number {
    // If Admin is selected, return -1
    if (this.selectedRoles.includes(UserRole.Admin)) {
      return UserRole.Admin; // -1
    }

    // Calculate bitwise OR of all selected roles
    return this.selectedRoles.reduce((combinedValue, role) => {
      return combinedValue | role;
    }, 0);
  }

  // Add this property to your component class
  private previousAdminState: boolean = false;

  handleRoleChanges(values: boolean[]) {
    //debugger
    const isAdminChecked = values[this.adminIndex];
    const anyOtherChecked = values.some((val, index) => index !== this.adminIndex && val);
    const allNonAdminSelected = values.every((value, index) => index === this.adminIndex ? true : value);

    // Existing logic: If all non-admin roles are selected → auto-check Admin & uncheck others
    if (allNonAdminSelected && this.roleOptions.length > 1) {
      this.rolesArray.controls.forEach((control, index) => {
        control.setValue(index === this.adminIndex, {emitEvent: false});
      });
      this.previousAdminState = true;
      this.rolesArray.updateValueAndValidity();
      return;
    }

    // Case 1: Admin was previously checked and now another option is checked
    if (this.previousAdminState && anyOtherChecked) {
      this.getControl(this.adminIndex).setValue(false, {emitEvent: false});
      this.previousAdminState = false;
      return;
    }

    // Case 2: Admin is newly checked while other options were checked
    if (isAdminChecked && !this.previousAdminState && anyOtherChecked) {
      this.rolesArray.controls.forEach((control, index) => {
        if (index !== this.adminIndex) {
          control.setValue(false, {emitEvent: false});
        }
      });
      this.previousAdminState = true;
      return;
    }

    // Case 3: Admin is checked alone
    if (isAdminChecked) {
      this.rolesArray.controls.forEach((control, index) => {
        if (index !== this.adminIndex) {
          control.setValue(false, {emitEvent: false});
        }
      });
      this.previousAdminState = true;
      return;
    }

    // Update previous state if admin was unchecked
    if (!isAdminChecked) {
      this.previousAdminState = false;
    }
  }

  setSelectedRoles(roles: UserRole[]) {
    this.rolesArray.controls.forEach((control, index) => {
      control.setValue(roles.includes(this.roleOptions[index].value), {emitEvent: false});
    });
    this.rolesArray.updateValueAndValidity();
  }

  onApprove() {
    this.dialogRef.close({
      action: 'approve',
      roles: this.selectedRoles,
      roleValue: this.selectedRoleValue
    });
  }

  onCancel() {
    console.log(this.selectedRoleValue)
    this.dialogRef.close({action: 'cancel'});
  }

}

