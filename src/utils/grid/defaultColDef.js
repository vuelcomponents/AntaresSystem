export default {
    flex: 1,
    minWidth: 75,
    resizable: true,
    cellClass:(params)=>{
        return `custom-active-cell active-cell-${params.data.id}-${params.colDef.field}`.replaceAll('.','-')
    },
    menuTabs: ['filterMenuTab'],
    filter:true,
    sortable:true,
    editable:true,
    cellStyle:(params)=>{
        const style= {textAlign:'left'}
        return style
    }
}