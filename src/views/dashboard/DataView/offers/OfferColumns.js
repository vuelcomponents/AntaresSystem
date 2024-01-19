

export default () => {
    return [
        {
            groupId: 'options',
            headerName: 'Options',

            children: [
                {
                    headerCheckboxSelection: true,
                    checkboxSelection: true,
                    showDisabledCheckboxes: true,
                    filter: false,
                    width: 55,
                },
                {
                    headerName: "",
                    width: 15,
                    id: 'goToUpdate',
                    headerClass: "mdi mdi-advertisements biggerFont flexCenter",
                    cellClass: "mdi mdi-advertisements biggerFont  active-update-cell-custom"
                },

            ]
        },
        {

            headerName: 'Details',
            children: [
                {
                    headerName: 'Code*',
                    field: 'code',
                    minWidth: 200,

                },
                {
                    headerName: 'Title*',
                    field: 'title',
                    minWidth: 200,
                },
                {
                    headerName: 'Message',
                    field: 'message',
                    valueGetter:(params)=>{
                        return `${params.data?.message?.substring(0,40)}...`
                    },
                    editable:false,
                    minWidth: 200,
                },
                {
                    headerName: 'Image',
                    // collectionKey:'image',
                    valueGetter:params => params.data?.image?.code,
                    cellClass: `active-cell-custom`
                },
                {
                    headerName: 'Position',
                    field: 'position.code',
                    // owner:'offer',
                    // cellRenderer:'SelectionRenderer',
                    editable:false,
                    minWidth: 200,
                },
                {
                    headerName: 'Company',
                    field: 'company.code',
                    // owner:'offer',
                    // cellRenderer:'SelectionRenderer',
                    editable:false,
                    minWidth: 200,
                },

            ]
        }
    ]
}


