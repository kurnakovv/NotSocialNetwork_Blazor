import React from "react";
import "./AuthPanel.css";
import "../Button/Button.css";

const AuthPanel: React.FC = ({}) => {
    const [isLogin, setIsLogin] = React.useState<boolean>(true);
    return <>
        <div className="authPanel">
            <div className="tabs">
                <button className="Button" onClick={() => setIsLogin(true)}>Login</button>
                <button className="Button" onClick={() => setIsLogin(false)}>Registration</button>
                <button className="Button right-button">X</button>
            </div>
            {isLogin ? 
            <>
                <form>
                    <div className="loginSection">
                        <p className="subTitle">Email</p>
                        <input placeholder="enter your email" />
                        <br />
                        <button type="submit" className="Button okButton">Login</button>
                    </div>
                </form>
            </> 
            : 
            <>
                <form>
                    <div className="registrationSection">
                        <p className="subTitle">Name</p>
                        <input placeholder="enter your name" />
                        <p className="subTitle">Email</p>
                        <input placeholder="enter your email" />
                        <br />
                        <button type="submit" className="Button okButton">Registration</button>
                    </div>
                </form>
            </>}
        </div>
    </>
}

export default AuthPanel;