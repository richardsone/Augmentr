import { Component } from '@angular/core';
import { UserService } from '../services/user.service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Route } from '@angular/router/src/config';

@Component({
    selector: 'explorer',
    templateUrl: './explorer.component.html'
})
export class ExplorerComponent {
    public currentCount = 0;
    public dangerZone : String;
    public queryResults : String;

    public incrementCounter() {
        this.currentCount++;
    }

    constructor(private userService : UserService, private router : Router){}

    public query(){
        this.userService.query(this.dangerZone).subscribe(
            res => {
                this.queryResults = res._body;
            }
        )
    }
}