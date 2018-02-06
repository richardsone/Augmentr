export interface User {
    _id?: string,
    email: string,
    password: string,
    role: string,
    loggedIn: boolean,
    isAdmin: boolean
}
