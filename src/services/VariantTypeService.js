import {Service} from "@/services/Service";

export class VariantTypeService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'variant-type'
    }
}