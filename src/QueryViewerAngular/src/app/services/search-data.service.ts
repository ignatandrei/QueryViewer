import { Injectable } from '@angular/core';
import { FieldDescription } from './FieldDescription';

@Injectable({
  providedIn: 'root'
})
export class SearchDataService {

  private searches=new Map<string,FieldDescription[]>();
  constructor() { }

  public addSearch(data:FieldDescription[] ){
    var l = this.searches.size;
    l++;
    const key= "key"+l;
    this.searches.set(key,data);
    return key;
  }
  public getSearch(key:string): FieldDescription[] | null{
    
    if(!this.searches.has(key))
      return null;

    return this.searches.get(key)!;
  }


}
