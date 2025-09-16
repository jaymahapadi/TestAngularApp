import { Component, OnDestroy } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { FormsModule } from '@angular/forms'; 
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-category',
  standalone: false,
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css'
})
export class AddCategoryComponent implements OnDestroy {
  model: AddCategoryRequest;

  private addCategorySubscription? : Subscription;

  constructor(private categoryService:CategoryService) {
    this.model = {
      name: '',
      urlHandle: ''
    };
  }
  onFormSubmit() {
    this.addCategorySubscription= this.categoryService.addCategory(this.model)
      .subscribe({
        next: (response) => {
          console.log("Category added successfully");
        },
        error: (error) => {
          console.log("Error adding category:", error);
        }

      });
  }

  
  ngOnDestroy(): void {
    this.addCategorySubscription?.unsubscribe();
  }

}
