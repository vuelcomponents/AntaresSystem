import activeColumn from "@/utils/grid/activeColumn";
import {getThemeName} from "@/core/theme";

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
                    headerClass: "mdi mdi-domain biggerFont flexCenter",
                    cellClass: "mdi mdi-domain biggerFont  active-update-cell-custom"
                },
                {
                    headerName: 'Status  ⥨',
                    cellRenderer:'StatusSwitchRenderer',
                    minWidth: 200,
                    field:'status',
                    storeModel:"Company",
                    editable:false,
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
                    headerName: 'Description*',
                    field: 'description',
                    minWidth: 200,
                },
                {
                    headerName: 'Realisations',
                    collectionKey:'realisations',
                    valueGetter:params => params.data?.realisations?.length,
                    cellClass: `active-cell-custom`
                },
                {
                    headerName: 'Documents',
                    collectionKey:'documents',
                    valueGetter:params => params.data?.documents?.length,
                    cellClass: `active-cell-custom`
                },
                {
                    headerName: 'Address',
                    field: 'address',
                    minWidth: 200,
                    children: [
                        {
                            headerName: 'House and Local Number',
                            field: 'houseAndLocalNumber',
                            minWidth: 200,
                        },
                        {
                            headerName: 'Street Name',
                            field: 'streetName',
                            minWidth: 200,
                        },
                        {
                            headerName: 'Postcode',
                            field: 'postCode',
                            minWidth: 200,
                        },
                        {
                            headerName: 'City',
                            field: 'city',
                            minWidth: 200,
                        },
                        {
                            headerName: 'Country',
                            field: 'country',
                            minWidth: 200,
                        },
                    ]
                },

            ]
        }
    ]
}


