import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { User } from "../models/user";
import { JwtHelper } from "angular2-jwt";

import { UserService } from "./user.service";

@Injectable()
export class AuthService {
  private _currentUser: User;

  get currentUser(): User {
    return this._currentUser;
  }

  set currentUser(newUser: User) {
    console.log('new user', newUser)
    this._currentUser = newUser as User;
  }

  jwtHelper: JwtHelper = new JwtHelper();

  constructor(private userService: UserService, private router: Router) {
    console.log("auth service constructed");
    this._currentUser = {
      _id: "",
      Email: "",
      Name: '',
      Password: "",
      Role: 0,
      Tags: []
    };
    const token = localStorage.getItem("token");
    if (token) {
      this.jwtHelper.decodeToken(token);
    } else {
      const newVisitor = {
        _id: "",
        Email: "",
        Name: '',
        Password: "",
        Role: 0,
        Tags: []
      };
      this._currentUser = newVisitor;
      localStorage.setItem("token", JSON.stringify(newVisitor));
    }
  }

  login(user: any) {
    console.log(user);
    this._currentUser = user;
    return this.userService.login(user).subscribe(res => {
        console.log('we have a response');
        console.log(res);
    });
    // return this.userService.login(user).map(res => res.json()).map(
    //     res => {
    //         localStorage.setItem('token', res.token);
    //         this.decodeToken(user);
    //         return this._currentUser.loggedIn;
    //     }
    // );
  }

  logout() {
    localStorage.removeItem("token");
    this._currentUser = {
      _id: "",
      Email: "",
      Name: '',
      Password: "",
      Role: 0,
      Tags: []
    };
    localStorage.setItem("token", JSON.stringify(this._currentUser));
    this.router.navigate(["/"]);
  }

  private decodeToken(decodedUser: any) {
    this._currentUser._id = decodedUser._id;
    this._currentUser.Email = decodedUser.email;
    this._currentUser.Password = decodedUser.password;
    this._currentUser.Role = decodedUser.role;
    this._currentUser.Tags = decodedUser.Tags;
  }
}
