import { Outlet } from 'react-router-dom';
import { Suspense } from 'react';
import AppBar from './AppBar';
import css from '../../css/common/SharedLayout.module.css';

function SharedLayout({title}) {
    return (
        <>
            <AppBar />
            <div className={css.contanier}>
                <Suspense fallback={null}>
                    <Outlet />
                </Suspense>
            </div>
        </>
    );
};

export default SharedLayout;