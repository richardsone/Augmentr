import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import { User } from '../models/user';
import 'rxjs/add/operator/map';

@Injectable()
export class UserService {

    private headers = new Headers({ 'Content-Type': 'application/json', 'charset': 'UTF-8' });
    private options = new RequestOptions({ headers: this.headers });

    private url = 'api/v1/'

    constructor(private http: Http) { }

    register(user: User): Observable<any> {
        return this.http.post(this.url + 'auth/register', JSON.stringify(user), this.options);
    }

    login(credentials: any): Observable<any> {
        return this.http.post(this.url + 'auth/login', JSON.stringify(credentials), this.options);
    }

    countUsers(): Observable<any> {
        return this.http.get(this.url + 'users/count').map(res => res.json());
    }

    addUser(user: User): Observable<any> {
        return this.http.post(this.url + 'user', JSON.stringify(user), this.options);
    }

    getUser(user: User): Observable<any> {
        return this.http.get(this.url + `user/${user._id}`).map(res => res.json());
    }

    editUser(user: User): Observable<any> {
        return this.http.put(this.url + `user/${user._id}`, JSON.stringify(user), this.options);
    }

    query(queryString: String): Observable<any> {
        return this.http.post(this.url + 'user/query', JSON.stringify(queryString), this.options);
    }

    deleteUser(user: User): Observable<any> {
        return this.http.delete(this.url + `user/${user._id}`, this.options);
    }

}
