import { Button } from "antd";
import { Link } from "react-router-dom";

export default function Root(){
    return (<>

    Please click on 
    <Button type="primary">
    <Link to="/Admin/Databases">Databases</Link> 
    </Button>
    </>)
}