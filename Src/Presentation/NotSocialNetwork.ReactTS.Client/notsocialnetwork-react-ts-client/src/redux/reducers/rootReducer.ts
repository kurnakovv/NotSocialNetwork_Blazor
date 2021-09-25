import { combineReducers } from "redux";

import publications from "./publications";

const rootReducer = combineReducers({
    publications,
});

export default rootReducer;