import { IAuthor } from "../User/IAuthor";

export interface IPublication{
    id: string;
    text: string;
    author: IAuthor;
}