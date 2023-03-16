import React from 'react';
import './App.css';
import 'antd/dist/reset.css';
import './App.css';
import { LaptopOutlined, NotificationOutlined, UserOutlined } from '@ant-design/icons';
import type { MenuProps } from 'antd';
import { Breadcrumb, Layout, Menu, theme } from 'antd';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import Root from './Root';
import ContentAdminGui from './Admin/ContentAdminGUI';
import DatabaseAdminGui from './Admin/DatabaseAdminGUI';
import { DatabaseGui } from './Admin/DatabaseGUI';
import { DatabaseTableGui } from './Admin/DatabaseTableGUI';


const { Header, Content, Sider } = Layout;

const itemsHeader: MenuProps['items'] = ['Home', 'Admin'].map((key) => ({
    key: key.toLocaleLowerCase(),
    label: `${key}`,
}));

const items2: MenuProps['items'] = [UserOutlined, LaptopOutlined, NotificationOutlined].map(
    (icon, index) => {
        const key = String(index + 1);

        return {
            key: `sub${key}`,
            icon: React.createElement(icon),
            label: `subnav ${key}`,

            children: new Array(4).fill(null).map((_, j) => {
                const subKey = index * 4 + j + 1;
                return {
                    key: subKey,
                    label: `option${subKey}`,
                };
            }),
        };
    },
);
function App() {

    const router = createBrowserRouter([
        {
          path: "/",
          element: <Root />,
          //errorElement: <ErrorPage />,
        },
        {
            path: "/Home",
            element: <Root />,
            //errorElement: <ErrorPage />,
        },        
        {
            path: "/Admin",
            element: <ContentAdminGui />,
            //errorElement: <ErrorPage />,
        },
        {
            path: "/Admin/Databases",
            element: <DatabaseAdminGui />,
            //errorElement: <ErrorPage />,
        },        
        {
            path: "/Admin/Databases/:idDB",
            element: <DatabaseGui />,
            //errorElement: <ErrorPage />,
        },
        {
            path: "/Admin/Databases/:idDB/tables/:idTable",
            element: <DatabaseTableGui />,
            //errorElement: <ErrorPage />,
        },

      ]);
    
    const navigateTo = ( info: any ) => {
        window.location.href= `/${info.key}`;
    }
    const {
        token: { colorBgContainer },
    } = theme.useToken();
  return (
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
                    <Breadcrumb style={{ margin: '16px 0' }}>
                        <Breadcrumb.Item>Home</Breadcrumb.Item>
                        <Breadcrumb.Item>List</Breadcrumb.Item>
                        <Breadcrumb.Item>App</Breadcrumb.Item>
                    </Breadcrumb>
                    <Content
                        style={{
                            padding: 24,
                            margin: 0,
                            minHeight: 280,
                            background: colorBgContainer,
                        }}
                    >                        
                        <RouterProvider router={router} />
                    </Content>
                </Layout>
            </Layout>
        </Layout>
    );
}

export default App;
