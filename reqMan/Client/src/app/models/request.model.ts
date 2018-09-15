import { User } from './user.model';
import { RequestType } from './requestType.model';

export class Request {
    /*[Key]*/
    public RequestId: string;
    public RequestTypeId: string;
    public Username: string;
    public Description: string;
    public State: string;
    /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
    public DateRequested: Date;
    /*[DatabaseGenerated(DatabaseGeneratedOption.Computed)]*/
    public DateModified: Date;
    public User: User;
    public RequestType: RequestType;
}