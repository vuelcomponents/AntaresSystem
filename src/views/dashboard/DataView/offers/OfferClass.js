import ModelClass from "@/core/ModelClass";

export default class extends ModelClass {

    id;
    code;
    title;
    message;
    image;
    global;
    company;
    position;
    headers = {
        id: "Id",
        code:"Code",
        title:"Title",
        message:"Message",
        image:"Image",
        company:"Company",
        global:"Global",
        position:"Position"
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
        "image",
        'company',
        'global',
        "position"
    ]
    collections = [

    ]
    booleans = ['global']
    create = [

    ]
    select = [

    ]
    delete = [

    ]
    update = [

    ]

    objects = [
        'status',
        'company',
        'position',
        'image'
    ]
    textArea = [
        'message'
    ]
    textAreaHeaders = [
        'title'
    ]
    $hide = {
        // image: (options)=>{
        //     if(!this.id){
        //         return true;
        //     }
        //     if(this.id === 0){
        //         return true;
        //     }
        //     return false;
        // },

    }
    $filters = {
        position:(model,collection)=>{
            return collection.filter(c=>c.company.id === model.company?.id)
        },
        image:(model,collection)=>{
            return collection.filter(c => [".jpg", ".jpeg", ".png", ".gif", ".bmp"]
                .some(ext => c.fileName.endsWith(ext)))
        }

    }
    $lock = {
        position:()=>{
            return !!this.id;
        },
        company:()=>{
            return !!this.id;
        }
    }



}