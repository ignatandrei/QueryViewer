import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AllItemsComponent } from './all-items/all-items.component';
import { DisplayItemComponent } from './display-item/display-item.component';
import { DisplayQueryComponent } from './display-query/display-query.component';

const routes: Routes = [
  { path: '', redirectTo: '/AllItems', pathMatch: 'full' },
  { path: 'AllItems', component: AllItemsComponent },
  {path:"DisplayItem/:item", component: DisplayItemComponent},

  {path:"DisplayQuery/:item/:query/:key", component: DisplayQueryComponent},
  {path:"DisplayQuery/:item/:query", component: DisplayQueryComponent}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
