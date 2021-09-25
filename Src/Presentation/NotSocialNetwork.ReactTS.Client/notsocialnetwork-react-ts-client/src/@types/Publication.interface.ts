import IAuthor from "./IAuthor.interface";

interface IPublication {
    id: string;
    author: IAuthor;
    text: string;
    imagePaths: string[];
}

export default IPublication;