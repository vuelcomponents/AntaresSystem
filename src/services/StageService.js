import {Service} from "@/services/Service";

export class StageService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'stage'
    }
}