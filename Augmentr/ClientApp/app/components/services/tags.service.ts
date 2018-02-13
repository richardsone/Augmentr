import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions } from "@angular/http";

import { Observable } from "rxjs/Observable";
import { User } from "../models/user";
import { Tag } from "../models/tag";
import { AuthService } from "./auth.service";
import "rxjs/add/operator/map";

@Injectable()
export class TagService {
  private headers = new Headers({
    "Content-Type": "application/json",
    charset: "UTF-8"
  });
  private options = new RequestOptions({ headers: this.headers });

  private url = "api/v1/";

  constructor(private http: Http,
              private auth: AuthService) {}

  getAllTags(): Observable<any> {
    let userEmail = this.auth.currentUser.Email;
    return this.http.get(this.url + `user/tags/user/${userEmail}`).map(res=> res.json());
  }

  getAllAdminTags(): Observable<any> {
    return this.http.get(this.url + 'admin/tags/alltags').map(res=> res.json());
  }

  countUsers(): Observable<any> {
    return this.http.get(this.url + "users/count").map(res => res.json());
  }

  addTag(tag: any): Observable<any> {
    let now = Date.now().toString;
    let post = {
      Token: localStorage.getItem('token'),
      UserEmail: this.auth.currentUser.Email,
      Location: tag.Location,
      Content: tag.Content,
      TimePosted: now
    }
    console.log("What you're sending: " + JSON.stringify(post));
    return this.http.post(
      this.url + "user/tags",
      JSON.stringify(post),
      this.options
    );
  }

  deleteTag(id: number): Observable<any> {
    console.log("deleteTag: ", id);
    let evansBody = {
      Token: localStorage.getItem('token'),
      Id: id
    }
    return this.http.post(
      this.url + `user/tags/delete`,
      JSON.stringify(evansBody),
      this.options
    );
  }

  // getUser(user: User): Observable<any> {
  //   return this.http.get(this.url + `user/${user._id}`).map(res => res.json());
  // }

  // editUser(user: User): Observable<any> {
  //   return this.http.put(
  //     this.url + `user/${user._id}`,
  //     JSON.stringify(user),
  //     this.options
  //   );
  // }

  // deleteUser(user: User): Observable<any> {
  //   return this.http.delete(this.url + `user/${user._id}`, this.options);
  // }
}
