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
import { ClientComponent } from './Admin/client/client.component';
import { ClientListComponent } from './Admin/client-list/client-list.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { DatabaseAdminGuiComponent } from './Admin/database-admin-gui/database-admin-gui.component';
import { DatabaseGuiComponent } from './Admin/database-gui/database-gui.component';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    MynavigationComponent,
    AdminComponent,
    ClientComponent,
    ClientListComponent,
    DatabaseAdminGuiComponent,
    DatabaseGuiComponent
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
    RouterModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
