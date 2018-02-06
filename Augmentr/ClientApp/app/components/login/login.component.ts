import { Component, Inject } from "@angular/core";
import { Router } from "@angular/router";
import { AppComponent } from "../app/app.component";
import { AuthService } from "../services/auth.service";
import { UserService } from "../services/user.service";
import { User } from "../models/User";

@Component({
  selector: "login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent {
  public email: string;
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
    if (this.email === "admin@augmentr.com" && this.password === "password") {
      this.auth.login({
        _id: "1",
        email: this.email,
        password: this.password,
        role: "admin",
        loggedIn: true,
        isAdmin: true
      });
    } else if (
      this.email === "explorer@augmentr.com" &&
      this.password === "betterpassword"
    ) {
      this.auth.login({
        _id: "1",
        email: this.email,
        password: this.password,
        role: "explorer",
        loggedIn: true,
        isAdmin: false
      });
    }
    // Do stuff with login email and password
    // For the time being im accepting anything and that makes the currentUser a logged in admin
    // API calls, promise then redirect or display error
    this.errorMsg = this.email + " : " + this.password;
    console.log(this.auth.currentUser);
  }

  register() {
    console.log("registering\n");
    let newUser: User = {
      email: this.email,
      password: this.password,
      role: "explorer",
      loggedIn: true,
      isAdmin: false
    }
    this.userService.register(newUser).subscribe(
      response => {
        console.log("you successfully registered!", "success");
        console.log(response);
        let test = response._body;
        test = atob(response._body);
        console.log(test);
        // this.router.navigate(["/login"]);
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
