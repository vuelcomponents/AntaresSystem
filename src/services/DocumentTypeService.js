import {Service} from "@/services/Service";

export class DocumentTypeService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'document-type'
    }

}