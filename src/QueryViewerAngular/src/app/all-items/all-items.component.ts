import { utf8Encode } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { delay, tap } from 'rxjs/operators';
import { MetadataService } from '../services/metadata.service';

@Component({
  selector: 'app-all-items',
  templateUrl: './all-items.component.html',
  styleUrls: ['./all-items.component.css']
})
export class AllItemsComponent implements OnInit {

  public items : string[] = [];
  constructor(public ms:MetadataService) { 

    
  }

  ngOnInit(): void {
    
    this.ms.exposeItems()
      .pipe(
        delay(1000),
        tap(it=>this.items=it)
      )
    .subscribe();

  }

}
