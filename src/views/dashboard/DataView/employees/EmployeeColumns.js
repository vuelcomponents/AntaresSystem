import activeColumn from "@/utils/grid/activeColumn";
import {getThemeName} from "@/core/theme";

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
            {
                headerName:"",
                width: 15,
                id:'goToUpdate',
                headerClass: "mdi mdi-account-arrow-right biggerFont flexCenter",
                cellClass:"mdi mdi-account-arrow-right biggerFont active-update-cell-custom"
            },
            {
                headerName: 'Status  ⥨',
                cellRenderer:'StatusSwitchRenderer',
                minWidth: 200,
                field:'status',
                valueGetter:params=>{
                  return params.data?.status?.code
                },
                storeModel:"Employee",
                editable:false,
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
                      headerName: 'Personal ID',
                      field: 'pesel',
                      minWidth: 200,
                  },

                  {
                      headerName: 'Email*',
                      field: 'email',
                      minWidth: 200,
                  },
                  {
                      headerName: 'Tax ID',
                      field: 'bsn',
                      minWidth: 200,
                  },
                  {
                      headerName: 'Realisations',
                      collectionKey:'realisations',
                      valueGetter:params => params.data?.realisations?.length,
                      cellClass: `active-cell-custom`
                  },
                  {
                      headerName: 'Companies',
                      collectionKey:'companies',
                      valueGetter:params => params.data?.companies?.length,
                      cellClass: `active-cell-custom`
                  },
                  {
                      headerName: 'Documents',
                      collectionKey:'documents',
                      valueGetter:params => params.data?.documents?.length,
                      cellClass: `active-cell-custom`
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