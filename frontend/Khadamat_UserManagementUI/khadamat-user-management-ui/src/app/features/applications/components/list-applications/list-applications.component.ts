import {Component, inject} from '@angular/core';
import {ApplicationsService} from '../../services/applications.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {ApplicationResponse} from '../../models/ApplicationResponse';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {RejectApplicationDialogComponent} from '../reject-application-dialog/reject-application-dialog.component';
import {ApproveApplicationDialogComponent} from '../approve-application-dialog/approve-application-dialog.component';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-list-applications',
  imports: [MatIconModule, MatDividerModule, MatProgressSpinnerModule, MatTableModule, MatButtonModule, MatTableModule],
  templateUrl: './list-applications.component.html',
  styleUrl: './list-applications.component.scss'
})
export class ListApplicationsComponent {
  private readonly applicationService = inject(ApplicationsService);
  private readonly snackBar = inject(MatSnackBar);
  displayedColumns: string[] = ['id', 'username', 'email', 'status', 'actions'];
  applications: ApplicationResponse[] = [];
  dialog = inject(MatDialog);

  ngOnInit(): void {
    this.loadApplications();
  }

  loadApplications(): void {
    this.applicationService.getAllApplications().subscribe({
      next: (apps) => {
        this.applications = apps;
      },
      error: (err) => {
        this.snackBar.open('Failed to load applications', 'Close', {
          duration: 3000,
          panelClass: ['error-snackbar']
        });
        console.error('Error loading applications:', err);
      }
    });
  }

  openApproveDialog(application: ApplicationResponse): void {
    const dialogRef = this.dialog.open(ApproveApplicationDialogComponent, {
      width: '500px',
      data: {username: application.username, email: application.email}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result?.action === 'approve') {
        this.applicationService.approveApplication(application.id, result.roleValue)
          .subscribe({
            next: () => {
              this.snackBar.open('Application approved successfully', 'Close', {duration: 3000});
              setTimeout(() => {
                this.loadApplications(); // Reload immediately
              }, 5000)
            },
            error: (err) => {
              this.snackBar.open('Failed to approve application', 'Close', {
                duration: 5000,
                panelClass: ['error-snackbar']
              });
              console.error('Error approving application:', err);
            }
          });
      }
    });
  }

  openRejectDialog(application: ApplicationResponse): void {
    const dialogRef = this.dialog.open(RejectApplicationDialogComponent, {
      width: '500px',
      data: {username: application.username, email: application.email}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result?.action === 'reject') {
        this.applicationService.rejectApplication(application.id, result.reason)
          .subscribe({
            next: () => {
              this.snackBar.open('Application rejected successfully', 'Close', {duration: 3000});
              setTimeout(() => {
                this.loadApplications(); // Reload immediately
              }, 5000)
            },
            error: (err) => {
              this.snackBar.open('Failed to reject application', 'Close', {
                duration: 5000,
                panelClass: ['error-snackbar']
              });
              console.error('Error rejecting application:', err);
            }
          });
      }
    });
  }


}
