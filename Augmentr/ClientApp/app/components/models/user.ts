export interface User {
    _id?: string,
    Email: string,
    Name: string,
    Password: string,
    Role: string,
    loggedIn: boolean,
    isAdmin: boolean,
    Tags: any[]
}
