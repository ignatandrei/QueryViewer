import { List } from "antd"
import { useQuery } from "react-query"

export default function ContentAdmin() {

    const { isLoading, error, data } = useQuery('DBNames', () =>
    fetch('http://localhost:5018/MetaData/DBNames').then(res =>
      res.json() as unknown as string[]
    )
  )
  if (isLoading) return <>'Loading...'</>
 
   if (error) return <>'An error has occurred: ' + error</>

   if(!data) return <>'No data'</>

    return (<>
    You can administer the databases 
    {/* {data.map((db)=>{return <>{db}</>})  */}
       
    }
    
    <List
      header={<div>Databases</div>}
      footer={<div>Number : {data.length} </div>}
      bordered
      rowKey={(item: string) => item}
      dataSource={data}
      renderItem={(item: string) => (
        <List.Item>
         {data.indexOf(item)+1} ) {item}
        </List.Item>
      )}>
      </List>
    </>
    )

}