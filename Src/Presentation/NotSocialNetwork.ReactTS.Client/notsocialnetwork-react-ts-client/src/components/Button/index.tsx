import React from "react";
import './Button.css';

interface IButtonProps {
    text: string;
    reference: string;
}

const Button: React.FC<IButtonProps> = ({ text, reference }) => {
    return <>
        <a className="Button" href={reference}>{text}</a>
    </>
}

export default Button