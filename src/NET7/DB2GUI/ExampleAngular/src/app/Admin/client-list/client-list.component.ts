import { Component, OnInit } from '@angular/core';
import { ClientService } from '../client.service';
import { ApplicationDBContext_Client_Table } from '../ApplicationDBContext_Client_Table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.css']
})
export class ClientListComponent implements OnInit {

  public ClientList: ApplicationDBContext_Client_Table[] | null = null;
  constructor(private clientService: ClientService,private router: Router) { }

  ngOnInit(): void {
    this.RefreshClientList();
  }
  public RefreshClientList():void{

    this.clientService.getClients().subscribe((data: ApplicationDBContext_Client_Table[]) => {
    
      this.ClientList = data;
    });
  }
  public AddClient():void{
    var newClient = new ApplicationDBContext_Client_Table();
    var newName=window.prompt("Enter Client Name");
    if(newName==null)return;
    newClient.nameClient= newName;
    this.clientService.addClient(newClient).subscribe((data) => {
      this.RefreshClientList();
    });
  }
  
  public gotoClient(client:ApplicationDBContext_Client_Table):void{
    this.router.navigate(['admin','client', client.idClient]);
  }

}

