import {Service} from "@/services/Service";

export class VariantService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'variant'
    }
}