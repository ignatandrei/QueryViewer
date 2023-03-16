import { List, Button } from "antd";
import { Link, useParams } from "react-router-dom";
import useRxObs from "../useRXEffect";
import columnTable from "./column";
import DatabaseAdmin from "./DatabaseAdmin";

export function DatabaseTableGui(){

    let { idDB,idTable } = useParams();
    
    const [isLoading, error, data]= useRxObs<columnTable[]>(new DatabaseAdmin().getDatabaseTableColumns(idDB||'', idTable||''));
    const [isLoadingNumber, errorNumber, dataNumber]= useRxObs<number>(new DatabaseAdmin().getTableRowsNumber(idDB||'', idTable||''));
    
    if (isLoading) return <>'Loading...'</>
   
     if (error && error.length>0) return <>'An error has occurred: ' + error</>
  
     if(!data || data.length===0) return <>'No data'</>
  


return<>
The table is {idTable} from database {idDB}
{isLoadingNumber? "": `Number rows : ${dataNumber} `}
<List
      header={<div>Columns </div>}
      footer={<div>Number : {data.length} </div>}
      bordered
      itemLayout="horizontal"
      rowKey={(item: columnTable) => item.name}
      dataSource={data}
      renderItem={(item: columnTable) => (
        <List.Item>
         {data.indexOf(item)+1}.  {item.name} {item.type} {item.isNullable?'NULL':'NOT NULL'}          
        </List.Item>
      )}>
      </List> 
      
    </>
    
}