import { Collapse, Spin } from 'antd';
import useRxObs from '../useRXEffect';
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
    </Panel>
    <Panel header="Advanced Search" key="2">
      <p>Not yet ready</p>
    </Panel>
  </Collapse>
  </>
}