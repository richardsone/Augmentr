import { Component, Inject } from '@angular/core';
import { AppComponent } from '../app/app.component';
import { AuthService } from '../services/auth.service';
import { TagService } from '../services/tags.service';
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
    public content : string = 'fakeContent';
    
    constructor(private auth: AuthService,
                private tagService: TagService) {
        // This just shows that the app component's user object thing is getting passed in.
        console.log(auth.currentUser);
        // Do a API call to get the collection of tags that the current user can see
        // this.tags = array of tags, adjust the interface down there as needed.

        // this.tags = [
        //     { Id : 1, Location : "San Francisco" },
        //     { Id : 2, Location : "Houston" },
        //     { Id : 3, Location : "Milwaukee" },
        //     { Id : 4, Location : "Ney York" },
        //     { Id : 5, Location : "Denver" },
        //     { Id : 6, Location : "Drug house down the road" },
        //     { Id : 7, Location : "Questionable massage parlor" },
        //     { Id : 8, Location : "That chinese place with the dank dumplings" },
        //     { Id : 9, Location : "Your mom's place" },
        //     { Id : 10, Location : "Grand Canyon" }
        // ]

        this.tags = [];
    }

    createTag(){
        // Use this.location and whatever other fields we wanna add to tag creation to call the API and then refetch the tags list, 
        // or append the new tag to the list by just getting back the Id, we should have all the other info.
        this.errorMsg = this.location + "was just created.";
        this.tags.push({ Id: this.tags.length + 1, Location: this.location});
        let newTag = {
            Location: this.location,
            Content: this.content
        }
        this.tagService.addTag(newTag).subscribe(
            data => {
                console.log('Data: ', data);
            },
            error => {
                console.log('Error: ', error);
            }
        );
    }

    removeTag(deadTag : Tag){
        // API call to kill that tag.
        this.tags.splice(this.tags.indexOf(deadTag), 1)
    }

    clearError(){
        this.errorMsg = null;
    }
}
