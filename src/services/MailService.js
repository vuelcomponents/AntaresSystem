import {Service} from "@/services/Service";

export class MailService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'mail'
    }

}