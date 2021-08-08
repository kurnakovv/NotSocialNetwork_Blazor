import React from "react";
import "./ShortPublication.css"
import UnfavoriteImg from "../../img/unfavorite.png";
import FavoriteImg from "../../img/favorite.png";
import CommentsImg from "../../img/comments.png";
import BookmarkImg from "../../img/bookmark.png";

export class ShortPublication extends React.Component {
    render() {
        return (
            <>
                <div className="Container">
                    <div className="Header">
                        <img className="UserLogo" src="https://media-exp1.licdn.com/dms/image/C560BAQH9Cnv1weU07g/company-logo_200_200/0/1575479070098?e=2159024400&v=beta&t=QM9VSoWVooxDwCONWh22cw0jBBlBPcBOqAxbZIE18jw"/>
                        <p className="UserName">Author name</p>
                    </div>
                    <img className="MainImage" src="https://analyticsindiamag.com/wp-content/uploads/2020/10/7d744a684fe03ebc7e8de545f97739dd.jpg"/>
                    <div className="Panel">
                        <ul className="Panel">
                            <li className="RightButton"><img width={30} height={30} src={UnfavoriteImg}/></li>
                            <li className="RightButton"><img width={30} height={30} src={CommentsImg} /></li>
                            <li className="RightButton"><img width={30} height={30} src={BookmarkImg} /></li>
                            <li className="LeftButton"><a className="btn btn-outline-info">More</a></li>
                        </ul>
                    </div>
                    <div className="PublicationText">
                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting indus</p>
                    </div>
                </div>
            </>
        );
    }
}