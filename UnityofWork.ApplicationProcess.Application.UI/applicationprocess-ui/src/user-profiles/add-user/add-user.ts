import { inject, NewInstance } from 'aurelia-dependency-injection';
import { ValidationController, validateTrigger } from 'aurelia-validation';
import { ValidationRules } from 'aurelia-validation'
import { HttpClient, json } from 'aurelia-fetch-client';
import { UserModel,UserAssetsModel } from '../models/user-model';
import {Router} from 'aurelia-router';

@inject(NewInstance.of(ValidationController))
@inject(NewInstance.of(Router))
export class AddUser {
  usermodel: UserModel=new UserModel();

  controller = null;
  httpClient = null;
  httpClientNew = null;
  coins;
  selectedAssets: any = [];

  constructor(controller,_router:Router) {
    let http = new HttpClient();
    let httpnew = new HttpClient();

    this.httpClient = http;


    this.controller = controller;

    this.httpClientNew = httpnew;


    ValidationRules
      .ensure('firstName').required().minLength(3).withMessage('First Name must at least be 3 chars long.')
       .ensure('lastName').required().minLength(3).withMessage('Last Name must at least be 3 chars long.')
      .ensure('age').required().min(18).withMessage('Age must be atleast 18')
      .ensure('email').required().email().withMessage('Email must be valid')
      .ensure('userAddress.addressLine1').required().withMessage('Address Line 1 cannot be empty.')
      .ensure('userAddress.addressLine2').required().withMessage('Address Line 2 cannot be empty.')
      .ensure('userAddress.addressLine3').required().withMessage('Address Line 3 cannot be empty.')
      .ensure('userAddress.postalCode').required().withMessage('Postal Code cannot be empty.') 
      .on(this.usermodel);


    this.getCoins();
  }

  onReset() {
    this.controller.reset();
    this.usermodel=new UserModel();
  }

  createUser() {

    this.usermodel.userAssets=[];

    this.usermodel.selectedAssets.forEach(element => {
      let item = new UserAssetsModel();
      item.assetId = element;

      this.usermodel.userAssets.push(item)
    });

    console.log(this.usermodel);

     this.httpClient.fetch('https://localhost:44344/api/userprofile/createuser',
       {
         method: 'post',
         body: json(this.usermodel),
         headers: {
           "content-type": "application/json; charset=utf-8"
         }
       }).then(data => {
        this.usermodel=new UserModel();
       });
  };

  async getCoins() {
    await this.httpClientNew.fetch('https://api.coincap.io/v2/assets')
      .then(response => response.json())
      .then(data => {
        this.coins = data.data;
      });
  }

}
