import { Component, OnInit } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { delay, tap } from 'rxjs/operators';
import { MetadataService } from '../services/metadata.service';
import { StoreItemsQuery, StoreItemsService } from '../services/store-items.service';

@UntilDestroy()
@Component({
  selector: 'app-all-items',
  templateUrl: './all-items.component.html',
  styleUrls: ['./all-items.component.css']
})
export class AllItemsComponent implements OnInit {

  public items : string[] = [];
  constructor(public ms:MetadataService  ,private query: StoreItemsQuery) { 

    
  }

  ngOnInit(): void {
    
    this.query.select(it=>it.items)
      .pipe(
        delay(1000),
        untilDestroyed(this),                  
        tap(it=>this.items=it),
        
      ).subscribe();

  }

}
