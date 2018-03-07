import React from 'react';
import { HashRouter as Router, Route } from 'react-router-dom';
import SiteHeader from './components/SiteHeader';
import Home from './views/Home';
import Rules from './views/Rules';
import Ranking from './views/Ranking';
import Login from './views/Login';
import Register from './views/Register';

function App() {
  return (
    <Router>
      <div className="app">
        <SiteHeader />

        <Route path="/" component={Home} exact />
        <Route path="/rules" component={Rules} />
        <Route path="/ranking" component={Ranking} />
        <Route path="/login" component={Login} />
        <Route path="/register" component={Register} />
      </div>
    </Router>
  );
}

export default App;
