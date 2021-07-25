import { Component } from '@angular/core'
import { ApiService } from './api.services'

@Component({
    selector: 'quiz',
    templateUrl: './quiz.component.html',
})

export class QuizComponent{
    

    quiz : any = {};
    
    constructor (public api: ApiService){}
    
    ngOnInit(){
        this.api.quizSelected.subscribe(quiz => this.quiz= quiz)
    }

    refresh(): void {
        window.location.reload();
    }
}

