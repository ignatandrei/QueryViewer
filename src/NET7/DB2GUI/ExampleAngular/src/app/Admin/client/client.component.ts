import { Component , OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent  implements OnInit {
  public idClient: string | null = null;
  constructor(
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.idClient = this.route.snapshot.paramMap.get('id');
    
  }
}
