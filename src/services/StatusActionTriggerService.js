import {Service} from "@/services/Service";

export class StatusActionTriggerService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'status-action-trigger'
    }

}