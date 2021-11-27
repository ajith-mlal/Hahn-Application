
import { HttpClient, json } from 'aurelia-fetch-client';

export class UserDetail {
    httpClientNew = null;
    userdata = null;
    userId = 0;
    coins=null;
    constructor() {
        this.httpClientNew = new HttpClient();
        this.getCoins();
    }


    activate(params) {
        this.userId = params.id;
         this.getUserDetails();
    }

     getCoins() {
         this.httpClientNew.fetch('https://api.coincap.io/v2/assets')
          .then(response => response.json())
          .then(data => {
            this.coins=data.data;

          });
         
      }

    async getUserDetails() {
        await this.httpClientNew.fetch('https://localhost:44344/api/UserProfile/GetUser?userId='+this.userId)
            .then(response => response.json())
            .then(data => {
                this.userdata = data;

            });
            this.userdata.assets=[];

            for(var userass in this.userdata.userAssets){

                var userasset=this.coins.filter(p=>p.id==this.userdata.userAssets[userass].assetId);
                
                this.userdata.assets.push(userasset[0]);

            }

    }

}

