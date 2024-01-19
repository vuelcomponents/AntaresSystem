<script>
import {inject} from "vue";
import {EmployeeService} from "@/services/EmployeeService";
import GridClass from "@/utils/grid/GridClass";
import EmployeeClass from "@/views/dashboard/DataView/employees/EmployeeClass";
import AdviseEmployeeCompanyColumn from "@/views/dashboard/DataView/employees/AdviseEmployeeCompanyColumn";

export default{
  props:{
    employee:EmployeeClass,
  },
  setup(){
    const emitter = inject('emitter')
    const services = {
      employee:new EmployeeService(emitter)
    }
    return {
      services
    }
  },
  data(){
    return{
      grid:new GridClass({
            onCellValueChanged: null,
            onCellClicked: this.onCellClicked,
            onSelectionChanged: this.onSelectionChanged,
          },{
            loadList:this.loadList
          },
          this.services.employee
      ),
      advise:{},
      columns:[]

    }
  },
  async mounted(){
    this.advise =  await this.services.employee.advise({id: this.employee.id})
    this.getColumns()
    this.loadList();
  },
  methods:{
    loadList(){
      if(this.grid.api) {
        this.grid.api.setRowData(this.advise.data?.adviseContainers)
      }
    },
    getColumns(){
      this.columns = AdviseEmployeeCompanyColumn();
      this.advise.data?.columns.forEach(col=>{
        this.columns.push({
          headerName:col,
          comparator:(va,vb,na,nb,d)=>{
            if(na.data?.advises.find(a => a.position?.code === col)?.withLossPercent && nb.data?.advises.find(a => a.position?.code === col)?.withLossPercent) {
              return na.data?.advises.find(a => a.position?.code === col).withLossPercent
                  - nb.data?.advises.find(a => a.position?.code === col).withLossPercent
            }else return 1;
          },
          cellRenderer:'AdviseRowRenderer'
        })
      })
    },

  }
}
</script>

<template>
  <ag-grid-vue
      class="ag-theme-alpine _grid grid-black"
      domLayout="autoHeight"
      :rowData="grid.data"
      :grid-options="grid.options"
      rowSelection="multiple"
      animateRows="true"
      :columnDefs="columns"

  >
  </ag-grid-vue>
</template>

<style >

</style>