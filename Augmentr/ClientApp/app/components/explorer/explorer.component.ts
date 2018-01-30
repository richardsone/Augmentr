import { Component } from '@angular/core';

@Component({
    selector: 'explorer',
    templateUrl: './explorer.component.html'
})
export class ExplorerComponent {
    public currentCount = 0;

    public incrementCounter() {
        this.currentCount++;
    }
}