import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogpostServiceService } from '../services/blogpost-service.service';
import { Observable, Subscription } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';

@Component({
  selector: 'app-blogpost-list',
  standalone: false,
  templateUrl: './blogpost-list.component.html',
  styleUrl: './blogpost-list.component.css'
})
export class BlogpostListComponent implements OnInit {

  blogPosts$?:Observable<BlogPost[]>;
  constructor(private blogPostService:BlogpostServiceService
    
  ) { }

  ngOnInit(): void {
    this.blogPosts$=this.blogPostService.getAllBlogPosts();
  }

}
