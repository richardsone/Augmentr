import { Component, Inject } from "@angular/core";
import { Router } from "@angular/router";
import { AppComponent } from "../app/app.component";
import { AuthService } from "../services/auth.service";
import { UserService } from "../services/user.service";
import { User } from "../models/user";

@Component({
  selector: "login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent {
  public email: string;
  public name: string;
  public password: string;
  public errorMsg: any;
  public loggingIn: boolean = true;

  constructor(
    private auth: AuthService,
    private userService: UserService,
    private router: Router
  ) {
    console.log(auth.currentUser);
  }

  login() {
    // Do stuff with login email and password
    // For the time being im accepting anything and that makes the currentUser a logged in admin
    // API calls, promise then redirect or display error
    this.userService.login({Email: this.email, Password: this.password}).subscribe(
      response => {
        console.log("you've logged in");
        console.log(response._body);
        let loggedInUser = JSON.parse(response._body);
        this.auth.currentUser = loggedInUser as User;
        localStorage.setItem('token', response._body);
        console.log(this.auth.currentUser);
      }
    )

    this.errorMsg = this.email + " : " + this.password;
    console.log(this.auth.currentUser);
  }

  register() {
    console.log("registering\n");
    let newUser: User = {
      Email: this.email,
      Name: this.name,
      Password: this.password,
      Role: 1,
      Tags: []
    }
    this.userService.register(newUser).subscribe(
      response => {
        console.log("you successfully registered!", "success");
        console.log(response);
        let test = response._body;
        let myObject = JSON.parse(test);
        console.log(myObject);
        this.router.navigate(["/login"]);
      },
      error => {
          console.log("email already exists", "danger")
          console.log(error);
      }
    );
  }

  //   register() {
  //     // Do registration stuff
  //     // API calls, promise then redirect or display error
  //     this.errorMsg = this.email + " : " + this.password;
  //   }

  clearError() {
    this.errorMsg = null;
  }
}
