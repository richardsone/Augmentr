import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user';
import { JwtHelper } from 'angular2-jwt';

import { UserService } from './user.service';

@Injectable()
export class AuthService {
    private _currentUser : User;

    get currentUser(): User {
        return this._currentUser;
    }

    set currentUser(newUser: User) {
        this._currentUser 
    }

    jwtHelper: JwtHelper = new JwtHelper();

    constructor(private userService: UserService,
                private router: Router) {
        const token = localStorage.getItem('token');
        if (token) {
            this.decodeToken(token);
        } else {
            const newVisitor = {
                _id: '',
                email: '',
                password: '',
                role: 'visitor',
                loggedIn: false,
                isAdmin: false
            }
            this._currentUser = newVisitor;
            localStorage.setItem('token', JSON.stringify(newVisitor));
        }
    }

    login(user: User) {
        return this.userService.login(user).map(res => res.json()).map(
            res => {
                localStorage.setItem('token', res.token);
                this.decodeToken(user);
                return this._currentUser.loggedIn;
            }
        );
    }

    logout() {
        localStorage.removeItem('token');
        this._currentUser = { 
            _id: '', 
            email: '', 
            password: '', 
            role: 'visitor', 
            loggedIn: false, 
            isAdmin: false 
        };
        localStorage.setItem('token', JSON.stringify(this._currentUser));
        this.router.navigate(['/']);
    }

    decodeToken(decodedUser: any) {
        this._currentUser._id = decodedUser._id;
        this._currentUser.email = decodedUser.email;
        this._currentUser.isAdmin = decodedUser.isAdmin;
        this._currentUser.password = decodedUser.password;
        this._currentUser.role = decodedUser.role;
        this._currentUser.loggedIn = decodedUser.loggedIn;
    }
}
