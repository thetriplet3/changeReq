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
    attachment?: File;
    attachmentDir?: string;
    
    //Chid Care
    currentAmount?: number;
    revisedAmount?: number;

    //Pension
    currentPensionPerecentage?: number;
    revisedPensionPerecentage?: number;
    optOut?: boolean;

    //Cycle to Work
    cycleSchemeRequest?: string;
    cyclePartnerList?: string;

    //Gym and Season Ticket Loan
    startDate?: Date | string;

    //Season Ticket Loan
    zoneFrom?: string; 
    zoneTo?: string;

    //Taste Card
    tasteCardLink?: string;
}

export interface Attachment {
    fileName: string;
    fileType: string;
    fileUrl: string;
    fileLocation: string;
}