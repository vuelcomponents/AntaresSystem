export default (params, $) =>{
    switch(params.colDef?.id){
        case "goToUpdate":
            $.router.push({name:'DocumentUpdate', params:{id:params.data.id}})
            break;

    }
}