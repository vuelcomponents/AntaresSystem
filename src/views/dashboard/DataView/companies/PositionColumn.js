import toLocaleStr from "@/utils/functions/toLocaleStr";

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
                    headerName:'Unit Type',
                    field:'positionUnit',
                    editable:false,
                    valueGetter:params=>params.data?.positionUnit?.code,
                    cellClass:params=> params?.data?.positionUnit?.code === 'Position'
                        ? 'mdi mdi-account-hard-hat-outline'
                        : 'mdi mdi-chart-line-variant'
                },
                {
                    headerName: 'Demand*',
                    field: 'demand',
                    valueGetter:params=> toLocaleStr(params.data.demand)
                },
                {
                    headerName: 'Company',
                    field: 'company',
                    valueGetter:params=> params.data?.company?.code,
                    editable:false,
                },
            ]
        }
    ]
}


