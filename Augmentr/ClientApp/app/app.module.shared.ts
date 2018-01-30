import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { TagComponent } from './components/tags/tag.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';

import { AuthService } from './components/services/auth.service';
import { AuthGuardAdmin } from './components/services/auth-guard-admin.service';
import { AuthGuardExplorer } from './components/services/auth-guard-explorer.service';

import { ExplorerComponent } from './components/explorer/explorer.component';

import { User } from './components/models/user';
import { UserService } from './components/services/user.service';
import { Expansion } from '@angular/compiler';
import { AdminComponent } from './components/admin/admin.component';

@NgModule({
    declarations: [
        AppComponent,
        AdminComponent,
        ExplorerComponent,
        NavMenuComponent,
        LoginComponent,
        TagComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'login', component: LoginComponent },
            { path: 'tags', component: TagComponent, canActivate: [AuthGuardExplorer] },
            { path: 'explorer', component: ExplorerComponent, canActivate: [AuthGuardExplorer] },
            { path: 'admin', component: AdminComponent, canActivate: [AuthGuardAdmin] },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        AuthService,
        UserService,
        AuthGuardAdmin,
        AuthGuardExplorer
    ]
})
export class AppModuleShared {
}
