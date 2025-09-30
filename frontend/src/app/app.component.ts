import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MovieService } from './services/movie.service';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
  <header>
    <h1>🎬 MovieDB</h1>
    <div class="auth">
      <ng-container *ngIf="!auth.token; else logged">
        <input [(ngModel)]="username" placeholder="user">
        <input [(ngModel)]="password" placeholder="pass" type="password">
        <button (click)="onLogin()">Login</button>
        <button (click)="onRegister()">Register</button>
      </ng-container>
      <ng-template #logged>
        <span>👋 {{auth.username}}</span>
        <button (click)="onLogout()">Logout</button>
      </ng-template>
    </div>
  </header>

  <div class="container">
    <div class="controls">
      <input [(ngModel)]="q" placeholder="Search title..." (keyup.enter)="search()">
      <select [(ngModel)]="genre">
        <option value="">All Genres</option>
        <option *ngFor="let g of genres" [value]="g">{{g}}</option>
      </select>
      <select [(ngModel)]="sort">
        <option value="year_desc">Newest</option>
        <option value="title_asc">Title A→Z</option>
        <option value="title_desc">Title Z→A</option>
        <option value="rating_desc">Rating High→Low</option>
        <option value="rating_asc">Rating Low→High</option>
        <option value="year_asc">Oldest</option>
      </select>
      <button class="primary" (click)="search()">Apply</button>
    </div>

    <div class="pager">
      <button (click)="prev()" [disabled]="page<=1">Prev</button>
      <span>Page {{page}} / {{pages}}</span>
      <button (click)="next()" [disabled]="page>=pages">Next</button>
    </div>

    <div class="grid">
      <div class="card" *ngFor="let m of movies">
        <h3>{{m.title}}</h3>
        <div>
          <span class="badge">{{m.genre}}</span>
          <span class="badge">⭐ {{m.rating}}</span>
          <span class="badge">{{m.year}}</span>
        </div>
        <p><a class="link" href="#" (click)="open(m.id); $event.preventDefault()">Details & Reviews →</a></p>
      </div>
    </div>

    <div class="pager">
      <button (click)="prev()" [disabled]="page<=1">Prev</button>
      <span>Page {{page}} / {{pages}}</span>
      <button (click)="next()" [disabled]="page>=pages">Next</button>
    </div>

    <div *ngIf="selected" class="movie-detail">
      <h2>{{selected.title}} <small>({{selected.year}})</small></h2>
      <div><span class="badge">{{selected.genre}}</span> <span class="badge">⭐ {{selected.rating}}</span></div>

      <h3>Add Review</h3>
      <form (submit)="submitReview($event)">
        <input [(ngModel)]="text" name="text" placeholder="Your review..." style="width:320px">
        <select [(ngModel)]="stars" name="stars">
          <option *ngFor="let s of [5,4,3,2,1]" [value]="s">{{s}} ★</option>
        </select>
        <button class="primary">Submit</button>
      </form>

      <h3>Reviews</h3>
      <div *ngFor="let r of reviews" class="review">
        <strong>{{r.author}}</strong> — {{r.stars}} ★
        <div>{{r.text}}</div>
        <small>{{r.createdAt | date:'medium'}}</small>
      </div>
    </div>
  </div>
  `
})
export class AppComponent implements OnInit {
  movies:any[]=[]; total=0; page=1; pageSize=12; pages=1;
  q=''; genre=''; sort='year_desc';
  genres=['Action','Adventure','Animation','Comedy','Crime','Drama','Fantasy','Horror','Mystery','Romance','Sci-Fi','Thriller'];

  selected:any=null; reviews:any[]=[]; text=''; stars=5;
  username='demo'; password='demo123';

  constructor(public auth:AuthService, private api:MovieService){}
  ngOnInit(){ this.load(); }

  splitSort(){ const [s,d]=this.sort.split('_'); return { sort:s, dir:d }; }

  load(){
    const {sort,dir}=this.splitSort();
    this.api.getPaged(this.page,this.pageSize,sort,dir).subscribe(res=>{
      this.movies=res.items; this.total=res.total; this.pages=Math.max(1, Math.ceil(this.total/this.pageSize));
      this.selected=null;
    });
  }

  search(){
    const {sort,dir}=this.splitSort();
    this.api.search(this.q,this.genre,sort,dir).subscribe(res=>{
      this.movies=res; this.total=res.length; this.page=1; this.pages=1; this.selected=null;
    });
  }

  next(){ if(this.page<this.pages){ this.page++; this.load(); } }
  prev(){ if(this.page>1){ this.page--; this.load(); } }

  open(id:number){
    this.api.getById(id).subscribe(res=>{ this.selected=res; this.reviews=res.reviews||[]; });
  }

  submitReview(e:Event){
    e.preventDefault();
    if(!this.selected) return;
    if(!this.auth.token){ alert('Please login first'); return; }
    this.api.addReview(this.selected.id, this.text, this.stars).subscribe(_=>{
      this.open(this.selected.id); this.text=''; this.stars=5;
    });
  }

  onLogin(){
    this.auth.login(this.username,this.password).subscribe(res=>{
      this.auth.setSession(this.username, res.token);
    });
  }
  onRegister(){
    this.auth.register(this.username,this.password).subscribe(_=> this.onLogin());
  }
  onLogout(){ this.auth.logout(); }
}
