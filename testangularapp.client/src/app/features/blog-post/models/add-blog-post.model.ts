import { Category } from "../../category/models/category.model";

export interface AddBlogPost{
    Title:string;
    ShortDescription:string;
    Content:string;
    UrlHandle:string;
    Author:string;
    FeaturedImageUrl:string;
    PublishedDate:Date;
    IsVisible:boolean;
    
  Categories: Category[];
}