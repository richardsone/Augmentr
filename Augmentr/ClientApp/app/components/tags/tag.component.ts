import { Component, Inject } from '@angular/core';
import { AppComponent } from '../app/app.component';
import { AuthService } from '../services/auth.service';
import { Tag } from '../models/tag';

@Component({
    selector: 'tags',
    templateUrl: './tag.component.html',
    styleUrls: ['./tag.component.css']
})
export class TagComponent {
    public tags : Tag[];
    public password : string;
    public errorMsg : any;
    public loggingIn : boolean = true;
    public location : string;
    
    constructor(private auth: AuthService) {
        // This just shows that the app component's user object thing is getting passed in.
        console.log(auth.currentUser);
        // Do a API call to get the collection of tags that the current user can see
        // this.tags = array of tags, adjust the interface down there as needed.

        this.tags = [
            { id : 1, location : "San Francisco" },
            { id : 2, location : "Houston" },
            { id : 3, location : "Milwaukee" },
            { id : 4, location : "Ney York" },
            { id : 5, location : "Denver" },
            { id : 6, location : "Drug house down the road" },
            { id : 7, location : "Questionable massage parlor" },
            { id : 8, location : "That chinese place with the dank dumplings" },
            { id : 9, location : "Your mom's place" },
            { id : 10, location : "Grand Canyon" }
        ]
    }

    createTag(){
        // Use this.location and whatever other fields we wanna add to tag creation to call the API and then refetch the tags list, 
        // or append the new tag to the list by just getting back the Id, we should have all the other info.
        this.errorMsg = this.location + "was just created.";
        this.tags.push({ id: 666, location: this.location})
    }

    removeTag(deadTag : Tag){
        // API call to kill that tag.
        this.tags.splice(this.tags.indexOf(deadTag), 1)
    }

    clearError(){
        this.errorMsg = null;
    }
}
