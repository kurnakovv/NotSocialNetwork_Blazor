import { combineReducers } from "redux";

import publications from "./publications";
import authPanel from "./authPanel";

const rootReducer = combineReducers({
    publications,
    authPanel,
});

export default rootReducer;