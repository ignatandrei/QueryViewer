import { Breadcrumb } from "antd";
import { useEffect } from "react";
import { Link, useLocation, useMatches } from "react-router-dom";

export default function Bread() {

    let matches = useMatches();
    let crumbs = matches
        .filter((match) => (match.handle &&  (match.handle as any).crumb))
        .map((match) => (match.handle as any).crumb(match));

    const breadcrumbItems =crumbs.map(it=>{
            return {
                title: it,
                key: it,
            }
     });
        

    // let location=useLocation();    
    // const pathSnippets = location.pathname.split('/').filter((i) => i);

    // const extraBreadcrumbItems = pathSnippets.map((data, index) => {
    //     const url = `/${pathSnippets.slice(0, index + 1).join('/')}`;
        
    //     return {
    //         key: url,
    //         title: <Link to={url}>{data}</Link>,
    //     };
    // });

    // const breadcrumbItems = [
    //     {
    //         title: <Link to="/">Home</Link>,
    //         key: 'home',
    //     },
    // ].concat(extraBreadcrumbItems);


    return <>
        <Breadcrumb items={breadcrumbItems}></Breadcrumb>
    </>
}