const initialState: any = {
    publications: [],
}

const publications = (state = initialState, action: any) => {
    switch(action.type) {
        case "SET_PUBLICATIONS":
            return {
                ...state,
                publications: action.payload,
            };

        default:
            return state;
    }
}

export default publications;