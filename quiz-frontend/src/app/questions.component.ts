import { Component } from '@angular/core'
import { ActivatedRoute } from '@angular/router'
import { ApiService } from './api.services'

@Component({
    selector: 'questions',
    templateUrl: './questions.component.html'
})

export class QuestionsComponent{
    
    question = {}
    questions: any | undefined
    
    constructor (public api: ApiService, private route: ActivatedRoute){}
    
    ngOnInit() {
        var quizId = Number(this.route.snapshot.paramMap.get('quizId'))
        this.api.getQuestions(quizId).subscribe(res => {
            this.questions = res
        })
    }

    refresh(): void {
        window.location.reload();
    }
}

