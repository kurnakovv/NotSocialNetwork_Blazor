import React from "react";
import "./ShortPublication.css"
import UnfavoriteImg from "../../img/unfavorite.png";
import FavoriteImg from "../../img/favorite.png";
import CommentsImg from "../../img/comments.png";
import BookmarkImg from "../../img/bookmark.png";
import Button from "../Button";
import AppContext from "../../contexts/AppContext";


interface IShortPublicationState {
    isFavorite: boolean;
    favoriteImage: string;
}

export const ShortPublication: React.FC = ({ }) => {
    const [shortPublication, setShortPublication] = React.useState<IShortPublicationState>({
        isFavorite: false,
        favoriteImage: UnfavoriteImg
    });

    const context = React.useContext(AppContext);

    const setIsFavorite = (): void => {
        setShortPublication(prevState => ({ 
            ...prevState,
            isFavorite: !shortPublication.isFavorite,
            favoriteImage: !prevState.isFavorite ? FavoriteImg : UnfavoriteImg, // TODO: Fix now isFavorite is a old state.
         }));

         context.eventWindowIsVisible = true;
    }

    return (
        <>
            <div className="Container">
                <div className="Header">
                    <img className="UserLogo" src="https://media-exp1.licdn.com/dms/image/C560BAQH9Cnv1weU07g/company-logo_200_200/0/1575479070098?e=2159024400&v=beta&t=QM9VSoWVooxDwCONWh22cw0jBBlBPcBOqAxbZIE18jw" />
                    <p className="UserName">Author name</p>
                </div>
                <img className="MainImage" src="https://analyticsindiamag.com/wp-content/uploads/2020/10/7d744a684fe03ebc7e8de545f97739dd.jpg" />
                <div className="Panel">
                    <ul className="Panel">
                        <li className="RightButton"><img width={30} height={30} onClick={setIsFavorite} src={shortPublication?.favoriteImage} /></li>
                        <li className="RightButton"><img width={30} height={30} src={CommentsImg} /></li>
                        <li className="RightButton"><img width={30} height={30} src={BookmarkImg} /></li>
                        <li className="LeftButton"><Button text="More" reference="/" /></li>
                    </ul>
                </div>
                <div className="PublicationText">
                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting indus</p>
                </div>
            </div>
        </>
    );
}