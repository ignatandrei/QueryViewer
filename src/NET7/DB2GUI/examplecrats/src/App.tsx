import React from 'react';
import './App.css';
import 'antd/dist/reset.css';
import './App.css';
import { LaptopOutlined, NotificationOutlined, UserOutlined } from '@ant-design/icons';
import type { MenuProps } from 'antd';
import { Breadcrumb, Layout, Menu, theme } from 'antd';
import { BrowserRouter, createBrowserRouter, Link, Outlet, RouterProvider} from 'react-router-dom';
import Root from './Root';
import ContentAdminGui from './Admin/ContentAdminGUI';
import DatabaseAdminGui from './Admin/DatabaseAdminGUI';
import { DatabaseGui } from './Admin/DatabaseGUI';
import { DatabaseTableGui } from './Admin/DatabaseTableGUI';
import Bread from './Bread';


const { Header, Content, Sider } = Layout;

const itemsHeader: MenuProps['items'] = ['Home', 'Admin'].map((key) => ({
    key: key.toLocaleLowerCase(),
    label: `${key}`,
}));

const items2: MenuProps['items'] = [

        {
            key: `sub1`,
            //icon: React.createElement(icon),
            label: 'Home',

            children: [
                {
                    key: `Admin`,
                    label: 'Admin',
                }
            ]
        
    }
];
        
function App() {

    
    const navigateTo = ( info: any ) => {
        window.location.href= `/${info.key}`;
    }
    const {
        token: { colorBgContainer },
    } = theme.useToken();
  return (
    <>
        <Layout>
            <Header className="header">
                <div className="logo" />
              <Menu theme="dark" mode="horizontal" onClick={navigateTo} defaultSelectedKeys={['2']} items={itemsHeader} />
            </Header>
            <Layout>
                <Sider width={200} style={{ background: colorBgContainer }}>
                    <Menu
                        mode="inline"
                        defaultSelectedKeys={['1']}
                        defaultOpenKeys={['sub1']}
                        style={{ height: '100%', borderRight: 0 }}
                        items={items2}
                    />
                </Sider>
                <Layout style={{ padding: '0 24px 24px' }}>
                    <Content
                        style={{
                            padding: 24,
                            margin: 0,
                            minHeight: 280,
                            background: colorBgContainer,
                        }}
                    >
<Bread></Bread>
<Outlet/>                    
                        
                        
                    </Content>
                </Layout>
            </Layout>
        </Layout>
        </>
        );
}

export default App;
