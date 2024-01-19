import {Service} from "@/services/Service";

export class StatusService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'status'
    }
}