import React from "react";

const AppContext = React.createContext({
    eventWindowIsVisible: false,
    setEventWindowIsVisible: (eventWindowIsVisible: boolean) => {},
});

export default AppContext;