import {Service} from "@/services/Service";

export class SystemFunctionService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'system-function'
    }

}