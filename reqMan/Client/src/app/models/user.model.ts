export class User {
    username: string;
    password: string;
    email?: string;
    userType?: string;
    firstName?: string;
    lastName?: string;
}

export class UserType {
    userTypeId: string;
    name: string;
}
