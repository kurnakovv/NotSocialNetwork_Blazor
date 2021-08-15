import React from "react";
import "../EventWindow/EventWindow.css";

import Button from "../Button";

interface IEventWindowProps {
    img: string;
}

export default class EventWindow extends React.Component<IEventWindowProps, any> {
    render() {
        return <>
            <div className="EventWindow">
                <div className="ExitButton"><Button reference="/" text="X" /></div>
                <p>Publication added to:</p>
                <img className="WindowImg" width={25} height={25} src={this.props.img} />
            </div>
        </>
    }
}