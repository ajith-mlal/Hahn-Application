export class UserModel {
    firstName: string;
    lastName: string;
    age: number;
    email: string;
    userAssets:UserAssetsModel[];
    userAddress:UserAddressModel;
    selectedAssets:string[];
}

export class UserAssetsModel {
    assetId: string;
}

export class UserAddressModel {
    addressLine1: string;
    addressLine2: string;
    addressLine3: string;
    postalCode: string;
}