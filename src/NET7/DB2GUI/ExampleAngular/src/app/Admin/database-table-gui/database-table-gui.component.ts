import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-database-table-gui',
  templateUrl: './database-table-gui.component.html',
  styleUrls: ['./database-table-gui.component.css']
})
export class DatabaseTableGuiComponent implements OnInit {

  public idDB: string|null = null;
  public idTable: string|null = null;

  constructor(private route: ActivatedRoute) { 
     this.route.paramMap.subscribe(it=>{
      this.idDB = it.get("idDB");
      this.idTable = it.get("idTable");
    });
  }

  ngOnInit(): void {
  }

}
