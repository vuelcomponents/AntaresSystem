import ModelClass from "@/core/ModelClass";


export default class extends ModelClass{
    id;
    code;
    description;
    headers = {
        id: "Id",
        code:"Code",
        description:'Description',
    }
    text= [
        'code',
        'description',
    ]
    required = [
        'code',
        'description',
    ]
    dates = [

    ]
    numeric = []
    files = [
    ]
    collections = [

    ]
    create = [

    ]
    select = [

    ]
    delete = [

    ]
    update = [

    ]
    objects = [

    ]
    $hide = {
    }
    $filters = {

    }


}