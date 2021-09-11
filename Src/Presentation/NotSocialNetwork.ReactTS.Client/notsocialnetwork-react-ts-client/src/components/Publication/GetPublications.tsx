import React from "react";

export interface IAuthor {
    id: string,
    name: string
}

interface IPublication {
    id: string,
    text: string,
}

interface IProps {
    publications: Array<IPublication>;
}

export const GetPublications: React.FC<IProps> = ({ publications }) => {
    return (
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Text</th>
                </tr>
            </thead>
            <tbody>
                {publications && publications.map(publication => {
                    return <tr>
                        <td>{publication.id}</td>
                        <td>{publication.text}</td>
                    </tr>
                })}
            </tbody>
        </table>
    );
}