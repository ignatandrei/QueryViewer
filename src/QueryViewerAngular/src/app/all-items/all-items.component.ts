import { utf8Encode } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { MetadataService } from '../services/metadata.service';

@Component({
  selector: 'app-all-items',
  templateUrl: './all-items.component.html',
  styleUrls: ['./all-items.component.css']
})
export class AllItemsComponent implements OnInit {

  constructor(public ms:MetadataService) { 

    
  }

  ngOnInit(): void {
    
    //this.ms.exposeItems().subscribe(it=>window.alert('aaa'+it.length));

  }

}
