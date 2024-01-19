import {Service} from "@/services/Service";
import moment from "moment";


export class DocumentService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'document'
    }
}