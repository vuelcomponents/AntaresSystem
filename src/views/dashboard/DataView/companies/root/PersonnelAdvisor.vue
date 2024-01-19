<script>
import {inject} from "vue";
import CompanyClass from "@/views/dashboard/DataView/companies/CompanyClass";
import GridClass from "@/utils/grid/GridClass";
import AdviceEmployeesColumn from "@/views/dashboard/DataView/companies/AdviceEmployeesColumn";
import {CompanyService} from "@/services/CompanyService";

export default{
  props:{
    company:CompanyClass,
  },
  setup(){
    const emitter = inject('emitter')
    const services = {
      company:new CompanyService(emitter)
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
    this.advise =  await this.services.company.advise(this.company)
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
      this.columns = AdviceEmployeesColumn();
          this.advise.data?.columns.forEach(col=>{
            this.columns.push({
              headerName:col,
              comparator:(va,vb,na,nb,d)=>{
                return na.data?.advises.find(a=>a.position?.code === col).withLossPercent
                    - nb.data?.advises.find(a=>a.position?.code === col).withLossPercent
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