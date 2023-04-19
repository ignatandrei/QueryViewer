import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MynavigationComponent } from './mynavigation/mynavigation.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { AdminComponent } from './Admin/admin.component';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { HttpClientModule } from '@angular/common/http';
import { DatabaseAdminGuiComponent } from './Admin/database-admin-gui/database-admin-gui.component';
import { DatabaseGuiComponent } from './Admin/database-gui/database-gui.component';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { RouterModule } from '@angular/router';
import { DatabaseTableGuiComponent } from './Admin/database-table-gui/database-table-gui.component';

@NgModule({
  declarations: [
    AppComponent,
    MynavigationComponent,
    AdminComponent,
    DatabaseAdminGuiComponent,
    DatabaseGuiComponent,
    DatabaseTableGuiComponent,
  ],
  imports: [
    BreadcrumbModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    FormsModule,
    HttpClientModule,
    MatButtonModule,
    RouterModule,
    MatTableModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
