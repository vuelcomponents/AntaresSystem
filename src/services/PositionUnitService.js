import {Service} from "@/services/Service";

export class PositionUnitService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'position-unit'
    }

}