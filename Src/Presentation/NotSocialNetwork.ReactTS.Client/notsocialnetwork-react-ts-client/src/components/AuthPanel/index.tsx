import React from "react";
import "./AuthPanel.css";
import "../Button/Button.css";
import { ILoginRequest, IRegistrationRequest } from "../../types/Auth.interface";
import axios from "axios";
import { useDispatch } from "react-redux";
import { setIsAuthPanelOpen } from "../../redux/actions/authPanel";

const AuthPanel: React.FC = ({}) => {
    const [isLogin, setIsLogin] = React.useState<boolean>(true);
    const dispatch = useDispatch();

    const handleLogin = (e: any) => {
        e.preventDefault();
        
        const authorValues: ILoginRequest = {
            email: e.target.elements.email.value,
        }

        try{
            axios.post("https://localhost:5001/api/authentication", authorValues)
                .then((response) => {
                    localStorage.clear();

                    localStorage.setItem("authorId", response.data.userId);
                    localStorage.setItem("token", response.data.token);
                    localStorage.setItem("isAuth", "true");
            });

            dispatch(setIsAuthPanelOpen());
        } catch (error) {
            console.log(error);
        }
    }

    const handleRegistration = (e: any) => {
        e.preventDefault();
        
        const authorValues: IRegistrationRequest = {
            email: e.target.elements.email.value,
            name: e.target.elements.name.value,
        }

        try{
            axios.post("https://localhost:5001/api/user", authorValues)
                .then((response) => {
                    localStorage.clear();

                    localStorage.setItem("authorId", response.data.id);
                    localStorage.setItem("token", response.data.token);
                    localStorage.setItem("isAuth", "true");
            });

            dispatch(setIsAuthPanelOpen());
        } catch (error) {
            console.log(error);
        }
    }

    return <>
        <div className="authPanel">
            <div className="tabs">
                <button className="Button" onClick={() => setIsLogin(true)}>Login</button>
                <button className="Button" onClick={() => setIsLogin(false)}>Registration</button>
                <button className="Button right-button" onClick={() => dispatch(setIsAuthPanelOpen())}>X</button>
            </div>
            {isLogin ? 
            <>
                <form onSubmit={handleLogin}>
                    <div className="loginSection">
                        <p className="subTitle">Email</p>
                        <input name="email" placeholder="enter your email" />
                        <br />
                        <button type="submit" className="Button okButton">Login</button>
                    </div>
                </form>
            </> 
            : 
            <>
                <form onSubmit={handleRegistration}>
                    <div className="registrationSection">
                        <p className="subTitle">Name</p>
                        <input name="name" placeholder="enter your name" />
                        <p className="subTitle">Email</p>
                        <input name="email" placeholder="enter your email" />
                        <br />
                        <button type="submit" className="Button okButton">Registration</button>
                    </div>
                </form>
            </>}
        </div>
    </>
}

export default AuthPanel;