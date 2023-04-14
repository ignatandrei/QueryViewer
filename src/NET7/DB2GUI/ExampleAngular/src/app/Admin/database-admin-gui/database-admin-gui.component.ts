import { Component, OnInit } from '@angular/core';
import { DatabaseAdmin } from '../classes/DatabaseAdmin';


@Component({
  selector: 'app-database-admin-gui',
  templateUrl: './database-admin-gui.component.html',
  styleUrls: ['./database-admin-gui.component.css']
})
export class DatabaseAdminGuiComponent implements OnInit {

  public data:string[]|null=null;
  constructor(private db:DatabaseAdmin) { }

  ngOnInit(): void {
    this.db.getDatabases().subscribe((data:string[])=>{
      this.data=data;
    });

  }

}


