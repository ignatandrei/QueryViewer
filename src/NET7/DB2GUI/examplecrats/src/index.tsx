import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { createBrowserRouter, Link, Outlet, RouterProvider } from 'react-router-dom';
import ContentAdminGui from './Admin/ContentAdminGUI';
import DatabaseAdminGui from './Admin/DatabaseAdminGUI';
import { DatabaseGui } from './Admin/DatabaseGUI';
import { DatabaseTableGui } from './Admin/DatabaseTableGUI';
import Root from './Root';
// import { QueryClient, QueryClientProvider } from 'react-query';
// const queryClient = new QueryClient();
const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

const router = createBrowserRouter([

  {
    element: <App />,
    children: [

      {
        path: "/",
        element: <Root />,
        handle: {
          crumb: () => <Link to="/">Home</Link>,
        }
        //errorElement: <ErrorPage />,
      },
      {
        path: "/Home",
        element: <Root />,
        handle: {
          crumb: () => <Link to="/">Home</Link>,
        }

        //errorElement: <ErrorPage />,
      },
      {
        path: "/Admin",
        element: <ContentAdminGui />,
        //errorElement: <ErrorPage />,
        handle: {
          crumb: () => <Link to="/Admin">Admin</Link>,
        }

      },
      {
        path: "/Admin/Databases",
        element: <DatabaseAdminGui />,
        handle: {
          crumb: () => <Link to="/Admin/Databases">Databases</Link>,
        },
      },

      {
        id: "db",
        path: "/Admin/Databases/:idDB",
        element: <DatabaseGui />,
        handle: {
          crumb: () => <Link to="/Admin/Databases/xxx">DB:</Link>,
        },
      },
      {
        id: "table",
        path: "/Admin/Databases/:idDB/tables/:idTable",
        element: <DatabaseTableGui />,
        handle: {
          crumb: () => <Link to="/Admin/Databases/:idDB/tables/:idTable">Table:</Link>,
        }
        //errorElement: <ErrorPage />,
      },

    ]
  }
]);




root.render(
  <React.StrictMode>
    {/* <QueryClientProvider client={queryClient}> */}
    <RouterProvider router={router} />
    {/* </QueryClientProvider> */}
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
