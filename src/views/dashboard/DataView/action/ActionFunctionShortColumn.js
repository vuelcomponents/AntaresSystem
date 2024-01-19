
export default () => {
    return[
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
            ]
        },
        {
            groupId: 'personal',
            headerName: 'Personal',
            children: [
                {
                    headerName:'Details',
                    children:[
                        {
                            headerName: 'Name*',
                            field: 'firstName',
                            minWidth: 200,

                        },
                        {
                            headerName: 'Last Name*',
                            field: 'lastName',
                            minWidth: 200,
                        },
                        {
                            headerName: 'Date of Birth*',
                            field: 'dateOfBirth',
                            minWidth: 200,
                            cellRenderer:'DateRenderer',
                            headerClass: 'ag-right-aligned-header',
                        },
                    ]
                },
                {
                    headerName:'Contact and Address',
                    children:[
                        {
                            headerName: 'Private Phone*',
                            field: 'privatePhone',
                            minWidth: 200,
                            right:true,
                            headerClass: 'ag-right-aligned-header',
                        },
                        {
                            headerName: 'Main Address',
                            children:[
                                {
                                    headerName:'House and local number',
                                    field: 'houseAndLocalNumber',
                                    minWidth: 200,
                                },
                                {
                                    headerName:'Street name',
                                    field: 'streetName',
                                    minWidth: 200,
                                },
                                {
                                    headerName:'Postcode',
                                    field: 'postCode',
                                    minWidth: 200,
                                },
                                {
                                    headerName:'City',
                                    field: 'city',
                                    minWidth: 200,
                                },
                            ],
                        },
                        {
                            headerName: 'Sub Address',
                            children:[
                                {
                                    headerName:'House and local number',
                                    field: 'subHouseAndLocalNumber',
                                    minWidth: 200,
                                },
                                {
                                    headerName:'Street name',
                                    field: 'subStreetName',
                                    minWidth: 200,
                                },
                                {
                                    headerName:'Postcode',
                                    field: 'subPostcode',
                                    minWidth: 200,
                                },
                                {
                                    headerName:'City',
                                    field: 'subCity',
                                    minWidth: 200,
                                },
                            ],
                        },

                    ]
                }
            ]
        },
    ]
}