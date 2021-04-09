import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { debounceTime, delay, distinctUntilChanged, filter, finalize, switchMap, tap } from 'rxjs/operators';
import { SearchDataService } from '../services/search-data.service';
import { FieldDescription, SearchField } from '../services/FieldDescription';
import { MetadataService } from '../services/metadata.service';
import { DataService } from '../services/data.service';
import { receivedData } from '../services/receivedData';
import { ColumnMode, SelectionType, SortType, TableColumn } from '@swimlane/ngx-datatable';
import * as FileSaver from 'file-saver';
import * as XLSX from 'xlsx';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { LoaderService } from '../interceptors/loader.service';


@Component({
  selector: 'app-display-query',
  templateUrl: './display-query.component.html',
  styleUrls: ['./display-query.component.css']
})
export class DisplayQueryComponent implements OnInit {

  public item: string='';
  public query:string='';
  public key:string='';
  public fdArr:SearchField[]|null=[];
  public dataRec: receivedData |null = null;
  // rows = [
  //   { name: 'Austin', gender: 'Male', company: 'Swimlane' },
  //   { name: 'Dany', gender: 'Male', company: 'KFC' },
  //   { name: 'Molly', gender: 'Female', company: 'Burger King' }
  // ];

  // columns = [{ prop: 'name' }, { name: 'Gender' }, { name: 'Company' }];
  columns:TableColumn[]=[];
  rows:Array<any>|null=null;
  temp: Array<any>|null=null;
  ColumnMode = ColumnMode;
  SortType = SortType;
  SelectionType = SelectionType;


  fileType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
  fileExtension = '.xlsx';

  firstNameControl = new FormControl();
  formCtrlSub: Subscription|null = null;
  constructor(private route: ActivatedRoute, public ms: MetadataService, public searchData: SearchDataService, private data: DataService, private ls: LoaderService) { 

    
  }

  public exportExcel(): void {

    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.rows as any[]);
    const wb: XLSX.WorkBook = { Sheets: { 'data': ws }, SheetNames: ['data'] };
    const excelBuffer: any = XLSX.write(wb, { bookType: 'xlsx', type: 'array' });
    this.saveExcelFile(excelBuffer, "export");
  }

  private saveExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], {type: this.fileType});
    FileSaver.saveAs(data, fileName + this.fileExtension);
  }

  
  updateFilter(val: any) {
    try{
      this.ls.isLoading(true);
    
    if(!val){
      this.rows=this.temp;
      return;
    }
    
    // filter our data
    const t1 = this.temp?.filter(function (d) {
      
      const vals = Object.keys(d).map(key => d[key]).filter(it=>it!=null).map(it=>it.toString());
      return vals.filter(it=>it.toLowerCase().indexOf(val) !== -1).length>0 ;
      }
    )||[];

    this.rows = t1;
    }
    finally{
      this.ls.isLoading(false);
    }    
  }
  ngOnInit(): void {
    this.formCtrlSub=this
      .firstNameControl
      .valueChanges.pipe(
        
        debounceTime(1000),
        distinctUntilChanged(),
        tap(it=> this.ls.isLoading(true)),
        debounceTime(2000),
        tap(it=> this.updateFilter(it)),
        finalize(()=>this.ls.isLoading(false))

      ).subscribe();

      this.route.params
      .pipe(
        delay(1000),
        tap(it=> {
          this.item=it["item"] as string;
          this.query=it["query"] as string;
          this.key=it['key'] as string;
          this.searchData.getSearch(this.item, this.query,this.key).subscribe(
            arr=>{
              this.fdArr =arr;

              this.data.GetData(this.item,this.query,arr).subscribe(it=>
                {
                  if(it != null){
                  this.dataRec=it;
                  this.columns  = it.fieldNames.map(fn=>({ "name": fn.fieldName, 'prop':fn.fieldName ,'sortable':true}));
                  this.rows=[...it.values];
                  this.temp=[...it.values];
                  // console.log(this.columns);
                  // console.log(this.rows);
                  }
                } 
                  );



            }

          )

        }
        ),
        
        // tap(it=>console.log(it))
      )
    .subscribe();

  }
  ngOnDestroy() {
    if(this.formCtrlSub != null)
      this.formCtrlSub.unsubscribe();
  }

}
