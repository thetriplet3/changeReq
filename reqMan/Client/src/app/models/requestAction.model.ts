import { Request } from './request.model';
import { User } from './user.model';

export class RequestAction {
    /*[Key]*/
    public RequestActionSeq: number;
    public requestId: string;
    public username: string;
    public Action: string;
    public Comment: string;
    /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
    public Date: Date;
    /*[JsonIgnore]*/
    public Request: Request;
    public User: User;
}