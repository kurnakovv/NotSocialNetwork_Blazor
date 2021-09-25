import axios from "axios";

export const getPublications = () => (dispatch: any) => {
    axios.get("https://localhost:5001/api/publication/index=0")
            .then(({ data }) => {
                dispatch(setPublications(data));
            });
}

export const setPublications = (publications: any) => ({
    type: "SET_PUBLICATIONS",
    payload: publications,
})