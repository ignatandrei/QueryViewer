import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay, tap } from 'rxjs/operators';
import { MetadataService } from '../services/metadata.service';
import { ErrorService } from '../interceptors/error.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LoaderService } from '../interceptors/loader.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
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
    public items: string[]=[];
  constructor(private breakpointObserver: BreakpointObserver, public ms:MetadataService
    
    , private err: ErrorService,
    public snackBar: MatSnackBar
    ,         private ls: LoaderService
    ) {

      this.err.NextError().pipe(
        untilDestroyed(this),
        tap(it => {
          this.snackBar.open(it, 'ERROR', {
            duration: 5000,
            verticalPosition: 'top'
          });
        }),
        shareReplay()
      ).subscribe();

      this.ls.loading$().pipe(untilDestroyed(this)).subscribe(it => this.isLoading = it);

    }
  ngOnInit(): void {
    this.ms.exposeItems().pipe(untilDestroyed(this)).subscribe(it=>this.items=it); 
  }

  
}

