import { IAuthor } from "./IAuthor";

export interface IPublication{
    id: string;
    text: string;
    author: IAuthor;
}