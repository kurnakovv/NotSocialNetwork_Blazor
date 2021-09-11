import React from "react";
import Button from "../Button/index";
import 'bootstrap/dist/css/bootstrap.min.css';

const Search: React.FC = ({ }) => {
    return <>
        <form className="d-flex">
            <input className="form-control me-2" type="search" placeholder="Search" aria-label="Search" />
            <Button
                text="Search"
                reference="/"
            />
        </form>
    </>
}

export default Search;