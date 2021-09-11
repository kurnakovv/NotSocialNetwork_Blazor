import React from "react";
import Search from "../Search/index";
import Button from "../Button/index";
import logo from '../../logo.svg';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Navbar.css';

export const Navbar: React.FC = () => {
    return <>
        <div className="Navbar-main">
            <nav className="navbar navbar-expand-lg navbar-dark">
                <div className="container-fluid">
                    <img width={60} height={60} src={logo} className="App-logo" alt="logo" />
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                            <li className="nav-item">
                                <a className="nav-link active" href="/">NotSocialNetwork</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href="/Publications">Account</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href="/Publications">My publications</a>
                            </li>
                        </ul>
                        <Search />
                        <Button
                            text="Login"
                            reference="/Login"
                        />
                        <Button
                            text="Register"
                            reference="/Register"
                        />
                    </div>
                </div>
            </nav>
        </div>
    </>
}