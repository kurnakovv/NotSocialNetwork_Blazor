import React, { useState } from "react";
import "./ShortPublication.css"
import UnfavoriteImg from "../../img/unfavorite.png";
import FavoriteImg from "../../img/favorite.png";
import CommentsImg from "../../img/comments.png";
import BookmarkImg from "../../img/bookmark.png";
import Button from "../Button";


interface IShortPublicationState {
    isFavorite: boolean;
    favoriteImage: string;
}

export class ShortPublication extends React.Component<any, IShortPublicationState> {
    constructor(props: any){
        super(props);
        this.state = {
            isFavorite: false,
            favoriteImage: UnfavoriteImg,
        }
        this.setIsFavorite = this.setIsFavorite.bind(this);
    }

    setIsFavorite(): void{
        this.setState({isFavorite: !this.state.isFavorite});

        if(this.state.isFavorite){
            this.setState({favoriteImage: FavoriteImg});
        } else {
            this.setState({favoriteImage: UnfavoriteImg});
        }
    }

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
                            <li className="RightButton"><img width={30} height={30} onClick={this.setIsFavorite} src={this.state.favoriteImage}/></li>
                            <li className="RightButton"><img width={30} height={30} src={CommentsImg} /></li>
                            <li className="RightButton"><img width={30} height={30} src={BookmarkImg} /></li>
                            <li className="LeftButton"><Button text="More" reference="/"/></li>
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