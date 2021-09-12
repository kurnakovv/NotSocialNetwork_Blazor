import React from "react";

import "../EventWindow/EventWindow.css";
import Button from "../Button";
import AppContext from "../../contexts/AppContext";

interface IEventWindowProps {
    img: string;
}

const EventWindow: React.FC<IEventWindowProps> = ({ img }) => {

    const context = React.useContext(AppContext);

    return <>
        <div className="EventWindow" style={{display: context.eventWindowIsVisible ? "" : "none"}}>
            <div className="ExitButton"><Button reference="/" text="X" /></div>
            <p>Publication added to:</p>
            <img className="WindowImg" width={25} height={25} src={img} />
        </div>
    </>
}

export default EventWindow;