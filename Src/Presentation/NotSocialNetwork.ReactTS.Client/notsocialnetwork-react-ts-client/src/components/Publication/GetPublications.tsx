import React from "react";

export interface IAuthor {
    id: string,
    name: string
}

interface IPublication {
    id: string,
    text: string,
}

interface IProps{
    publications: Array<IPublication>;
}

export class GetPublications extends React.Component<IProps> {
    render() {
        return (
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Text</th>
                    </tr>
                </thead>
                <tbody>
                    {this.props.publications && this.props.publications.map(publication => {
                        return <tr>
                            <td>{publication.id}</td>
                            <td>{publication.text}</td>
                        </tr>
                    })}
                </tbody>
            </table>
        );
    }
}