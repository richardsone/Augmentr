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
        this.auth.login({
            _id: '1',
            email: 'stuff@stuff.com',
            password: 'password',
            role: 'explorer',
            loggedIn: true,
            isAdmin: false
        }); 
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
