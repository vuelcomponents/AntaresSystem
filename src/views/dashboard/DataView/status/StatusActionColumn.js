
export default () => {
    return [
        {
            headerName: 'Details',
            children: [

                {
                    headerCheckboxSelection: true,
                    checkboxSelection: true,
                    showDisabledCheckboxes: true,
                    filter: false,
                    width: 55,
                },
                {
                    headerName: 'Function',
                    field: 'actionFunction',
                    minWidth: 200,
                    valueGetter:params=> params.data?.actionFunction?.code,
                    editable:false,
                },
                {
                    headerName: 'Trigger',
                    field: 'statusActionTrigger.code',
                    minWidth: 200,
                    editable:false,
                },
            ]
        }
    ]
}


