import { Component } from '@angular/core'
import { ApiService } from './api.services'

@Component({
    selector: 'favorite',
    templateUrl: './favorite.component.html'
})

export class FavoriteComponent{
    
    quiz: any = {}
    quizzes: any | undefined
    
    constructor (public api: ApiService){}
    
    ngOnInit() {
        this.api.getFavoriteQuizzes().subscribe(res => {
            this.quizzes = res
        })
    }

    refresh(): void {
        window.location.reload();
    }
}

