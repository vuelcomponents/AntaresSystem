export default (params, $) =>{
    if(!params.data){
        return;
    }
    $.service.update(params.data).then(res=>{
        if(!res.status || res.status !==200) {
            var colId = params.colDef.field;
            var rowData = params.node.data;
            rowData[colId] = params.oldValue;
            params.node.setData(rowData);
            return;
        }
        if(res.data){
            params.node.setData(res.data)
            // ostatnio dodane, trzeba wszedzie dodac populacje na update
        }
    })
}