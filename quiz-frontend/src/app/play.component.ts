import { Component } from '@angular/core'
import { ApiService } from './api.services'

@Component({
    selector: 'play',
    templateUrl: './play.component.html',
    styleUrls: ['style.css']
})

export class PlayComponent{
    
    quizzes: any
    quizId: number = 0
    constructor (public api: ApiService){}
    
    ngOnInit() {
        this.api.getAllQuizzes().subscribe(res => {
            this.quizzes = res
        })
    }
    
    postFavorite(quizId: any){
        this.api.postFavorite(quizId)
    }
}



