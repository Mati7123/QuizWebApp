import { Component } from '@angular/core'
import { ApiService } from './api.services'

@Component({
    selector: 'quizzes',
    templateUrl: './quizzes.component.html',
    styleUrls: ['style.css'],
})

export class QuizzesComponent{
    
    quiz: any = {}
    quizzes: any | undefined
    
    constructor (public api: ApiService){}
    
    ngOnInit() {
        console.log("once")
        this.api.getQuizzes().subscribe(res => {
            this.quizzes = res
        })
    }

    refresh(): void {
        window.location.reload();
    }
}

