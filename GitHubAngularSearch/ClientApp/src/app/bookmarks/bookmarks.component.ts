import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-bookmarks',
  templateUrl: './bookmarks.component.html',
  styleUrls: ['./bookmarks.component.css']
})
export class BookmarksComponent implements OnInit {


  bookmarks: Bookmark[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    const keys = Object.keys(localStorage)

    let newBookmarks: Bookmark[] = [];
    http.get<Bookmark[]>('https://localhost:7204/api/Repos/bookmark').subscribe(result => {
      keys.forEach(function (key) {
        var b: Bookmark = { id: key, url: localStorage[key] };
        newBookmarks.push(b);
      });
      this.bookmarks = newBookmarks;
      console.log(this.bookmarks);
    }, error => console.error(error));

  }

  ngOnInit(): void {

    }

}


interface Bookmark {
  id: string;
  url: string;

}
