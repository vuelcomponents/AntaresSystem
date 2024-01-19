export default  (collection, key, options) => {
    options.life.param = key;
    if(!key){
        options.life.id = null;
        return;
    }
    const rows = options.grid.getAllRows()
    const row = rows.find(g=>g.id === options.life.id)
    row[key] = collection
    options.grid.api.setRowData(rows)
}