import ModelClass from "@/core/ModelClass";
import RealisationService from "@/services/RealisationService";
import RealisationColumn from "@/views/dashboard/DataView/variants/RealisationColumn";

export default class extends ModelClass {

    id;
    code;
    title;
    message;
    documents;
    headers = {
        id: "Id",
        code:"Code",
        title:"Title",
        message:"Message",
        documents:"Documents"
    }
    text = [
        "code",
        'title',
    ]
    numeric = []
    required = [
        "code",
        "message",
        "title",
        "documents"
    ]
    collections = [
        "documents"
    ]
    create = [
        "documents"
    ]
    select = [
        "documents"
    ]
    delete = [
        "documents"
    ]
    update = [
        "documents"
    ]

    objects = [
        'status'
    ]
    textArea = [
        'message'
    ]
    textAreaHeaders = [
        'title'
    ]
    $hide = {
        documents: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },

    }
    $filters = {

    }



}