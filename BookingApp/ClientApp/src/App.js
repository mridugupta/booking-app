import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
//import { Route, Routes } from 'react-router-dom';
import RoomList from './components/RoomList';
import RoomDetail from './components/RoomDetail';

import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';

/*function App() {
    return (
        <Router>
            <div>
                <Routes>
                    <Route path="/" exact component={RoomList} />
                    <Route path="/room/:id" component={RoomDetail} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;*/
export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Routes>
          {AppRoutes.map((route, index) => {
            const { element, ...rest } = route;
            return <Route key={index} {...rest} element={element} />;
          })}
        </Routes>
      </Layout>
    );
  }
}
