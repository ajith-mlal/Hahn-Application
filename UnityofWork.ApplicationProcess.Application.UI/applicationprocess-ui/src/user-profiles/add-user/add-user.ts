import { inject, NewInstance } from 'aurelia-dependency-injection';
import { ValidationController, validateTrigger } from 'aurelia-validation';
import { ValidationRules } from 'aurelia-validation'
import { HttpClient, json } from 'aurelia-fetch-client';


@inject(NewInstance.of(ValidationController))

export class AddUser {

  firstName = '';
  lastName = '';
  age: number;
  email = '';
  addressLine1 = '';
  addressLine2 = '';
  addressLine3 = '';
  postalCode = '';
  controller = null;
  httpClient = null;
  httpClientNew = null;
  coins;
  selectedAssets: any = [];
  constructor(controller) {
    let http = new HttpClient();
    let httpnew = new HttpClient();

    this.httpClient = http;


    this.controller = controller;
    //this.httpClient = http;
    this.httpClientNew = httpnew;


    ValidationRules
      .ensure('firstName').required().minLength(3).withMessage('First Name must at least be 3 chars long.')
      .ensure('lastName').required().minLength(3).withMessage('Last Name must at least be 3 chars long.')
      .ensure('age').required().min(18).withMessage('Age must be atleast 18')
      .ensure('email').required().email().withMessage('Email must be valid')
      .ensure('addressLine1').required().withMessage('Address Line 1 cannot be empty.')
      .ensure('addressLine2').required().withMessage('Address Line 2 cannot be empty.')
      .ensure('addressLine3').required().withMessage('Address Line 3 cannot be empty.')
      .ensure('postalCode').required().withMessage('Postal Code cannot be empty.')
      .on(this);

    this.getCoins();
  }



  createUser() {

    var myUser = {
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      age: this.age,
      userAssets: [],
      userAddress: {
        addressLine1: this.addressLine1,
        addressLine2: this.addressLine2,
        addressLine3: this.addressLine3,
        postalCode: this.postalCode
      }
    }

    this.selectedAssets.forEach(element => {
      let item = new UserAsset();
      item.assetId = element;
      myUser.userAssets.push(item);
    });


    this.httpClient.fetch('https://localhost:44344/api/userprofile/createuser',
      {
        // mode: "no-cors",
        method: 'post',
        body: json(myUser),
        headers: {
          "content-type": "application/json; charset=utf-8"
        }
      }).then(data => {

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

export class UserAsset {
  assetId: any;
}