import ModelClass from "@/core/ModelClass";
import RealisationService from "@/services/RealisationService";
import RealisationColumn from "@/views/dashboard/DataView/variants/RealisationColumn";

export default class extends ModelClass {

    id;
    code;
    description;
    status;
    headers = {
        id: "Id",
        code:"Code",
        description:"Description",
        status:"Status",

    }
    text = [
        "code",
        'description',
    ]
    numeric = []
    required = [
        "code",
        "description",
        "status",

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
        'status',
    ]
    textArea = [

    ]
    textAreaHeaders = [

    ]
    $hide = {

    }
    $lock = {

    }
    $filters = {

    }



}