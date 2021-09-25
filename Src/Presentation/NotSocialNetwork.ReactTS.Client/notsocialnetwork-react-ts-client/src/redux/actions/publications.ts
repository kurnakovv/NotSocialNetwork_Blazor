import axios from "axios";
import IPublication from "../../types/Publication.interface";

export const getPublications = () => (dispatch: any) => {
    axios.get("https://localhost:5001/api/publication/index=0")
            .then(({ data }) => {
                dispatch(setPublications(data));
            });
}

export const setPublications = (publications: IPublication[]) => ({
    type: "SET_PUBLICATIONS",
    payload: publications,
})