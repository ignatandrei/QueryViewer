import { Button, Collapse, Input, Spin } from 'antd';
import useRxObs from '../useRXEffect';
import columnTable, { SearchCriteria, SearchCriteriaArray } from './column';
import DatabaseAdmin from './DatabaseAdmin';
import { Cascader } from 'antd';
import { useState } from 'react';
const { Panel } = Collapse;

export type DataTableProps = {
    DBName: string;
    TableName: string;
}
export type ActionSearch ={
    searchSimple:(searchData: SearchData)=>void;
    loadingData: boolean;        
}
export class SearchData{
    public constructor(init: Partial<SearchData> | null = null){
        if(init == null)
            return ;
        Object.assign(this,init);
    }

    public IsValid(): boolean {
        if(this.ColumnName==null || this.ColumnName==='')
            return false;

        if(this.Operator==null || this.Operator===SearchCriteria.None)
            return false;

        if(this.Value==null || this.Value==='')
            return false;

        return true;
    }
    public ColumnName: string='';
    public Operator: SearchCriteria=SearchCriteria.None;
    public Value: string='';
}
interface Option {
    value: string | number;
    label: string;
    children?: Option[];
  }
export default function DatabaseTableSelector(dtProps: DataTableProps & ActionSearch){

    const [searchData, setSearchData] = useState<SearchData|null>(null);
    const interaction=new DatabaseAdmin();
    const functLoadData1 = (fields: columnTable[]) => {
        console.log('sdasdasda',SearchCriteriaArray )
        const options = fields.map((d: columnTable) => {
            var it = new columnTable(d);
            var label=it.name;        
            if(it.ispk) label=`*${label}`;
            if(!it.isnullable) label=`*${label}`;
            return {
          value: it.name,
          label: label,
          children: it.SupportedOperators().map((op: SearchCriteria) => {
            return {
              value: op.toString(),
              label: SearchCriteriaArray[+op].toString(),
            };
          })
        };
        });

        setColumnsOptions(options);
    }

    const [loading, error] = useRxObs(interaction.getDatabaseTableColumns(dtProps.DBName, dtProps.TableName), functLoadData1);
    
    const [columnsOptions, setColumnsOptions] = useState<Option[]>([
        {
          value: 'Columns',
          label: `Loading...`,
        }
      ]);
    
    const onChangeField = (value: (string | number)[]) => {
        setSearchData(it=>{
            var prev='';
            if(it!=null)
                prev=it.Value;
            
            return new SearchData({ColumnName: value[0] as string, Operator: value[1] as SearchCriteria, Value: prev});
            
            
        });
      };
      const onChangeValue=(value: string) => {
        setSearchData(it=>{
            if(it!=null)
                return new SearchData({...it , Value: value});
            else
                return new SearchData({ColumnName: '' , Operator: SearchCriteria.None, Value: value});
        
        });
      }
      const filterColumns = (inputValue: string) =>{
      
        return columnsOptions.some(
        (option) => (option.label as string).toLowerCase().indexOf(inputValue.toLowerCase()) > -1,
      );
    }
    const onClickSimple=()=>{
      
      if(dtProps && dtProps.searchSimple && searchData!=null)
        dtProps.searchSimple!(searchData!);
    }
    return <>
    <Collapse accordion>
    <Panel header="Simple Search" key="1">
      {loading && <Spin />}
      {error && <> - error loading data </>}
      <Cascader  showSearch={{filter: filterColumns }} options={columnsOptions} onChange={onChangeField} placeholder="Please select column" />
      <Input placeholder="enter the value"  onChange ={e=>onChangeValue(e.target.value)}/>

      <Button loading={dtProps.loadingData} disabled={(searchData == null) || !searchData.IsValid()}  type="primary" onClick={onClickSimple} >Search {'=>'} {searchData && 
      <>
        {searchData.ColumnName} {SearchCriteriaArray[+searchData?.Operator]} {searchData?.Value}
      </>
        } 
      </Button>
    </Panel>
    <Panel header="Advanced Search" key="2">
      <p>Not yet ready</p>
    </Panel>
  </Collapse>
  </>
}