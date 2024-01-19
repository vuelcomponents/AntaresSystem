export default (modules, functions)=>{
    const menu = []
    if(modules?.includes('delete')){
       menu.push( {
            label:'Delete selected',
            icon:'mdi mdi-minus',
            command:()=>{functions.deleteSelected()},
        })
    }
    return menu
}