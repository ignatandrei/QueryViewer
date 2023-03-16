import { Button, List } from "antd"
import { useEffect, useState } from "react";
import { useQuery } from "react-query"
import { Link } from "react-router-dom"
import useRxObs from "../useRXEffect";
import DatabaseAdmin from "./DatabaseAdmin";

export default function DatabaseAdminGui() {

  const [isLoading, error, data]= useRxObs<string[]>(()=>new DatabaseAdmin().getDatabases());
  
  if (isLoading) return <>'Loading...'</>
 
   if (error && error.length>0) return <>'An error has occurred: ' + error</>

   if(!data) return <>'No data'</>

    return (<>
    You can administer the databases 
    {/* {data.map((db)=>{return <>{db}</>})  */}
       
   <List
      header={<div>Databases</div>}
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
    )

}