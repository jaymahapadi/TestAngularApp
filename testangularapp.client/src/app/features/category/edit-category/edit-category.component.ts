import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscribable, Subscription } from 'rxjs';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { UpdateCategory } from '../models/update-category-request.model';

@Component({
  selector: 'app-edit-category',
  standalone: false,
  templateUrl: './edit-category.component.html',
  styleUrl: './edit-category.component.css'
})
export class EditCategoryComponent implements OnInit,OnDestroy{

  id:string |null=null;
  paramSubscription?:Subscription;
  editCategorySubscription?:Subscription;
  
  updateCategorySubscription?:Subscription;

  category?:Category;

  constructor (private route:ActivatedRoute,
    private categoryService:CategoryService,
  private router:Router)
  {

  }
  ngOnDestroy(): void {
    this.paramSubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
    this.updateCategorySubscription?.unsubscribe();
  }
  ngOnInit(): void {
    this.paramSubscription=this.route.paramMap.subscribe({
      next:(params)=>{
        this.id=params.get('id');
      }

    });
    
    if(this.id)
    {
      this.editCategorySubscription=this.categoryService.getCategoryById(this.id)
      .subscribe({
        next:(response)=> {
          this.category=response;
        }
      });
    }
  }

  onFormSubmit(): void
  {
    const updateCategory:UpdateCategory={
      name:this.category?.name ?? '',
      urlHandle:this.category?.urlHandle ?? ''
    }
    console.log(updateCategory);
    if(this.id)
    {
    this.updateCategorySubscription=this.categoryService.updateCategory(this.id,updateCategory)
    .subscribe({
      next:(response)=>{
        this.router.navigateByUrl('admin/categories');
      }
    });
  }
  }
  onDelete()
  {}
  

}
