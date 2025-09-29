import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';

@Injectable({
  providedIn: 'root'
})
export class BlogpostServiceService {

  constructor(private httpClient:HttpClient  ) { }

  addBlogPost(postData: AddBlogPost): Observable<BlogPost> {
    // Logic to add a blog post
    return this.httpClient.post<BlogPost>(`${environment.apiBaseUrl}/api/BlogPosts`, postData);
  }

  getAllBlogPosts(): Observable<BlogPost[]> {
    return this.httpClient.get<BlogPost[]>(`${environment.apiBaseUrl}/api/BlogPosts`);
  }

}
