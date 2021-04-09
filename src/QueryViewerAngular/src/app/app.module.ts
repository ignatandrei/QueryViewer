import { BrowserModule, Meta } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MyNavComponent } from './my-nav/my-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { AllItemsComponent } from './all-items/all-items.component';
import { HttpClientModule } from '@angular/common/http';
import { DisplayItemComponent } from './display-item/display-item.component';
import { FormsModule } from '@angular/forms';
import { DisplayQueryComponent } from './display-query/display-query.component';
import{ MatExpansionModule} from '@angular/material/expansion';
import{  MatTableModule} from '@angular/material/table';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { httpInterceptorProviders } from './interceptors/barrelInterceptors';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { ReactiveFormsModule } from '@angular/forms';
import { MetadataService } from './services/metadata.service';

export function InitializeMetadata(ms: MetadataService) {
  return ()=> ms.Init();
  }


@NgModule({
  declarations: [
    AppComponent,
    MyNavComponent,
    AllItemsComponent,
    DisplayItemComponent,
    DisplayQueryComponent
  ],
  imports: [
    MatProgressSpinnerModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LayoutModule,
    NgxDatatableModule ,
    MatTableModule,
    MatExpansionModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatInputModule,
    MatIconModule,
    MatSelectModule,
    MatListModule,
    HttpClientModule,
    FormsModule,
    MatSnackBarModule
    ],
  providers: [ 

    ...httpInterceptorProviders,
    MetadataService,
    { provide: APP_INITIALIZER,useFactory: InitializeMetadata, deps: [MetadataService], multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
