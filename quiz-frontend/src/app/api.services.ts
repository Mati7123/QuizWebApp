import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { Subject} from 'rxjs'

@Injectable()
export class ApiService {

    private selectedQuestion = new Subject<any>();
    questionSelected = this.selectedQuestion.asObservable();

    private selectedQuiz = new Subject<any>();
    quizSelected = this.selectedQuiz.asObservable();

    constructor(private http: HttpClient) {}

    getQuestions(quziId: number){
        return this.http.get(`http://localhost:5001/api/questions/${quziId}`);
    }

    getQuizzes(){
        return this.http.get('http://localhost:5001/api/quizzes');
    }

    getAllQuizzes(){
        return this.http.get('http://localhost:5001/api/quizzes/all');
    }

    getFavoriteQuizzes(){
        return this.http.get('http://localhost:5001/api/quizzes/my');
    }

    postQuestion(question: any){
        this.http.post('http://localhost:5001/api/questions', question).subscribe(res => {
            console.log(res)
        })
    }

    putQuestion(question: any) {
        this.http.put(`http://localhost:5001/api/questions/${question.id}`, question).subscribe(res => {
            console.log(res)
        }) 
    }
    deleteQuestion(question: any){
        this.http.delete(`http://localhost:5001/api/questions/${question.id}`, question).subscribe(res => {
            console.log(res)
        }) 
    }

    postQuiz(quiz: any){
        this.http.post('http://localhost:5001/api/quizzes', quiz).subscribe(res => {
            console.log(res)
        })
    }
    postFavorite(quizId: number){
        this.http.post(`http://localhost:5001/api/quizzes/my/${quizId}`, quizId).subscribe(res => {
            console.log(res)
        })
    }

    deleteFavorite(quiz:any){
        this.http.delete(`http://localhost:5001/api/quizzes/my/${quiz.id}`, quiz).subscribe(res => {
            console.log(res)
        })
    }

    putQuiz(quiz: any) {
        this.http.put(`http://localhost:5001/api/quizzes/${quiz.id}`, quiz).subscribe(res => {
            console.log(res)
        }) 
    }
    deleteQuiz(quiz: any){
        this.http.delete(`http://localhost:5001/api/quizzes/${quiz.id}`, quiz).subscribe(res => {
            console.log(res)
        }) 
    }
    
    selectQuestion(question: any) {
        this.selectedQuestion.next(question)
    }

    selectQuiz(quiz: any) {
        this.selectedQuiz.next(quiz)
    }
}