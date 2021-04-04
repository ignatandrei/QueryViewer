import { KeyValue } from './KeyValue';

export class FieldDescription {
  constructor(init: Partial<FieldDescription>) {
    if (init != null) {
      Object.assign(this, init);

      if (init.defaultValue != null)
        this.defaultValue = new SearchField(init.defaultValue);
        
    }

    if (this.defaultValue == null)
      this.defaultValue = new SearchField({ fieldName: this.fieldName, criteria: 0 });


    if(this.defaultValue.criteria != 0){
        // console.log(this.defaultValue );
    }
  }
  public itemName: string = '';
  public queryName: string = '';
  public fieldName: string = '';
  public fieldType: string = '';
  public defaultValue: SearchField = new SearchField({ criteria: 0 });
  public possibleSearches: KeyValue[] = [];
}
export class SearchField {
  constructor(init: Partial<SearchField>) {
    Object.assign(this, init);
  }
  public fieldName: string = '';
  public value: string = '';
  public criteria: number = 0;
  public criteriaString: string = '';
}
