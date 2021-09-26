export interface ILoginRequest {
    email: string;
}

export interface ILoginResponse {
    token: string;
    authorId: string;
}

export interface IRegistrationRequest {
    name: string;
    email: string;
    image?: string;
}

export interface IRegistrationResponse {
    id: string;
    name: string;
    email: string;
    image: string;
}