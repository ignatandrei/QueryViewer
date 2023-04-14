import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './Admin/admin.component';
import { DatabaseAdminGuiComponent } from './Admin/database-admin-gui/database-admin-gui.component';
import { DatabaseGuiComponent } from './Admin/database-gui/database-gui.component';

// import { ClientComponent } from './Admin/client/client.component';
// import { ClientListComponent } from './Admin/client-list/client-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/admin', pathMatch: 'full' },
  { path: 'admin', component:AdminComponent},
  // {path:'admin/client/:id',component:ClientComponent},
  // {path:'admin/clientList',component:ClientListComponent},
  { path: 'admin/databases', component:DatabaseAdminGuiComponent },
  {path: "admin/databases/:idDB", component:DatabaseGuiComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
