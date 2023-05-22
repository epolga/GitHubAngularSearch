import { Component, Inject } from '@angular/core';
import {
  MatButtonModule,
  MatFormFieldModule,
  MatInputModule,
  MatRippleModule,
  MatLabel,
  MatHint,
  MatInput,
  MatButton
} from '@angular/material';
import {FormControl, FormGroup} from "@angular/forms";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { BehaviorSubject } from 'rxjs';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  private subject = new BehaviorSubject<Repo[]>([]);
  searchTerm: string = '';

  repos$: Observable<Repo[]> = this.subject.asObservable();

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  form = new FormGroup({
    searchTerm: new FormControl('')
  })


  onClick() {
    const repos$ = this.http.get<Repo[]>('https://localhost:7204/api/Repos/' + this.searchTerm).subscribe(
      result => {
        this.subject.next(result);
      }
    );

  }

  bookmark(id: string, url: string) {
    localStorage.setItem(`ID${id}`, url);
    
    const urlToPutTo = `https://localhost:7204/api/Repos/${id}`;
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    const urlTopass = JSON.stringify(url);
    this.http.put<string>(urlToPutTo, urlTopass, httpOptions).subscribe();

  }
}
interface Repo {
  id: string;
  name: string;
  url: string;
  avatarUrl: number;
}
