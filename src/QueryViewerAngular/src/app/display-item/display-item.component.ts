import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap, tap } from 'rxjs/operators';
import { SearchDataService } from '../services/search-data.service';
import { FieldDescription, SearchField } from '../services/FieldDescription';
import { MetadataService } from '../services/metadata.service';

@Component({
  selector: 'app-display-item',
  templateUrl: './display-item.component.html',
  styleUrls: ['./display-item.component.css']
})
export class DisplayItemComponent implements OnInit {

  public queries: string[]=[];
  public item: string=''; 
  public FieldsForQuery = new Map<string,FieldDescription[]>();
  constructor(private router: Router, private route: ActivatedRoute, public ms: MetadataService, private searchData: SearchDataService) { 

    this.route.params
      .pipe(
        tap(it=> this.item=(it["item"] as string)),
        // tap(it=>console.log(it))
      )
    .subscribe(it=> this.ms.GetQueries(this.item));

  }

  existsCriteria(itQ: string):boolean{
    return ((this.FieldsForQuery.get(itQ)?.filter(it=>it.defaultValue.criteria !=0).length??0)>0);
  }
  DisplayQuery(query:string){
    const fdArr:FieldDescription[]=[];
    this.FieldsForQuery.get(query)?.forEach(el=>{
      if(el.defaultValue.criteria != 0 && el.defaultValue.value.trim().length>0){
        var crit = el.defaultValue.criteria.toString();
        el.defaultValue.criteriaString = el.possibleSearches.filter(it=>it.key == crit)[0].value;
        fdArr.push(el);
      }
    }); 
    var searches= fdArr.map(it=> {
      
        var r= new SearchField(it.defaultValue);
        return r;
      });
    var key  = this.searchData.addSearch(this.item, query,searches).subscribe(key=>
    this.router.navigateByUrl('/DisplayQuery/'+ this.item+'/'+ query+"/"+ key)
    );
  }
  ngOnInit(): void {


    this.ms.exposeItemWithQuery().subscribe(it=>
      
      {
        if(it.item == this.item){
          it.queries.forEach(element => {
              this.FieldsForQuery.set(element,[]);   
            
            this.ms.GetFields(this.item,element);
          });
          this.queries=it.queries;
        }
      }
      );
    this.ms.exposeFields().subscribe(it=>{
      it.forEach(el=>{
          if(el.itemName == this.item){
            var arr =this.FieldsForQuery.get(el.queryName);
            if(arr?.filter(it=>it.fieldName == el.fieldName).length == 0)            
                  arr?.push(el);   
            
            this.ms.GetSearch(el.fieldType).subscribe(
              searches=>el.possibleSearches = searches
            )       
          }

      });
    })
    
  }

}
