import { Component } from '@angular/core';
import { UserService } from '../services/user.service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Route } from '@angular/router/src/config';
import { Console } from '@angular/core/src/console';

@Component({
    selector: 'explorer',
    templateUrl: './explorer.component.html'
})
export class ExplorerComponent {
    public currentCount = 0;
    public dangerZone : String;
    public queryResults : String;
    private injectionTriggers = ["\"", "select", "drop", "=", "<", ">"]; 

    public incrementCounter() {
        this.currentCount++;
    }

    public detectQuotes(event : any){
        // This could be way more intensive but time contraints.
        if(this.containsAny(this.dangerZone, this.injectionTriggers)){
            this.dangerZone = "";
        }
    }

    private containsAny(str : String, substrings : Array<string>) {
        var substring : string;
        for (var i = 0; i != substrings.length; i++) {
            substring = substrings[i];
            if (str.toLowerCase().includes(substring)) {
                return true;
            }
        }
        return false; 
    }

    constructor(private userService: UserService, private router: Router) {}

    public query(){
        this.userService.query(this.dangerZone).subscribe(
            res => {
                this.queryResults = res._body
            },
            error => {
                this.queryResults = error.statusText;
            }
        );
    }
}