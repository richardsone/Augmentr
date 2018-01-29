import { Component } from '@angular/core';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    // I put the currentUser out here in the App level since it'll carry across the entire app
    public currentUser: Object = {};
    
    constructor(){
        this.currentUser = { level: 'visitor', loggedIn: false};
    }
}
