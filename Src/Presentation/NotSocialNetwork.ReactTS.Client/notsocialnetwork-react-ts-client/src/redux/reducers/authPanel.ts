const initialState = {
    isAuthPanelOpen: false,
}

const authPanel = (state = initialState, action: any) => {
    switch(action.type) {
        case "SET_IS_AUTH_PANEL_OPEN":
            return {
                ...state,
                isAuthPanelOpen: !state.isAuthPanelOpen,
            };
        default:
            return state;
    }
}

export default authPanel;