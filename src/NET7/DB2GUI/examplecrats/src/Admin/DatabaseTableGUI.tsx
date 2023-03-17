import { List, Button } from "antd";
import React, { Suspense } from "react";
import { Link, useParams } from "react-router-dom";
import useRxObs from "../useRXEffect";
import columnTable from "./column";
import DatabaseAdmin from "./DatabaseAdmin";
import Department_table_data from "./Generated/Models/ApplicationDBContext/Department";

export function DatabaseTableGui(){

    let { idDB,idTable } = useParams();
    
//    const [isLoading, error, data]= useRxObs<columnTable[]>(new DatabaseAdmin().getDatabaseTableColumns(idDB||'', idTable||''));
    
    // if (isLoading) return <>'Loading...'</>
   
    //  if (error && error.length>0) return <>'An error has occurred: ' + error</>
  
    //  if(!data || data.length===0) return <>'No data'</>
  
  const MyComponent = React.lazy(() => import(`./Generated/Models/${idDB}/${idTable}`));
return<>

 <Suspense fallback={<div>Loading...</div>}>
  <MyComponent />  
</Suspense>
     
    </>
    
}