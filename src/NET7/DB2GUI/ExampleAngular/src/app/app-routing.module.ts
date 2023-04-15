import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './Admin/admin.component';
import { DatabaseAdminGuiComponent } from './Admin/database-admin-gui/database-admin-gui.component';
import { DatabaseGuiComponent } from './Admin/database-gui/database-gui.component';
import { DatabaseTableGuiComponent } from './Admin/database-table-gui/database-table-gui.component';

// import { ClientComponent } from './Admin/client/client.component';
// import { ClientListComponent } from './Admin/client-list/client-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'admin', pathMatch: 'full' },
  {
    path: 'admin',
    children:[
      {path:'',   component: AdminComponent},

  {
    path:'databases',
    pathMatch: 'prefix'

  ,children: [
    {path:'', component:DatabaseAdminGuiComponent },
    { path: ':idDB', 
      children: [
        {path:'', component:DatabaseGuiComponent},
        {path:'tables', children:[
          {path:'', component:DatabaseGuiComponent},
          {path:':idTable', component:DatabaseTableGuiComponent}
        ]},
      ]
    }
  ] 
},
]
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes,{ enableTracing: true })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
