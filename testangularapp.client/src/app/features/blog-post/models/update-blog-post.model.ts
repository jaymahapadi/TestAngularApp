export interface UpdateBlogPost
{
Title:string;
    ShortDescription:string;
    Content:string;
    UrlHandle:string;
    Author:string;
    FeaturedImageUrl:string;
    PublishedDate:Date;
    IsVisible:boolean;
    
  Categories: string[];
}