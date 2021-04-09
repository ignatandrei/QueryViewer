import { Injectable} from '@angular/core';
import { Query, Store, StoreConfig } from '@datorama/akita';

export class Items{
  public items:string[]=['loading'];
}


@StoreConfig({ name: 'items' })
@Injectable({
  providedIn: 'root'
})
export class StoreItemsService extends Store<Items> {

  constructor() { 

    super(new Items());
  }

}

@Injectable({ providedIn: 'root' })
export class StoreItemsQuery extends Query<Items> {
  constructor(protected store: StoreItemsService) {
    super(store);
  }
}