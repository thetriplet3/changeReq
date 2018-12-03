import { User } from './user.model';
import { RequestType } from './requestType.model';

export class Request {
    /*[Key]*/
    requestId: string;
    requestTypeId: string;
    username: string;
    description: string;
    state: string;
    /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
    dateRequested: string;
    /*[DatabaseGenerated(DatabaseGeneratedOption.Computed)]*/
    dateModified: string;
    user: User;
    requestType: RequestType;

    //Common for Child care and Pension
    employer?: string;
    address?: string;
    city?: string;
    county?: string;
    postal?: number;
    nin?: string;
    dob?: string;

    //Child care
    title?: string;
    firstName?: string;
    lastName?: string;
    jobTitle?: string;
    department?: string;
    payrollNo?: string;
    homeContactNo?: string;
    workContactNo?: string;
    email: string;

    //Pension
    schemeName?: string;
    schemeNumber?: number;
    fullName?: string;
    nationality?: string;
    contactNo?: string;
    reference?: string;
}