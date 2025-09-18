import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-category-list',
  standalone: false,
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent implements OnInit{

  categories$?:Observable<Category[]>;
  constructor(private categoryService:CategoryService)
  {

  }
  ngOnInit(): void {
    console.log(this.categoryService.getAllCategories());
    this.categories$=this.categoryService.getAllCategories();
    
  }
}
