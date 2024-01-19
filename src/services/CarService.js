import {Service} from "@/services/Service";

export class CarService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'car'
    }
}