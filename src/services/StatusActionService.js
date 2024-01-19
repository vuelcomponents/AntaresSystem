import {Service} from "@/services/Service";

export class StatusActionService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'status-action'
    }

}