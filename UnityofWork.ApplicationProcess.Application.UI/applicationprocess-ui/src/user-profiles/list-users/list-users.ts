import { HttpClient } from 'aurelia-fetch-client';
import {Router} from 'aurelia-router';
import {inject} from 'aurelia-framework';

@inject(Router)
export class ListUsers {
    httpClientNew = null;
    userdata=null;
    router=null;
    constructor(router) {
        this.httpClientNew=new HttpClient();
        this.getUserProfiles();
        this.router=router;
    }

     getUserProfiles() {
         this.httpClientNew.fetch('https://localhost:44344/api/UserProfile/GetUsers')
          .then(response => response.json())
          .then(data => {
            this.userdata=data;
            
          });
        
      }

      onAdd($event){
        this.router.navigateToRoute('add');
      }
}