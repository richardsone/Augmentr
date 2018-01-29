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
        // This just shows that the app component's user object thing is getting passed in.
        console.log(this.parent.currentUser)
    }

    login(){
        // Do stuff with login email and password
        // For the time being im accepting anything and that makes the currentUser a logged in admin
        // API calls, promise then redirect or display error
        this.errorMsg = this.email + " : " + this.password;
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
