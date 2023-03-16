import { List, Button } from "antd";
import { useMemo } from "react";
import { Link, useParams } from "react-router-dom";
import useRxObs from "../useRXEffect";
import DatabaseAdmin from "./DatabaseAdmin";

export function DatabaseGui(){

    let { idDB } = useParams();
    
    const [isLoading, error, data]= useRxObs<string[]>(new DatabaseAdmin().getDatabaseTables(idDB||''));
    
    if (isLoading) return <>'Loading...'</>
   
     if (error && error.length>0) return <>'An error has occurred: ' + error</>
  
     if(!data || data.length===0) return <>'No data'</>
  


return<>
The database is {idDB}  

<List
      header={<div>Tables </div>}
      footer={<div>Number : {data.length} </div>}
      bordered
      itemLayout="horizontal"
      rowKey={(item: string) => item}
      dataSource={data}
      renderItem={(item: string) => (
        <List.Item>

         
         <Button type="primary">
         {data.indexOf(item)+1}. <Link to={`/Admin/Databases/${item}`} > {item} </Link>
          </Button>
        </List.Item>
      )}>
      </List> 
      
    </>
    
}