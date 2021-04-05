import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay, tap } from 'rxjs/operators';
import { MetadataService } from '../services/metadata.service';
import { ErrorService } from '../interceptors/error.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LoaderService } from '../interceptors/loader.service';

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
    public isLoading = false;
  constructor(private breakpointObserver: BreakpointObserver, public ms:MetadataService
    
    , private err: ErrorService,
    public snackBar: MatSnackBar
    ,         private ls: LoaderService
    ) {

      this.err.NextError().pipe(
        tap(it => {
          // this.snackBar.open(it, 'ERROR', {
          //   duration: 5000,
          //   verticalPosition: 'top'
          // });
        }),
        shareReplay()
      ).subscribe();

      this.ls.loading$().subscribe(it => this.isLoading = it);

    }
  ngOnInit(): void {
    this.ms.GetItems();
    //this.ms.exposeItems().subscribe(it=> window.alert('b'+it.length));
    //this.ms.exposeItems().subscribe(it=> window.alert('b1'+it.length));
  
  }

  
}

