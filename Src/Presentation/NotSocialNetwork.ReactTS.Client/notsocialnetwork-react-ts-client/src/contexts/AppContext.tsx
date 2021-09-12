import React from "react";

const AppContext = React.createContext({
    eventWindowIsVisible: false,
    setEventWindowIsVisible: (eventWindowIsVisible: boolean) => {},
    eventWindowText: "",
    setEventWindowText: (eventWindowText: string) => {},
    eventWindowImg: "",
    setEventWindowImg: (eventWindowImg: string) => {},
});

export default AppContext;