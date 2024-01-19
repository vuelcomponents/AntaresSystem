import {Service} from "@/services/Service";

export class ComparatorService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'comparator'
    }
}