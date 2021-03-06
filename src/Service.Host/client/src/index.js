﻿import React from 'react';
import ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { SnackbarProvider } from 'notistack';
import ThemeProvider from '@material-ui/styles/ThemeProvider';
import { linnTheme } from '@linn-it/linn-form-components-library';
import configureStore from './configureStore';
import Root from './components/Root';
import userManager from './helpers/userManager';
import 'typeface-roboto';
import '../assets/printStyles.css';

const initialState = {};
const store = configureStore(initialState);
const { user } = store.getState().oidc;

const render = Component => {
    ReactDOM.render(
        <ThemeProvider theme={linnTheme}>
            <SnackbarProvider dense maxSnack={5}>
                <AppContainer>
                    <div className="pageContainer">
                        <Component store={store} />
                    </div>
                </AppContainer>
            </SnackbarProvider>
        </ThemeProvider>,
        document.getElementById('root')
    );
};

if (
    (!user || user.expired) &&
    window.location.pathname !== '/production/maintenance/signin-oidc-client'
) {
    userManager.signinRedirect({
        data: { redirect: window.location.pathname + window.location.search }
    });
} else {
    render(Root);

    // Hot Module Replacement API
    if (module.hot) {
        //module.hot.accept('./reducers', () => store.replaceReducer(reducer));
        module.hot.accept('./components/Root', () => {
            const NextRoot = require('./components/Root').default;
            render(NextRoot);
        });
    }
}
