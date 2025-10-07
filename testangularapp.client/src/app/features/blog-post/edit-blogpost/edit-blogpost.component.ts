import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogpostServiceService } from '../services/blogpost-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPost } from '../models/blog-post.model';
import { Observable, Subscription } from 'rxjs';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { UpdateBlogPost } from '../models/update-blog-post.model';

@Component({
  selector: 'app-edit-blogpost',
  standalone: false,
  templateUrl: './edit-blogpost.component.html',
  styleUrl: './edit-blogpost.component.css'
})
export class EditBlogpostComponent implements OnInit,OnDestroy {
  id:string |null=null;
  paramSubscription?:Subscription;
  editBlogPostSubscription?:Subscription;
  updateBlogPostSubscription?:Subscription;
  deleteBlogPostSubscription?:Subscription;
  model?:BlogPost;
  categories$?:Observable<Category[]>;
  selectedCategories?:string[];
  isImageSelectorVisible: boolean = false;

  constructor(private blogPostService :BlogpostServiceService
    ,private route:ActivatedRoute,
    private router:Router
    ,private categoryService:CategoryService
  ) { }
  ngOnDestroy(): void {
    this.paramSubscription?.unsubscribe();
    this.editBlogPostSubscription?.unsubscribe();
    this.updateBlogPostSubscription?.unsubscribe();
    this.deleteBlogPostSubscription?.unsubscribe();
  }

  ngOnInit(): void {

    this.categories$=this.categoryService.getAllCategories();

    this.paramSubscription=this.route.paramMap.subscribe({
      next:(params)=>{
        this.id=params.get('id');
      }
    });
    
    if(this.id)
    {
      this.blogPostService.getBlogPostById(this.id)
      .subscribe({
        next:(response)=> {
          this.model=response;
          this.selectedCategories=response.categories.map(c=>c.id);
        }
      });
    }
  }

  onFormSubmit(): void
  {
    // Logic to handle form submission and update the blog post
    if (this.model && this.id) {
      var updateBlogPost: UpdateBlogPost = {
        Author: this.model.author,
        Content: this.model.content,
        ShortDescription: this.model.shortDescription,
        FeaturedImageUrl: this.model.featuredImageUrl,
        IsVisible: this.model.isVisible,
        PublishedDate: this.model.publishedDate,
        Title: this.model.title,
        UrlHandle: this.model.urlHandle,
        Categories: this.selectedCategories ?? []
      };

      this.updateBlogPostSubscription = this.blogPostService.updateBlogPost(this.id, updateBlogPost)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/blogposts');
        }
      });
    }
  }

  onDelete()
  {
    if(this.id)
    {
      this.deleteBlogPostSubscription=this.blogPostService.deleteBlogPost(this.id)
      .subscribe({
        next:(response)=>{
          this.router.navigateByUrl('/admin/blogposts');
        }
      });
    }
  }

  openImageSelector() {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector() {
    this.isImageSelectorVisible = false;
  }
}
