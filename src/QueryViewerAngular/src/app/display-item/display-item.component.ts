import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { delay, switchMap, switchMapTo, tap } from 'rxjs/operators';
import { SearchDataService } from '../services/search-data.service';
import { FieldDescription, SearchField } from '../services/FieldDescription';
import { MetadataService } from '../services/metadata.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { pipe } from 'rxjs';
@UntilDestroy()
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


  }
  GetSql(item: string, query:string){
    this.ms.GetSql(item,query)
      .pipe(
        tap(it=>window.alert(it)),
        untilDestroyed(this)
      )
        .subscribe();
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
    var key  = this.searchData.addSearch(this.item, query,searches)
    .pipe(      
        untilDestroyed(this)
    )
    .subscribe(key=>
    this.router.navigateByUrl('/DisplayQuery/'+ this.item+'/'+ query+"/"+ key)
    );
  }
  ngOnInit(): void {


    this.route.params
    .pipe(
      delay(1000),
      tap(it=> this.item=(it["item"] as string)),
      // tap(it=>console.log(it))
      switchMap(it=>this.ms.exposeItemWithQuery(this.item)),      
      untilDestroyed(this)
    )
    .subscribe(it=>
      
      {
        if(it.item == this.item){
          it.queries.forEach(element => {
              this.FieldsForQuery.set(element,[]);   
            
            this.ms.exposeFields(this.item,element) 
            .pipe(untilDestroyed(this))
            .subscribe(it=>{
              it.forEach(el=>{
                  if(el.itemName == this.item){
                    var arr =this.FieldsForQuery.get(el.queryName);
                    if(arr?.filter(it=>it.fieldName == el.fieldName).length == 0)            
                          arr?.push(el);   
                    
                    this.ms.GetSearch(el.fieldType)
                    .pipe(untilDestroyed(this))
                    .subscribe(
                      searches=>el.possibleSearches = searches
                    )       
                  }
        
              });
            })
            
            
            
            ;
          });
          this.queries=it.queries;
        }
      }
      );
    
  }

}
