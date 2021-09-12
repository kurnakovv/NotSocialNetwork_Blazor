import React from "react";

import "../EventWindow/EventWindow.css";
import "../Button/Button.css";
import AppContext from "../../contexts/AppContext";

const EventWindow: React.FC = ({}) => {

    const context = React.useContext(AppContext);
    const { eventWindowIsVisible, setEventWindowIsVisible, eventWindowText, eventWindowImg } = context;

    return <>
        <div className="EventWindow" style={{display: eventWindowIsVisible ? "" : "none"}}>
            <div className="ExitButton"><button className="Button" onClick={() => setEventWindowIsVisible(false)}>X</button></div>
            <p>{eventWindowText}</p>
            <img className="WindowImg" width={25} height={25} src={eventWindowImg} />
        </div>
    </>
}

export default EventWindow;