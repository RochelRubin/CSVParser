import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './Layout';
import Home from './Pages/Home';
import Upload from './Pages/Upload';
import Generate from './Pages/Generate';


export default class App extends Component {
    static displayName = App.name;
    render() {
        return (
            <Layout>
                <Route exact path="/" component={Home} />
                <Route exact path="/upload" component={Upload} />
                <Route exact path="/generate" component={Generate} />
            </Layout>
        )
    }
}