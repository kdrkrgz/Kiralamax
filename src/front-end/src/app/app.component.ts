import { Component } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'KiralaMAX | Araç Kiralamanın Yeni Adresi';
  spinner:boolean=false;
  
  /**
   *
   */
  isLandingPage:boolean;
  constructor(private router: Router) {
    this.checkRoute();
  }

 // I know that's not best way to do it but i must :(
  checkRoute(){
    let url = window.location.href.split('/',4);
    const last = url[url.length -1 ];
    last === '' ? this.isLandingPage = true:this.isLandingPage = false;
  }
}
