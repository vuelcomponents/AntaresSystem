import {Service} from "@/services/Service";

export class ActionFunctionService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'action-function'
    }

}