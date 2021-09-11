import React from "react";

import "../EventWindow/EventWindow.css";
import Button from "../Button";

interface IEventWindowProps {
    img: string;
}

const EventWindow: React.FC<IEventWindowProps> = ({ img }) => {
    return <>
        <div className="EventWindow">
            <div className="ExitButton"><Button reference="/" text="X" /></div>
            <p>Publication added to:</p>
            <img className="WindowImg" width={25} height={25} src={img} />
        </div>
    </>
}

export default EventWindow;