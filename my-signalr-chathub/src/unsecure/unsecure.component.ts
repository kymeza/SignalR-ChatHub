import { Component } from '@angular/core';
import { PowerShellService } from './unsecure.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { productDto } from 'src/dtos/supertienda/productDto';
import { HttpEventType, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-command-runner',
  standalone: true,
  templateUrl: './unsecure.component.html',
  styleUrls: ['./unsecure.component.css'],
  imports: [FormsModule, CommonModule]
})
export class CommandRunnerComponent {
    command: string = '';
    output: string = '';
    productId: string = '';
    products: productDto[] = [];
    fileToUpload: File | null = null;
    fileName: string = '';
    uploadStatus: string = '';
    username: string = '';
    password: string = '';
    userStatus: string = '';
        
    constructor(private powerShellService: PowerShellService) {}

    runCommand(command: string) {
      if (command) {
        this.powerShellService.runCommand(command).subscribe({
          next: (data) => this.output = data,
          error: (error) => {
            // If the error has a status code, it could be a more specific problem.
            if (error.status) {
              this.output = `Error ${error.status}: ${error.statusText}`;
            } else {
              this.output = 'An unexpected error occurred.';
            }
          }
        });
      } else {
        this.output = 'Please enter a command.';
      }
    }

    onFileSelected(event: any) {
      this.fileToUpload = event.target.files[0];
    }
      
    uploadFile() {
      if (!this.fileToUpload) {
        this.uploadStatus = 'Please select a file to upload.';
        return;
      }
  
      this.powerShellService.uploadFile(this.fileToUpload, this.fileName).subscribe({
        next: (event) => {
          // Handle a HttpEventType.UploadProgress event (optional)
          if (event.type === HttpEventType.UploadProgress && event.total) {
            const progress = Math.round(100 * event.loaded / event.total);
            this.uploadStatus = `File is ${progress}% uploaded.`;
          } else if (event instanceof HttpResponse) {
            this.uploadStatus = 'File uploaded successfully!';
          }
        },
        error: (error) => {
          this.uploadStatus = `Error: ${error.statusText} (${error.status})`;
        }
      });
    }
      
      getProducts() {
        this.powerShellService.getProducts().subscribe({
          next: (data) => this.products = data,
          error: (error) => console.error('Error: ', error)
        });
      }
      
      getProduct() {
        this.powerShellService.getProduct(this.productId).subscribe({
          next: (data) => this.products = data, // Assuming you want to display this as a single-item array
          error: (error) => console.error('Error: ', error)
        });
      }

      checkUser() {
        this.powerShellService.checkUser(this.username, this.password).subscribe({
          next: (response) => this.userStatus = 'User found and validated',
          error: (error) => {
            if (error.status === 404) {
              this.userStatus = 'User not found';
            } else if (error.status === 401) {
              this.userStatus = 'User found but not validated';
            } else {
              this.userStatus = `Error: ${error.statusText} (${error.status})`;
            }
          }
        });
      }
}
