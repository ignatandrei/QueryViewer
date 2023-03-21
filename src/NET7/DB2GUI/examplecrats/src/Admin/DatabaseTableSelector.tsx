import { Collapse, Spin } from 'antd';
import useRxObs from '../useRXEffect';
import columnTable from './column';
import DatabaseAdmin from './DatabaseAdmin';

const { Panel } = Collapse;

export type DataTableProps = {
    DBName: string;
    TableName: string;
}
export default function DatabaseTableSelector(dtProps: DataTableProps){

    const interaction=new DatabaseAdmin();
    
    const [loading, error,fields] = useRxObs(interaction.getDatabaseTableColumns(dtProps.DBName, dtProps.TableName));
    return <>
    <Collapse accordion>
    <Panel header="Simple Search" key="1">
      <p>Fields for {dtProps.TableName} </p>
      {loading && <Spin />}
      {error && <> - error loading data </>}
      {fields && <> - {fields.length} fields</>}
      {fields && fields.map((it: columnTable, index:number) => 
        <>
    
        <p key={index}>{it.name}:{it.type}, {it.isnullable?"Null,":""}  {it.ispk?"PK":""}</p>
        </>
        )}
    </Panel>
    <Panel header="Advanced Search" key="2">
      <p>Not yet ready</p>
    </Panel>
  </Collapse>
  </>
}