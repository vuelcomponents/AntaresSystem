

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
                    headerClass: "mdi mdi-email-box biggerFont flexCenter",
                    cellClass: "mdi mdi-email-box biggerFont  active-update-cell-custom"
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
                    headerName: 'Documents',
                    collectionKey:'documents',
                    valueGetter:params => params.data?.documents?.length,
                    cellClass: `active-cell-custom`
                },
            ]
        }
    ]
}


