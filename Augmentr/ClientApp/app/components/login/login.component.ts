import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from '../app/app.component';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    public email : string;
    public password : string;
    public errorMsg : any;
    public loggingIn : boolean = true;
    
    constructor(private auth: AuthService,
                private router: Router){
        console.log(auth.currentUser);
    }

    login(){
        if (this.email === 'admin@augmentr.com' && this.password === 'password') {
            this.auth.login({
                _id: '1',
                email: this.email,
                password: this.password,
                role: 'admin',
                loggedIn: true,
                isAdmin: true
            }); 
        } else if (this.email ==='explorer@augmentr.com' && this.password === 'betterpassword') {
            this.auth.login({
                _id: '1',
                email: this.email,
                password: this.password,
                role: 'explorer',
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

    register(){
        // Do registration stuff
        // API calls, promise then redirect or display error
        this.errorMsg = this.email + " : " + this.password;
    }

    clearError(){
        this.errorMsg = null;
    }

}
