import {Service} from "@/services/Service";

export class HouseService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'house'
    }
}