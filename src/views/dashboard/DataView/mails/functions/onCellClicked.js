export default (params, $) =>{
    console.log('cell clicked', params)
    switch(params.colDef.id){
        case "goToUpdate":
            $.router.push({name: 'MailUpdate', params:{id:params.data.id}})
            break;
    }
}