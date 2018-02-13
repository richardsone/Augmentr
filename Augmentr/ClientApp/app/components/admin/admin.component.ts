import { Component, Inject } from '@angular/core';
import { AppComponent } from '../app/app.component';
import { AuthService } from '../services/auth.service';
import { TagService } from '../services/tags.service';
import { Tag } from '../models/tag';

@Component({
    selector: 'admin',
    templateUrl: './admin.component.html',
})
export class AdminComponent {
    public tags: Tag[];
    public password: string;
    public errorMsg: any;
    public loggingIn: boolean = true;
    public location: string;
    public content: string = 'fakeContent';

    constructor(private auth: AuthService,
        private tagService: TagService) {
        // This just shows that the app component's user object thing is getting passed in.
        console.log(auth.currentUser);
        // Do a API call to get the collection of tags that the current user can see
        // this.tags = array of tags, adjust the interface down there as needed.
        this.tags = [];
        this.tagService.getAllAdminTags().subscribe(data => {
            console.log('data: ', data);
            this.tags = data;
            console.log(this.tags);
        })
        // this.tags = [];
    }

    createTag() {
        // Use this.location and whatever other fields we wanna add to tag creation to call the API and then refetch the tags list, 
        // or append the new tag to the list by just getting back the Id, we should have all the other info.
        this.errorMsg = this.location + "was just created.";
        this.tags.push({ Id: this.tags.length + 1, Location: this.location });
        let newTag = {
            Location: this.location,
            Content: this.content
        }
        this.tagService.addTag(newTag).subscribe(
            data => {
                console.log('Data: ', data);
                this.tagService.getAllAdminTags().subscribe(tags => {
                    this.tags = tags.tags;
                });
            },
            error => {
                console.log('Error: ', error);
            }
        );
    }

    removeTag(deadTag: any) {
        // API call to kill that tag.
        console.log(deadTag);
        this.tagService.deleteAdminTag(deadTag.id).subscribe(response => {
            console.log(response);
            this.tagService.getAllAdminTags().subscribe(tags => {
                this.tags = tags;
            });
        });
    }

    clearError() {
        this.errorMsg = null;
    }
}
