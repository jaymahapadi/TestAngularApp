import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogpostServiceService } from '../services/blogpost-service.service';
import { AddBlogPost } from '../models/add-blog-post.model';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
@Component({
  selector: 'app-add-blogpost',
  standalone: false,
  templateUrl: './add-blogpost.component.html',
  styleUrl: './add-blogpost.component.css'
})
export class AddBlogpostComponent implements OnDestroy,OnInit {
  model:AddBlogPost;
 addBlospostSubscription?:Subscription
  constructor(private blogService: BlogpostServiceService
    ,private categoryService:CategoryService
    ,private router:Router
  ) { 
    this.model = {Title:''
      ,ShortDescription:''
      ,Content:''
      ,Author:''
      ,UrlHandle:''
      ,PublishedDate:new Date()
      ,FeaturedImageUrl:''
      ,IsVisible:true
      ,Categories:[]
    };
  }

  categories$?:Observable<Category[]>;

  ngOnInit(): void {
    this.categories$=this.categoryService.getAllCategories();
  }
  ngOnDestroy(): void {
    this.addBlospostSubscription?.unsubscribe();
  }
  
  onFormSubmit(){
    console.log(this.model);
    if(this.model){
      this.addBlospostSubscription= this.blogService.addBlogPost(this.model)
      .subscribe({
        next:(data)=>{
          console.log('Blog post added:', data);
          this.router.navigateByUrl('admin/blogposts');
        },
        error:(error)=>{
          console.error('Error adding blog post:', error);
        }
      });
    }
  }
}
