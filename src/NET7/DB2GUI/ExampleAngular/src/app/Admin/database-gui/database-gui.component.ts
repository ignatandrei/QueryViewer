import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DatabaseAdmin } from '../classes/DatabaseAdmin';


@Component({
  selector: 'app-database-gui',
  templateUrl: './database-gui.component.html',
  styleUrls: ['./database-gui.component.css']
})
export class DatabaseGuiComponent implements OnInit {

  public idDB: string | null = null;
  public data: string[]|null = null;
  constructor(private route: ActivatedRoute, private db:DatabaseAdmin) { 
    this.idDB = this.route.snapshot.paramMap.get('idDB');
  }

  ngOnInit(): void {
    this.db.getDatabaseTables(this.idDB||'').subscribe(it=>{
      this.data=it.sort();
    });
  } 


}


