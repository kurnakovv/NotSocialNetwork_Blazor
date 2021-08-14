import React from "react";
import './Button.css';

interface IButtonProps{
    text: string;
    reference: string;
}

export default class Button extends React.Component<IButtonProps> {
    render(){
        return <>
            <a className="Button" href={this.props.reference}>{this.props.text}</a>
        </>
    }
}