import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { MetadataService } from '../services/metadata.service';

@Component({
  selector: 'app-my-nav',
  templateUrl: './my-nav.component.html',
  styleUrls: ['./my-nav.component.css']
})
export class MyNavComponent implements OnInit  {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(private breakpointObserver: BreakpointObserver, public ms:MetadataService) {
    }
  ngOnInit(): void {
    this.ms.GetItems();
    //this.ms.exposeItems().subscribe(it=> window.alert('b'+it.length));
    //this.ms.exposeItems().subscribe(it=> window.alert('b1'+it.length));
  
  }

  
}
