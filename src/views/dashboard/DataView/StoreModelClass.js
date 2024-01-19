import ModelClass from "@/core/ModelClass";


export default class extends ModelClass{
    id;
    code;

    headers = {
        id: "Id",
        code:"Code",
    }
    text= [
        'code',
    ]
    required = [
        'code',
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

}