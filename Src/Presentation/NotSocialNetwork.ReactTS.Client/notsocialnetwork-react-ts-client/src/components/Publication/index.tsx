import React from "react";
import "./Publication.css"
import UnfavoriteImg from "../../img/unfavorite.png";
import FavoriteImg from "../../img/favorite.png";
import CommentsImg from "../../img/comments.png";
import BookmarkImg from "../../img/bookmark.png";
import Button from "../Button";
import AppContext from "../../contexts/AppContext";
import IPublication from "../../types/Publication.interface";


interface IPublicationState {
    isFavorite: boolean;
    favoriteImage: string;
}

export const Publication: React.FC<IPublication> = ({ text, author }) => {
    const [publication, setPublication] = React.useState<IPublicationState>({
        isFavorite: false,
        favoriteImage: UnfavoriteImg
    });

    const context = React.useContext(AppContext);

    React.useEffect(() => {
        if(publication.isFavorite) {
            context.setEventWindowText("Publication added to:");
            context.setEventWindowImg(FavoriteImg);
        } else {
            context.setEventWindowText("Publication deleted from:");
            context.setEventWindowImg(UnfavoriteImg);
        }
    }, [publication.isFavorite])

    const setIsFavorite = (): void => {
        setPublication(prevState => { 
            return {
                ...prevState,
                isFavorite: !publication.isFavorite,
            }
         });

        setPublication(prevState => { 
            return {
                ...prevState,
                favoriteImage: prevState.isFavorite ? FavoriteImg : UnfavoriteImg,
            }
         });

        context.setEventWindowIsVisible(true);
    }

    return (
        <>
            <div className="Container">
                <div className="Header">
                    <img className="UserLogo" src="https://media-exp1.licdn.com/dms/image/C560BAQH9Cnv1weU07g/company-logo_200_200/0/1575479070098?e=2159024400&v=beta&t=QM9VSoWVooxDwCONWh22cw0jBBlBPcBOqAxbZIE18jw" />
                    <p className="UserName">{author.name}</p>
                </div>
                <img className="MainImage" src="data:image/png;base64,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" />
                <div className="Panel">
                    <ul className="Panel">
                        <li className="RightButton"><img width={30} height={30} onClick={setIsFavorite} src={publication?.favoriteImage} /></li>
                        <li className="RightButton"><img width={30} height={30} src={CommentsImg} /></li>
                        <li className="RightButton"><img width={30} height={30} src={BookmarkImg} /></li>
                        <li className="LeftButton"><Button text="More" reference="/" /></li>
                    </ul>
                </div>
                <div className="PublicationText">
                    <p>{text}</p>
                </div>
            </div>
        </>
    );
}