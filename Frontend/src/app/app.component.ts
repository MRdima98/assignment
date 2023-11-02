import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
    private todos = 'https://localhost:7096/GetAllTasks';
    private todosByUser = 'https://localhost:7096/GetTasksByUser';
    userId: number | null = null;
    limit: number | null = null;
    offset: number | null = null;
    data = [];

    constructor(private http: HttpClient) { }

    fetchAllTodos(limit?: number | null, offset?: number | null, userId?: number | null): Observable<any> {
      let link: string = this.todos;

      if (userId) {
          link = this.todosByUser;
          link = link.concat(`?userID=${userId}`);
      }
      if (limit && link.includes("?")) {
          link = link.concat(`&limit=${limit}`);
      } else if (limit) {
          link = link.concat(`?limit=${limit}`);
      }

      if (offset && link.includes("?")) {
          link = link.concat(`&offset=${offset}`);
      } else if(offset) {
          link = link.concat(`?offset=${offset}`);
      }
      return this.http.get(link);
    }

    ngOnInit(): void {
      this.fetchAllTodos().subscribe((result) => {
        this.data = result;
      });
    }

    handleButtonClick(): void {
        this.fetchAllTodos(this.limit, this.offset, this.userId).subscribe((result) => {
          this.data = result;
        }, (error) => {
          if (error.status == 404) {
            this.data = [];
          }
        });
    }

    setUserId(event: Event): void {
        const input = parseInt((event.target as HTMLInputElement).value);
        if (Number.isNaN(input)){
            this.userId = null;
        } else {
            this.userId = parseInt((event.target as HTMLInputElement).value);
        }
    }

    setLimit(event: Event): void {
        const input = parseInt((event.target as HTMLInputElement).value);
        if (Number.isNaN(input)){
            this.limit = null;
        } else {
            this.limit = parseInt((event.target as HTMLInputElement).value);
        }
    }

    setOffset(event: Event): void {
        const input = parseInt((event.target as HTMLInputElement).value);
        if (Number.isNaN(input)){
            this.offset = null;
        } else {
            this.offset = parseInt((event.target as HTMLInputElement).value);
        }
    }
}
