import {Service} from "@/services/Service";

export class StoreModelService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'store-model'
    }
}