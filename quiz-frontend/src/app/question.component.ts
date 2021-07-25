import { Component } from '@angular/core'
import { ActivatedRoute } from '@angular/router';
import { ApiService } from './api.services'

@Component({
    selector: 'question',
    templateUrl: './question.component.html'
})

export class QuestionComponent{
    

    question : any = {};
    quizId: number = 0
    constructor (public api: ApiService, private route: ActivatedRoute){}

    ngOnInit(){
        this.quizId = Number(this.route.snapshot.paramMap.get('quizId'))
        this.api.questionSelected.subscribe(question => this.question = question)
    }

    post(question: any){
        question.quizId = this.quizId
        this.api.postQuestion(question)
        window.location.reload();
    }
    
}

