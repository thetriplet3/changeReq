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
}